using UnityEngine;

public interface IInteractable
{
    int Priority { get; }
    bool CanInteractable { get; }
    void Interact(GameObject interactor);
}