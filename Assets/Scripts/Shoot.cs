using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float bulletSpeed;
    public Rigidbody2D b_rb;

    void Update()
    {
        b_rb.AddForce(new Vector2(bulletSpeed, 0));
        Destroy(gameObject, 5);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ghost")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }  
    }

}
