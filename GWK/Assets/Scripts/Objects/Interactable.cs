using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent ToCall;

    public void Interact()
    {
        ToCall.Invoke();
    }
}