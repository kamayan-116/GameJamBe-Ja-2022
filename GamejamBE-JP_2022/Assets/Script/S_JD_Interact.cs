using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Interact : MonoBehaviour
{
    private bool canBeActivate = false;
    [HideInInspector]
    public string interactionType = "GetWater";
    [HideInInspector]
    public bool ReadyToCollect = true;

    void Update()
    {
        if (canBeActivate && Input.GetButtonDown("Interact") && ReadyToCollect)
        {
            S_JD_Player.Instance.Interact(interactionType);
            Interaction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canBeActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canBeActivate = false;
        }
    }

    protected virtual void Interaction()
    {

    }
}