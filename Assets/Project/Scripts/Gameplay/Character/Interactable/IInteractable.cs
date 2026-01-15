using UnityEngine;

public interface IInteractable
{
    int Priority { get; }
    void Interact(GameObject interactor);
}