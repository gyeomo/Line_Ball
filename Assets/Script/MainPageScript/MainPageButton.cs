using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageButton : MonoBehaviour {

    public Button button_MS;
    public Button button_MQ;
    // Use this for initialization
    void Start () {
        button_MS.onClick.AddListener(Button_MS);
        button_MQ.onClick.AddListener(Button_MQ);
    }

    // Update is called once per frame
    void Button_MS()
    {
        SceneManager.LoadScene("GameStage1", LoadSceneMode.Single);
    }
    void Button_MQ()
    {
        System.GC.Collect();
        Application.Quit();
    }
}
