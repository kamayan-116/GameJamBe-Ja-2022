using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_WaterCollectPoint : S_JD_Interact
{
    public float TimeBetweenCollect = 2f;
    //public float TimeOfCollect = 2f;

    private void Start()
    {
        interactionType = "GetWater";
    }
    protected override void Interaction()
    {
        if (S_JD_Player.Instance.WaterValue < 5)
        {
            StartCoroutine(LatenceWaterGathering());
            //ReadyToCollect = false;
        }            
        //StartCoroutine(Latence());
    }

    IEnumerator Latence()
    {
        yield return new WaitForSeconds(TimeBetweenCollect);
        ReadyToCollect = true;
    }

    IEnumerator LatenceWaterGathering()
    {
        S_JD_CanvasManager.Instance.SetInactivePressE();
        S_JD_Player.Instance.AvailableMouvement = false;
        if (S_JD_Player.Instance.WaterValue < 5 && !S_JD_Player.Instance.RecoltWater)
        {
            S_JD_Player.Instance.RecoltWater = true;
            if (S_JD_Player.Instance.elementType == S_JD_Player.ElementType.Water)
            {
                S_JD_Player.Instance.WaterValue += 2;
                S_JD_CanvasManager.Instance.SetValueWater(S_JD_Player.Instance.WaterValue);
                S_JD_Player.Instance.PlayHeartParticle();
            }
            else
            {
                S_JD_Player.Instance.WaterValue += 1;
                S_JD_CanvasManager.Instance.SetValueWater(S_JD_Player.Instance.WaterValue);
            }
        }
        if (S_JD_Player.Instance.WaterValue > 5)
        {
            S_JD_Player.Instance.WaterValue = 5;
            S_JD_CanvasManager.Instance.SetValueWater(S_JD_Player.Instance.WaterValue);
        }
        yield return new WaitForSeconds(TimeBetweenCollect);
        S_JD_Player.Instance.AvailableMouvement = true;
        S_JD_CanvasManager.Instance.SetActivePressE();
        S_JD_Player.Instance.RecoltWater = false;
        //ReadyToCollect = true;
        //S_JD_GameManager.Instance.TreeNumber -= 1;
        //StartCoroutine(DeadTimer());
    }
}
