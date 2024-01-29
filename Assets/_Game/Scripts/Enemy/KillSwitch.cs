using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillSwitch : MonoBehaviour
{
    public GameObject player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            var x = SceneManager.GetActiveScene();
            SceneManager.LoadScene(x.name);
        }
    }
}
