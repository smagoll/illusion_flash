using UnityEngine;

public class Campfire : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int priority;
    
    public int Priority => priority;
    
    public void Interact(GameObject interactor)
    {
        Debug.Log("Костёр активирован");
    }
}