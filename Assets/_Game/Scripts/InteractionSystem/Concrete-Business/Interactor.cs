using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
  [SerializeField] private Transform _interactionPoint;
  [SerializeField] private float _interactionPointRadius;
  [SerializeField] private LayerMask _interactableMask;
  [SerializeField] private InteractionPromptUI _interactionPrompt;  

  private readonly Collider[] _colliders = new Collider[3];
  [SerializeField] private int _numFound;

  private IInteractable _interactable;

 /// <summary>
/// Update is called every frame, if the MonoBehaviour is enabled.
/// </summary>
    private void Update()
    {

        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position,_interactionPointRadius,_colliders,_interactableMask);

        if(_numFound > 0){
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if(_interactable != null){

                if(!_interactionPrompt.IsDisplay) _interactionPrompt.SetUp(_interactable.InteractionPrompt);

                if(Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this,this.gameObject);
               

            }

        }
        else{
            if(_interactable != null) _interactable = null;
            if(_interactionPrompt.IsDisplay) _interactionPrompt.Close();
        }


    }


     /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position,_interactionPointRadius);
    }


   
}
