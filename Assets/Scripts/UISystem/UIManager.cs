using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    private static UIManager _instance;

    private GameObject canvas;


    private GameObject blurBackground;
    private GameObject mainMenuPanel;
    private GameObject pauseMenuPanel;
    private GameObject HUDPanel;
    private GameObject gameOverMenuPanel;
    private GameObject coutdownPanel;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }

    public UIManager()
    {
        canvas = GameplayManager.Instance.canvas;
        if (canvas == null)
        {
            Debug.LogError("Canvas not found in UIManager");

        }

        Init();
        SetupButtonEvent();

    }

    private void Init()
    {
        blurBackground = canvas.transform.Find("BlurBackground").gameObject;
        mainMenuPanel = canvas.transform.Find("MainMenuPanel").gameObject;
        pauseMenuPanel = canvas.transform.Find("PauseMenuPanel").gameObject;
        HUDPanel = canvas.transform.Find("HUD").gameObject;
        gameOverMenuPanel = canvas.transform.Find("GameOverMenuPanel").gameObject;
        coutdownPanel = canvas.transform.Find("CountdownPanel").gameObject;
    }

    public void SetupButtonEvent()
    {
        // Main Menu
        mainMenuPanel.transform.Find("ButtonGroup/PlayButton").GetComponent<Button>().onClick.AddListener(OnStartButtonClicked);
        mainMenuPanel.transform.Find("ButtonGroup/ExitButton").GetComponent<Button>().onClick.AddListener(Application.Quit);

        // Pause Menu
        pauseMenuPanel.transform.Find("ButtonGroup/ResumeButton").GetComponent<Button>().onClick.AddListener(OnResumeButtonClicked);
        pauseMenuPanel.transform.Find("ButtonGroup/NewGameButton").GetComponent<Button>().onClick.AddListener(OnRestartButtonClicked);
        pauseMenuPanel.transform.Find("ButtonGroup/MainMenuButton").GetComponent<Button>().onClick.AddListener(OnMainMenuButtonClicked);

        // Game Over Menu
        gameOverMenuPanel.transform.Find("ButtonGroup/NewGameButton").GetComponent<Button>().onClick.AddListener(OnRestartButtonClicked);
        gameOverMenuPanel.transform.Find("ButtonGroup/MainMenuButton").GetComponent<Button>().onClick.AddListener(OnMainMenuButtonClicked);

        // HUD
        HUDPanel.transform.Find("PauseButton").GetComponent<Button>().onClick.AddListener(OnPauseButtonClicked);

    }


    private void SetText(TMP_Text text, string value)
    {
        if (text != null)
        {
            text.text = value;
        }
        else
        {
            Debug.LogError("Text not found");
        }
    }

    public void ShowBlurBackground(bool show)
    {
        blurBackground.SetActive(show);
    }

    public void ShowMainMenuPanel(bool show)
    {
        mainMenuPanel.SetActive(show);
        ShowBlurBackground(show);
    }

    public void ShowPauseMenuPanel(bool show)
    {

        pauseMenuPanel.SetActive(show);
        ShowBlurBackground(show);
    }

    public void ShowHUDPanel(bool show)
    {
        HUDPanel.SetActive(show);
        
    }

    public void ShowGameOverMenuPanel(bool show)
    {
        gameOverMenuPanel.SetActive(show);
        ShowBlurBackground(show);
    }

    public void ShowCountdownPanel(bool show)
    {
        ShowBlurBackground(false);
        coutdownPanel.SetActive(show);
        
    }

    private void OnStartButtonClicked()
    {
        GameplayManager.Instance.StartGame();
        ShowMainMenuPanel(false);
        UpdateHUDPanel(0, 0);
        ShowHUDPanel(true);
    }

    private void OnPauseButtonClicked()
    {
        GameplayManager.Instance.PauseGame();
        ShowPauseMenuPanel(true);
    }

    private void OnResumeButtonClicked()
    {
        GameplayManager.Instance.ContinueGame();
        ShowPauseMenuPanel(false);
    }

    private void OnRestartButtonClicked()
    {
        GameplayManager.Instance.RestartGame();
        ShowGameOverMenuPanel(false);
        ShowPauseMenuPanel(false);
        ShowHUDPanel(true);
    }

    private void OnMainMenuButtonClicked()
    {
        GameplayManager.Instance.Reset();
        ShowGameOverMenuPanel(false);
        ShowMainMenuPanel(true);
        
    }

    public void UpdatePauseMenuPanel(float distance, int coin)
    {
        SetText(pauseMenuPanel.transform.Find("StatGroup/Distance/Value").GetComponent<TMP_Text>(), distance.ToString("0"));
        SetText(pauseMenuPanel.transform.Find("StatGroup/Coins/Value").GetComponent<TMP_Text>(), coin.ToString());
    }

    public void UpdateHUDPanel(float distance, int coin)
    {
        
        SetText(HUDPanel.transform.Find("Distance").GetComponentInChildren<TMP_Text>(), distance.ToString("0"));
        SetText(HUDPanel.transform.Find("Coin").GetComponentInChildren<TMP_Text>(), coin.ToString());
    }

    public void UpdateGameOverMenuPanel(float distance, int coin)
    {
        SetText(gameOverMenuPanel.transform.Find("StatGroup/Distance/Value").GetComponent<TMP_Text>(), distance.ToString("0"));
        SetText(gameOverMenuPanel.transform.Find("StatGroup/Coins/Value").GetComponent<TMP_Text>(), coin.ToString());
    }

    public void UpdateCountDownPanel(int time)
    {
        SetText(coutdownPanel.transform.GetChild(0).GetComponent<TMP_Text>(), time.ToString());
    }

}
