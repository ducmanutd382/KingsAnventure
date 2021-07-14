using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject playScreen;
    public PanelManager mainScr;

    // Start is called before the first frame update
    void Start()
    {
        PlayStateManager.PlayStateChanged += PlayStateChanged;
    }

    private void PlayStateChanged()
    {
        switch (PlayStateManager.CurrentPlayState)
        {
            case PlayStateManager.GameState.Play:
                Time.timeScale = 1;
                playScreen.SetActive(true);
                pauseScreen.SetActive(false);
                break;
            case PlayStateManager.GameState.Pause:
                Time.timeScale = 0;
                playScreen.SetActive(false);
                pauseScreen.SetActive(true);
                break;
        }      
    }

    public void Ins_BtnPause_Clicked()
    {
        PlayStateManager.CurrentPlayState = PlayStateManager.GameState.Pause;
        Debug.Log("da an pauseeeeeeeee");
    }

    public void Ins_BtnResume_Clicked()
    {
        //StartCoroutine(SceneHelper.CurrentMap);
        PlayStateManager.CurrentPlayState = PlayStateManager.GameState.Play;
        //pauseScreen.SetActive(false);
    }

    public void Ins_BtnMainScene_Clicked()
    {
        
        //StartCoroutine(SceneHelper.UnLoadMap());
        StartCoroutine(SceneHelper.DoLoadMainScene());
        //mainScr.gameObject.GetComponent<UIMainManager>().mainScreen.SetActive(true);
        //pauseScreen.SetActive(false);
        //playScreen.SetActive(false);
        /*mainScr.gameObject.GetComponent<UIMainManager>().stateScreen.SetActive(false);
        mainScr.gameObject.GetComponent<UIMainManager>().mainScreen.SetActive(true);*/
    }
}
