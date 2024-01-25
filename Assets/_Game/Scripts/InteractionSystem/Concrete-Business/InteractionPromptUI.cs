using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InteractionPromptUI : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private TextMeshProUGUI _promtText;
     [SerializeField] private  GameObject uipanel;
   
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        _camera = Camera.main;
        uipanel.SetActive(false);
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        var rotation = _camera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    public bool IsDisplay = false;
    public void SetUp(string promtText){

        _promtText.text = promtText;
        uipanel.SetActive(true);
        IsDisplay = true;

    }
    public void Close (){
        uipanel.SetActive(false);
        IsDisplay = false;
    }

}
