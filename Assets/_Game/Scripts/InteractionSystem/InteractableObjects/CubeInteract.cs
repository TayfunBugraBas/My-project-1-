using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor,GameObject gameObject)
    {
      Debug.Log(_prompt);
      Destroy(gameObject);
      return true;
    }
}
