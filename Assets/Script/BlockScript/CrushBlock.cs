using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushBlock : MonoBehaviour {
    private float hp;
    public GameObject particle;
    public Material crushMaterial;
    void Start()
    {
        hp = 10;
        particle.transform.position = gameObject.transform.position;
        particle.transform.rotation = gameObject.transform.rotation;
        particle.transform.localScale = gameObject.transform.localScale;     
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (gameObject.layer == LayerMask.NameToLayer("CrushBlock"))
            {
                hp -= collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody2D>().mass * 8f;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("FallingBlock"))
            {
                hp -= collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody2D>().mass * 5f;
            }
            //Debug.Log(gameObject.layer+"  "+hp);
            if(hp <3)
                gameObject.GetComponent<MeshRenderer>().material = crushMaterial;
            if (hp <= 0)
            {
                Instantiate(particle, gameObject.transform.position , gameObject.transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
