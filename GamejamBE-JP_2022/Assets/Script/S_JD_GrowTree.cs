using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_GrowTree : S_JD_Interact
{
    private int WaterValue = 0;
    public GameObject[] TreeList;
    private bool CanBeActivate = true;
    private GameObject Parent;
    private GameObject Tree;

    private void Start()
    {
        interactionType = "GiveWater";
    }
    protected override void Interaction()
    {
        if (CanBeActivate && S_JD_Player.Instance.WaterValue > 0)
        {
            S_JD_Player.Instance.WaterValue -= 1;
            WaterValue += 1;
            S_JD_CanvasManager.Instance.SetValueWater(S_JD_Player.Instance.WaterValue);           
            if (WaterValue == 2)
            {
                Tree = Instantiate(TreeList[Random.Range(0, TreeList.Length)], gameObject.transform.position, gameObject.transform.rotation);
                Tree.GetComponent<S_JD_Tree>().SetParent(Parent);
                Destroy(gameObject);
            }
            StartCoroutine(LatenceBetweenWater());
        }
    }

    IEnumerator LatenceBetweenWater()
    {
        CanBeActivate = false;
        yield return new WaitForSeconds(2f);
        CanBeActivate = true;
    }
    public void SetParent(GameObject _parent)
    {
        Parent = _parent;
    }
}
