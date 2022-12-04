using System.Collections;
using UnityEngine;

public class S_JD_Tree : S_JD_Interact
{
    public Animator UpTreeAnim;
    public GameObject UpTree;
    public GameObject BottomTree;
    public float TimeOfCuttingWood = 2f;

    private GameObject Parent;

    private void Start()
    {
        interactionType = "GetWood";
        S_JD_GameManager.Instance.TreeNumber += 1;
    }

    protected override void Interaction()
    {
        StartCoroutine(LatenceWoodCutting());
        ReadyToCollect = false;
    }

    IEnumerator LatenceWoodCutting()
    {
        S_JD_Player.Instance.CutMode = true;
        S_JD_CanvasManager.Instance.SetInactivePressE();
        S_JD_Player.Instance.AvailableMouvement = false;
        yield return new WaitForSeconds(TimeOfCuttingWood);
        UpTreeAnim.SetTrigger("Fall");
        S_JD_Player.Instance.CutMode = false;
        S_JD_Player.Instance.AvailableMouvement = true;
        S_JD_GameManager.Instance.TreeNumber -= 1;
        StartCoroutine(DeadTimer());
    }

    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(10f);
        StartCoroutine(Parent.GetComponent<S_JD_TreeSpawner>().LatenceGrowingTree());
        Destroy(gameObject);
    }

    public void SetParent(GameObject _parent)
    {
        Parent = _parent;
    }

}
