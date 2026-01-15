using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int priority;
    [SerializeField]
    private GameObject fire;

    private bool isActivated;
    private bool isInteractable;
    
    public int Priority => priority;
    public bool CanInteractable => isInteractable;
    
    public void Interact(GameObject interactor)
    {
        if (!isActivated) Activate();
    }
    
    private void Activate()
    {
        isActivated = true;
        
        fire.SetActive(true);
        
        Debug.Log("Костёр активирован");
    }
}