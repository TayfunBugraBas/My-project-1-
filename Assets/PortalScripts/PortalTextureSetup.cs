using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public List<Camera> cameras;

    public List<Material> cameraMats;



    void Start()
    {
        for(int y = 0; y < cameras.Count; y++ ){
        if(cameras[y].targetTexture != null){

             cameras[y].targetTexture.Release();

        }
        cameras[y].targetTexture = new RenderTexture(Screen.width,Screen.height,24);
        cameraMats[y].mainTexture = cameras[y].targetTexture;
        }
    }

    
   
}
