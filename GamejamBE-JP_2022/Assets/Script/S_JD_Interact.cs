using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_Interact : MonoBehaviour
{
    private bool canBeActivate = false;
    public string interactionType = "GetWater";

    void Update()
    {
        if (canBeActivate && Input.GetButtonDown("Interact"))
        {
            S_JD_Player.Instance.Interact(interactionType);
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
        print("exit");
        if (other.tag == "Player")
        {
            canBeActivate = false;
        }
    }

    private void Interaction()
    {

    }
}