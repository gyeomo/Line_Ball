using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour {

    public GameObject Circle;
    private string tagString = "LineJ";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(tagString))
        {
            float massRatio = Circle.gameObject.GetComponent<Rigidbody2D>().mass/0.1f;
            float circleVelocity = collision.relativeVelocity.magnitude* massRatio;
            //   Debug.Log(circleVelocity);
            // 입사벡터를 알아본다. (충돌할때 충돌한 물체의 입사 벡터 노말값)
            Vector2 collVector2 = new Vector2(Circle.transform.position.x, Circle.transform.position.y);
            Vector2 incomingVector = collision.contacts[0].point - collVector2;
            incomingVector = incomingVector.normalized;
            // 충돌한 면의 법선 벡터를 구해낸다.
            Vector2 normalVector = collision.contacts[0].normal;
            // 법선 벡터와 입사벡터을 이용하여 반사벡터를 알아낸다.
            Vector2 reflectVector = Vector2.Reflect(incomingVector, normalVector); //반사각
            reflectVector = reflectVector.normalized;
            Circle.GetComponent<Rigidbody2D>().AddForce(reflectVector * circleVelocity * 5f);
           // Debug.Log("Jump!");
        }
    }
}
