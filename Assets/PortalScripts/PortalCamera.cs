using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCam;
    public Transform Portal;
    public Transform RelPortal;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerOffset = playerCam.position - RelPortal.position;
        transform.position = Portal.position + playerOffset;

        float angDiff = Quaternion.Angle(Portal.rotation,RelPortal.rotation);

        Quaternion PortalRotDiff = Quaternion.AngleAxis(angDiff,Vector3.up);   
        Vector3 newCamDir = PortalRotDiff * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCamDir,Vector3.up);


    }
}
