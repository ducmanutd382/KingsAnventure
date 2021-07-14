using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject stateScreen;
    [SerializeField] GameObject exitScreen;
    [SerializeField] GameObject playScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.GameStateChanged += GameStateChanged;
    }

    private void GameStateChanged()
    {
        switch (GameStateManager.CurrentState)
        {
            case GameState.Idle:
                mainScreen.SetActive(true);
                stateScreen.SetActive(false);
                exitScreen.SetActive(false);
                playScreen.SetActive(false);
                pauseScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                break;
            case GameState.Stage:
                mainScreen.SetActive(false);
                stateScreen.SetActive(true);
                exitScreen.SetActive(false);
                playScreen.SetActive(false);
                pauseScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                break;
            case GameState.Exit:
                exitScreen.SetActive(true);
                mainScreen.SetActive(true);
                stateScreen.SetActive(false);
                playScreen.SetActive(false);
                pauseScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                break;
            case GameState.Play:
                Time.timeScale = 1;
                exitScreen.SetActive(false);
                mainScreen.SetActive(false);
                stateScreen.SetActive(false);
                playScreen.SetActive(true);
                pauseScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                exitScreen.SetActive(false);
                pauseScreen.SetActive(true);
                mainScreen.SetActive(false);
                playScreen.SetActive(false);
                stateScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                break;
            case GameState.Win:
                Time.timeScale = 0;
                exitScreen.SetActive(false);
                pauseScreen.SetActive(false);
                mainScreen.SetActive(false);
                playScreen.SetActive(false);
                stateScreen.SetActive(false);
                winScreen.SetActive(true);
                loseScreen.SetActive(false);
                break;
            case GameState.Lose:
                Time.timeScale = 0;
                exitScreen.SetActive(false);
                pauseScreen.SetActive(false);
                mainScreen.SetActive(false);
                playScreen.SetActive(false);
                stateScreen.SetActive(false);
                winScreen.SetActive(false);
                loseScreen.SetActive(true);
                break;
            
        }
    }

    public void Ins_BtnPlay_Clicked()
    {
        GameStateManager.CurrentState = GameState.Stage;
    }
    public void Ins_BtnBackToMenu_Clicked()
    {
        GameStateManager.CurrentState = GameState.Idle;
    }

    public void Ins_BtnExitScreen_Clicked()
    {
        GameStateManager.CurrentState = GameState.Exit;
    }
    public void Ins_QuitGame_Clicked()
    {
        Application.Quit();
        Debug.Log("Game exit");
    }

    public void Ins_BtnPause_Clicked()
    {
        GameStateManager.CurrentState = GameState.Pause;
    }

    public void Ins_BtnResume_Clicked()
    {
        //StartCoroutine(SceneHelper.CurrentMap);
        GameStateManager.CurrentState = GameState.Play;
    }

    public void Ins_StageOneScene_Clicked()
    {
        GameStateManager.CurrentState = GameState.Play;
        StartCoroutine(SceneHelper.DoLoadMap1());
        
        //GameStateManager.CurrentState = GameState.Play;
    }

    public void Ins_UnLoadScene_Clicked()
    {
        StartCoroutine(SceneHelper.UnLoadMap());
        GameStateManager.CurrentState = GameState.Play;
    }

    public void Ins_StageTwoScene_Clicked()
    {
        StartCoroutine(SceneHelper.DoLoadMap2());
        GameStateManager.CurrentState = GameState.Play;
    }

    public void UnLoad_CurrentMap_Clicked()
    {
        StartCoroutine(SceneHelper.UnLoadMap());
    }


}
