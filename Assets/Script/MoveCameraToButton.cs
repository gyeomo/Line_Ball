using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToButton : MonoBehaviour {
    public GameObject cameraZ;
    int speed = 5;
    bool a, b;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if(a) cameraZ.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * 1, Space.World);
        if(b) cameraZ.transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * -1, Space.World);
    }
    public void Up()
    {
        a = false;
    }
    public void Down()
    {
        a = true;
    }
    public void BackUp()
    {
        b = false;
    }
    public void BackDown()
    {
        b = true;
    }
}
