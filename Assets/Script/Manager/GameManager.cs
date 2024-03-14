using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    //singleton
    #region singletonPara
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
    }
    #endregion


    public GameObject m_zero;
    public PlayerInput m_playerInpuActions;

    [SerializeField] Image m_blackImage;
    [SerializeField] float m_timeOfFade;



    private void Start()
    {
        m_zero = GameObject.Find("Zero");
        if (m_zero != null)
        {
            m_playerInpuActions = m_zero.GetComponent<PlayerInput>();
        }
    }

    public void StopTime(bool _stopTime)
    {
        if (_stopTime)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void LoadScene(string _name)
    {
        StartCoroutine(LoadSceneAsync(_name));
    }

    IEnumerator LoadSceneAsync(string _name)
    {
        // Load the loading screen
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("LoadingScene");
        loadingOperation.allowSceneActivation = false;

        // Load the target scene in the background
        AsyncOperation targetSceneOperation = SceneManager.LoadSceneAsync(_name);
        targetSceneOperation.allowSceneActivation = false;

        //pause
        StopTime(true);

        //fadeIn
        Color color = Color.magenta;
        if (m_blackImage != null)
        {
            float countdown = 0;
            color = m_blackImage.color;
            while (m_timeOfFade > countdown)
            {
                countdown += Time.unscaledDeltaTime;

                float alpha = countdown / m_timeOfFade;
                if (alpha > 1)
                    alpha = 1;

                color.a = alpha;
            }
        }

        //loadingscrean
        while (!loadingOperation.isDone)
        {
            // If the loading scene is fully loaded, activate it
            if (loadingOperation.progress >= 0.9f)
            {
                StopTime(false);
                //stop all sound / fmod event
                FMOD.Studio.Bus masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
                masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);

                if (color != Color.magenta) color.a = 0;
                loadingOperation.allowSceneActivation = true;
            }
            yield return null;

        }

        while (!targetSceneOperation.isDone)
        {
            // Update your loading screen progress here (e.g., loading bar)
            float progress = Mathf.Clamp01(targetSceneOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // If the target scene is fully loaded, activate it
            if (targetSceneOperation.progress >= 0.9f)
            {
                StopTime(false);
                if (m_playerInpuActions != null)
                    m_playerInpuActions.SwitchCurrentActionMap("Player");

                targetSceneOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        //fadeOut
        color = Color.magenta;
        if (m_blackImage != null)
        {
            float countdown = 0;
            color = m_blackImage.color;
            color.a = 1;
            while (m_timeOfFade > countdown)
            {
                countdown += Time.unscaledDeltaTime;

                float alpha = 1 - countdown / m_timeOfFade;
                if (alpha < 0)
                    alpha = 0;

                color.a = alpha;
            }
        }
    }

    public void SetMouseVisible(bool _visible)
    {
        if (_visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
