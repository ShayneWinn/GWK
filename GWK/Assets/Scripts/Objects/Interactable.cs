using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public UnityEvent ToCall;
    public string InteractMessage = "Press 'e' to open";
    public Text InteractObject;

    public void Interact()
    {
        ToCall.Invoke();
    }

    public void Prompt()
    {
        InteractObject.text = InteractMessage;
    }

    public void UnPrompt()
    {
        InteractObject.text = "";
    }
}