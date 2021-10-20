using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if __DEBUG_AVAILABLE__
using UnityEditor;
#endif

public class Enemies : MonoBehaviour
{
    public Transform player;

    public float speed = 2;

    public float followSpeed = 0.2f;
    public float followDistance = 10;

    //Debug Mode
    Vector3 playerOffset;
    Vector3 playerOffsetPorjected;
    Vector3 playerOffsetNormalized;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    #if __DEBUG_AVAILABLE__
    private void OnDrawGizmos()
    {
        if (Switches.debugMode && Switches.debugShowIds)
        {
            Handles.Label(transform.position + new Vector3(0, 0.2f, 0), gameObject.name);
        }

        if (Switches.debugMode && Switches.debugShowEnemyFollowInfo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, followDistance);

            if (distance < followDistance)
            {
                Gizmos.DrawLine(transform.position, transform.position + playerOffset);
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + playerOffsetPorjected);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, transform.position + playerOffsetNormalized);
            }

            Handles.Label(transform.position + new Vector3(0, 0.8f, 0), " distance: " + distance);
        }
    }
    #endif

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.right * speed * Time.deltaTime;
        if(gameObject.name == "Enemy07")
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        playerOffset = player.position - transform.position;
        playerOffset = new Vector3(playerOffset.x, playerOffset.y, 0);

        distance = playerOffset.magnitude;

        if(distance < followDistance)
        {
            playerOffsetPorjected = new Vector3(0, playerOffset.y, 0);
            playerOffsetNormalized = playerOffset.normalized;
            transform.position += playerOffsetNormalized * followSpeed * Time.deltaTime;
        }
        
    }
}
