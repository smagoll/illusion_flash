using System;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    public event Action OnInteract;
    public event Action<string> OnEnterInteract;
    public event Action OnExitInteract;
    
    [SerializeField] private float interactionRadius = 2f;
    [SerializeField] private float scanInterval = 0.1f;
    
    private float timer;
    
    private IInteractable current;
    
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            FindInteractable();
            timer = scanInterval;
        }
    }
    
    private void FindInteractable()
    {
        current = null;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            interactionRadius
        );

        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out IInteractable interactable))
                continue;

            float dist = Vector3.Distance(
                transform.position,
                hit.transform.position
            );

            Vector3 dir = (hit.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, dir) < 0.5f)
                continue;
            
            if (dist < minDist)
            {
                minDist = dist;
                current = interactable;
            }
        }

        //TODO : сделать так, чтобы можно было устанавливать кнопку и текст
        if (current != null)
            OnEnterInteract?.Invoke("Взаимодействовать");
        else
            OnExitInteract?.Invoke();
    }

    public void TryInteract()
    {
        if (current != null)
        {
            current.Interact(gameObject);
        }
    }
}