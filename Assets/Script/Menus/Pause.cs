using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    GameManager gm;

    Canvas m_canvas;

    [SerializeField] string m_menuSceneName;

    void Start()
    {
        gm = GameManager.Instance;
        m_canvas = gameObject.GetComponent<Canvas>();
    }

    public void ActiveDesactivePause()
    {
        if (m_canvas.enabled)
        {
            Resume();
        }
        else
        {
            gm.StopTime(true);
            gm.SetMouseVisible(true);

            if (gm.m_playerInpuActions != null)
                gm.m_playerInpuActions.SwitchCurrentActionMap("UI");

            m_canvas.enabled = true;
        }
    }

    public void Resume()
    {
        gm.StopTime(false);
        gm.SetMouseVisible(false);

        if (gm.m_playerInpuActions != null)
            gm.m_playerInpuActions.SwitchCurrentActionMap("Player");

        m_canvas.enabled = false;
    }

    public void QuitToMenu()
    {
        gm.StopTime(false);
        gm.SetMouseVisible(true);

        if (gm.m_playerInpuActions != null)
            gm.m_playerInpuActions.SwitchCurrentActionMap("UI");

        gm.LoadScene(m_menuSceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }






}
