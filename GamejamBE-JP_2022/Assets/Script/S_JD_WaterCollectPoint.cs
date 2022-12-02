using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_WaterCollectPoint : S_JD_Interact
{
    protected override void Interaction()
    {
        print("Water = " + S_JD_Player.Instance.WaterValue);
    }
}
