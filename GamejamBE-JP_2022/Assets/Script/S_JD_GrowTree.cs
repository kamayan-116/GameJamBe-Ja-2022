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
    public Material[] midTreemat;

    public int typeoftree = 0;

    private void Start()
    {
        interactionType = "GiveWater";
        //typeoftree = Random.Range(0, 2);
    }
    protected override void Interaction()
    {
        if (CanBeActivate && S_JD_Player.Instance.WaterValue > 0)
        {
            S_JD_Player.Instance.WaterValue -= 1;
            WaterValue += 1;
            S_JD_CanvasManager.Instance.SetValueWater(S_JD_Player.Instance.WaterValue);
            StartCoroutine(LatenceBetweenWater());         
        }
    }

    IEnumerator LatenceBetweenWater()
    {
        CanBeActivate = false;
        S_JD_CanvasManager.Instance.SetInactivePressE();
        S_JD_Player.Instance.AvailableMouvement = false;
        yield return new WaitForSeconds(2f);
        CanBeActivate = true;
        S_JD_Player.Instance.AvailableMouvement = true;
        S_JD_CanvasManager.Instance.SetActivePressE();
        if (WaterValue == 2)
        {
            Tree = Instantiate(TreeList[typeoftree], gameObject.transform.position, gameObject.transform.rotation);
            Tree.GetComponent<S_JD_Tree>().SetParent(Parent);
            Destroy(gameObject);
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material = midTreemat[typeoftree];
            GetComponentInChildren<Transform>().localScale = new Vector3(3, 3, 3);
        }
    }
    public void SetParent(GameObject _parent, int _typeoftree)
    {
        Parent = _parent;
        typeoftree = _typeoftree;
    }
}
