using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour
{
    private GameObject[] Circle;
    public GameObject StartPosition;
    public Button button_S;
    public Button button_RS;
    public Button button_M;
    public Button button_X;
    public Button button_E;
    public Button button_T;
    public Button button_N;
    public Text BallNum;
    public GameObject panel;
    public Text text;
    public GameObject cameraZ;
    public bool startFlag = true;
    private int BallCounter;
   // protected GameObject Circle_R;
    protected GameObject[] Line;
    protected GameObject[] Collider;
    private MoveCamera script1;
    private DrawLine2D script2;
    void Start()
    {
        button_S.onClick.AddListener(Button_S);
        button_RS.onClick.AddListener(Button_RS);
        button_M.onClick.AddListener(Button_M);
        button_X.onClick.AddListener(Button_X);
        button_E.onClick.AddListener(Button_E);
        button_T.onClick.AddListener(Button_T);
        button_N.onClick.AddListener(Button_N);
        script1 = GameObject.Find("Main Camera").GetComponent<MoveCamera>();
        script2 = GameObject.Find("GameObject").GetComponent<DrawLine2D>();
        Circle = GameObject.FindGameObjectsWithTag("Ball");
        BallCounter = Circle.Length-1;
        if (BallNum != null)
        {
            BallNum.text = (BallCounter+1).ToString();
        }
    }
    void Button_N()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        button_N.gameObject.SetActive(false);
        button_T.gameObject.SetActive(true);       
        System.GC.Collect();

    }
    void Button_S()
    {
        if (startFlag) {
           // Circle[BallCounter].gameObject.SetActive(true);
            Circle[BallCounter].GetComponent<Rigidbody2D>().simulated = true;
            cameraZ.transform.position =new Vector3(Circle[BallCounter].transform.position.x, 0,-10);
            startFlag = false;
            script1.ballNum = BallCounter;
            BallCounter--;
        }
        else if (!script1.followFlag && BallCounter >= 0)
        {
            Circle[BallCounter].gameObject.SetActive(true);
            Circle[BallCounter].GetComponent<Rigidbody2D>().simulated = true;
            cameraZ.transform.position = new Vector3(Circle[BallCounter].transform.position.x, 0, -10);
            script1.ballNum = BallCounter;
            BallCounter--;
            script1.followFlag = true;
        }
        if(BallNum != null)
        {
            BallNum.text = (BallCounter + 1).ToString();
        }
    }
    void Button_M()
    {
        text.text = "Menu";
        button_X.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        script2.isMenuActive = true;
    }
    void Button_X()
    {
        panel.gameObject.SetActive(false);
        script2.isMenuActive = false;
    }
    void Button_E()
    {
        
        SceneManager.LoadScene("MainPage", LoadSceneMode.Single);
        System.GC.Collect();
    }
    void Button_T()
    {
        /* //Circle_R = GameObject.Find("Circle");
         Line = GameObject.FindGameObjectsWithTag("LineP");
         Collider = GameObject.FindGameObjectsWithTag("Collider");

         if (Circle != null)
         {
             Circle.gameObject.SetActive(false);
             Circle.transform.position = StartPosition.transform.position;
         }
             //Destroy(Circle.gameObject);
         if (Line != null)
         {
             foreach (GameObject ob in Line)
             {
                 Destroy(ob.gameObject);
             }
         }
         if (Collider != null)
         {
             foreach (GameObject ob in Collider)
             {
                 Destroy(ob.gameObject);
             }
         }
          System.GC.Collect();
         Resources.UnloadUnusedAssets();
         Vector3 cameraSet;
         cameraSet.x = 0;
         cameraSet.y = 0;
         cameraSet.z = -10;
         cameraZ.transform.position = cameraSet;
         panel.gameObject.SetActive(false);*/       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        System.GC.Collect();
        script2.isMenuActive = false;
        startFlag = true;
    }
    void Button_RS()
    {
        if (startFlag)
        {
               Line = GameObject.FindGameObjectsWithTag("Line");
            
            int index;
            if (Line != null)
            {
                index = Line.Length - 1;
                if (index >= 0)
                {
                    if (Line[index] != null)
                    {
                        //Destroy(Line[index].gameObject);
                        Line[index].gameObject.SetActive(false);
                    }
                }
            }
            
        }
        

    }
}



