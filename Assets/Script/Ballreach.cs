using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ballreach : MonoBehaviour {
    public GameObject panel;
    public Button button_X;
    public Button button_T;
    public Button button_N;
    public Text text;
    private DrawLine2D script2;
    // Use this for initialization
    void Start () {
        script2 = GameObject.Find("GameObject").GetComponent<DrawLine2D>();
    }
	
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Circle1"))
        {
            FinishGame();
        }

    }
    public void FinishGame()
    {
        text.text = "Success";
        panel.gameObject.SetActive(true);
        button_X.gameObject.SetActive(false);
        script2.isMenuActive = true;
        if (SceneManager.GetActiveScene().buildIndex != 15)
        {
            button_T.gameObject.SetActive(false);
            button_N.gameObject.SetActive(true);
        }
    }
}
