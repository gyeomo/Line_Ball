using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour {
    public GameObject panel;
    public Button button_X;
    public Button button_T;
    public Button button_N;
    public Text text;
    private DrawLine2D script2;
    // Use this for initialization
    void Start()
    {
        script2 = GameObject.Find("GameObject").GetComponent<DrawLine2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Circle1"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            FinishGame();
        }
    }
    public void FinishGame()
    {
         text.text = "Failure";
         panel.gameObject.SetActive(true);
         button_X.gameObject.SetActive(false);
         script2.isMenuActive = true;           
    }
}
