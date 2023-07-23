using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;
    private bool isOverLap = false;


   

    // Update is called once per frame
    void Update()
    {
        if(isOverLap){

           Vector3 portToPly = player.position - transform.position;
          float dotProduct = Vector3.Dot(transform.up,portToPly);
            if(dotProduct <0f){

                float rotationDiff = Quaternion.Angle(transform.rotation,receiver.rotation);
                rotationDiff +=180;
                player.Rotate(Vector3.up,rotationDiff);
                Vector3 positionOffset = Quaternion.Euler(0f,rotationDiff,0f)*portToPly;
                player.position = receiver.position + positionOffset;
            
                isOverLap = false;
            }

        }
        
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Debug.Log("triggered");
          isOverLap = true;
        }
    }
    void OnTriggerExit(Collider other){

           if(other.tag == "Player"){

                isOverLap = false;

           }

    }  
}
