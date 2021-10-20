using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Transform gameManager;
    public Transform gameCamera;

    public float speed;

    public float depth = 3;

    Vector3 relativeposition;

    Rigidbody2D rigidBody;
    GameManager gameManagerC;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        gameManagerC = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManagerC.IsShowingDialog()) { return; }

        float debugPreviousSpeed = 0;
        if (Switches.debugMode && Switches.debugTurboMode)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                debugPreviousSpeed = speed;
                speed = speed * 20;
            }
        }

        Vector3 rp = relativeposition;

        if (Input.GetKey(KeyCode.W))
        {
            rp = rp + Vector3.up * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S))
        {
            rp = rp - Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rp = rp + Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rp = rp - Vector3.right * speed * Time.deltaTime;
        }
        rp = new Vector3(rp.x, rp.y, depth);

        relativeposition = rp;

        //transform.position = gameCamera.TransformPoint(relativeposition);

        Vector3 p = gameCamera.TransformPoint(relativeposition);
        rigidBody.MovePosition(p);

        #if __DEBUG_AVAILABLE__
        if (Switches.debugMode && Switches.debugTurboMode)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                speed = debugPreviousSpeed;
            }
        }
        #endif
        
    }
}
