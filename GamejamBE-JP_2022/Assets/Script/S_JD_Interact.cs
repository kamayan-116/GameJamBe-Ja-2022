using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 親クラス
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
            if (ReadyToCollect)
                S_JD_CanvasManager.Instance.SetActivePressE();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canBeActivate = false;
            S_JD_CanvasManager.Instance.SetInactivePressE();
        }
    }

    protected virtual void Interaction()
    {

    }
}