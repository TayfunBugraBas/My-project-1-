using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessageManager : MonoBehaviour
{
    public GameObject popupPanel;
    private bool isWorked ;

    void Start()
    {
        // Ba�lang��ta pop-up panelini gizle
        popupPanel.SetActive(false);
        isWorked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Trigger b�lgesine giri� yap�ld���nda �a�r�lacak metot
        if (other.CompareTag("Player") && isWorked == false) 
        {
            ShowPopup();
            isWorked = true;
        }
    }

    public void ShowPopup()
    {
        // Pop-up panelini g�ster
        popupPanel.SetActive(true);

       
        Invoke("ClosePopup",1.2f); 
    }

    public void ClosePopup()
    {
        // Pop-up panelini kapat
        popupPanel.SetActive(false);
    }
}
