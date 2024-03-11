using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameManager gm;

    [SerializeField] Canvas m_mainPanel;
    [SerializeField] Canvas m_optionPanel;
    [SerializeField] Canvas m_controlOptionPanel;
    [SerializeField] Canvas m_soundOptionPanel;
    [SerializeField] Canvas m_graphicOptionPanel;
    [SerializeField] Canvas m_CreditPanel;

    [SerializeField] string m_playButonSceneName;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;

        if (gm.m_playerInpuActions != null)
            gm.m_playerInpuActions.SwitchCurrentActionMap("UI");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        gm.LoadScene(m_playButonSceneName);
    }

    //TODO fare le meme system de bouton que soundMenu

    public void MainToOption()
    {
        m_mainPanel.enabled = false;
        m_optionPanel.enabled = true;
    }

    public void MainToCredit()
    {
        m_mainPanel.enabled = false;
        m_CreditPanel.enabled = true;
    }

    public void OptionToControl()
    {
        m_optionPanel.enabled = false;
        m_controlOptionPanel.enabled = true;
    }

    public void OptionToSound()
    {
        m_optionPanel.enabled = false;
        m_soundOptionPanel.enabled = true;
    }

    public void OptionToGraphic()
    {
        m_optionPanel.enabled = false;
        m_graphicOptionPanel.enabled = true;
    }

    public void BackOption()
    {
        m_mainPanel.enabled = true;
        m_optionPanel.enabled = false;
    }

    public void BackCredit()
    {
        m_mainPanel.enabled = true;
        m_CreditPanel.enabled = false;
    }

    public void BackControl()
    {
    m_optionPanel.enabled = true;
    m_controlOptionPanel.enabled = false;
    }

    public void BackGraphic()
    {
        m_optionPanel.enabled = true;
        m_graphicOptionPanel.enabled = false;
    }

    public void BackSound()
    {
        m_optionPanel.enabled = true;
        m_soundOptionPanel.enabled = false;
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
