using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);

    }

    public override void OnLoseFocus()
    {

    }
}
