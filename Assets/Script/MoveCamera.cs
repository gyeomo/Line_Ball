using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    private Transform tr;
    Vector3 tartgetPosition;
    public GameObject[] Circle;
    public bool followFlag;
    public int ballNum;
    private ButtonScript script1;
    void Start()
    {
        followFlag = true;
        tr = GetComponent<Transform>();
        script1 = GameObject.Find("ButtonManager").GetComponent<ButtonScript>();
        Circle = GameObject.FindGameObjectsWithTag("Ball");

    }
    private void LateUpdate()
    {     
        if (!script1.startFlag && followFlag)
        {
            tartgetPosition = Circle[ballNum].transform.position;
            tartgetPosition.y = 0;
            tartgetPosition.z = -10;
            tr.transform.position = Vector3.Slerp(tr.transform.position, tartgetPosition,2f * Time.deltaTime);
            if (Circle[ballNum].GetComponent<Rigidbody2D>().velocity.x == 0 && Circle[ballNum].GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                followFlag = false;
            }            
        }
        else if (Circle[ballNum].GetComponent<Rigidbody2D>().velocity.x != 0 && Circle[ballNum].GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            followFlag = true;
        }
    }

}
