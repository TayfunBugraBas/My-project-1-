using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessageManager : MonoBehaviour
{
    public GameObject popupPanel;
    private bool isWorked ;

    void Start()
    {
        // Baþlangýçta pop-up panelini gizle
        popupPanel.SetActive(false);
        isWorked = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Trigger bölgesine giriþ yapýldýðýnda çaðrýlacak metot
        if (other.CompareTag("Player") && isWorked == false) 
        {
            ShowPopup();
            isWorked = true;
        }
    }

    public void ShowPopup()
    {
        // Pop-up panelini göster
        popupPanel.SetActive(true);

       
        Invoke("ClosePopup",1.2f); 
    }

    public void ClosePopup()
    {
        // Pop-up panelini kapat
        popupPanel.SetActive(false);
    }
}
