using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_CanvasManager : MonoBehaviour
{
    public Animator DeathFade;
    public void DeathFading()
    {
        DeathFade.SetTrigger("DeathTrigger");
    }
}
