using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_WaterCollectPoint : S_JD_Interact
{
    public float TimeBetweenCollect = 2f;

    private void Start()
    {
        interactionType = "GetWater";
    }
    protected override void Interaction()
    {
        ReadyToCollect = false;
        StartCoroutine(Latence());
    }

    IEnumerator Latence()
    {
        yield return new WaitForSeconds(TimeBetweenCollect);
        ReadyToCollect = true;
    }
}
