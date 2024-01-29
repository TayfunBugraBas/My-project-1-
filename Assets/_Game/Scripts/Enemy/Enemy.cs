using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    bool lookAt;
    public GameObject player;
    Rigidbody rb;
    public float speed = 10f;
    public float multiplier = 10f;
    public float speed_limit = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerDetect.found)
        {
            lookAt = true;
        }
        Vector3 vel = rb.velocity;
        if (lookAt && vel.x > -2 && vel.x < 2 && vel.z > -2 && vel.z < 2 && vel.y > -2 && vel.y < 2)
        {
            transform.LookAt(player.transform);
           
            
                rb.AddForce(speed * multiplier * Time.deltaTime * transform.forward);
            
        }

    }
    
}
