using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0f, -speed*10f, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Ground")
        {
            Game.gameOver = true;
        }
        else if(collision.gameObject.name=="Player")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
