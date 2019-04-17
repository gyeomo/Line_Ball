using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
    public GameObject Circle;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            float circleVelocity = collision.relativeVelocity.magnitude;
         //   Debug.Log("충돌");
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            Vector2 incomingVector = -(collision.gameObject.transform.position - gameObject.transform.position);           
            gameObject.GetComponent<Rigidbody2D>().AddForce(incomingVector , ForceMode2D.Impulse);
            Circle.GetComponent<Rigidbody2D>().AddForce(incomingVector * circleVelocity * 5f);
            
        }

    }
}
