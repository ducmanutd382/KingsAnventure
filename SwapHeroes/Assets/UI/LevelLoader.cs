using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] GameObject menuScene;
    [SerializeField] GameObject Map1;
    [SerializeField] GameObject Map2;

    private void Start()
    {
        
    }

    private void SceneChanged()
    {

    }

    /*public void Ins_BtnStageOne_Clicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetSceneByName()GameScene);
    }
    public void Ins_BtnStageTwo_Clicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //SceneManager.LoadScene(SceneManager.GetSceneByName()GameScene);
    }*/
}
