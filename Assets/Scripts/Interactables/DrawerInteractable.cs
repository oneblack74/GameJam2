using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DrawerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 distanceToOpen;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private float duration = 1.0f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Coroutine currentCoroutine;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition - distanceToOpen;
    }

    public void Interact()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (isOpen)
        {
            currentCoroutine = StartCoroutine(CloseDrawer());
        }
        else
        {
            currentCoroutine = StartCoroutine(OpenDrawer());
        }
        isOpen = !isOpen;
    }

    private IEnumerator OpenDrawer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(closedPosition, openPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = openPosition;
    }

    private IEnumerator CloseDrawer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(openPosition, closedPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = closedPosition;
    }
}
