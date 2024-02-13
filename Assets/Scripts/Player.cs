using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public Text info;
    public float moveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Game.gameOver = false;
        Game.win = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Game.gameOver)
        {
            //info.text = "Game Over";
            return;
        }

        if(Game.win)
        {

            return;
            
        }

        float moveAlongXAxis = 0f;

        if(Input.GetKey(KeyCode.A))
        {
            moveAlongXAxis += -moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveAlongXAxis += -moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveAlongXAxis += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveAlongXAxis += moveSpeed * Time.deltaTime;
        }

        rb.MovePosition(rb.position + new Vector3(moveAlongXAxis, 0f, 0f));

        Ball[] enemiesInScene = FindObjectsOfType<Ball>();
        
        if(enemiesInScene==null)
        {
            Game.win = true;

            return;
        }

        if(enemiesInScene.Length<1)
        {
            Game.win = true;

            return;
        }
    }
}
