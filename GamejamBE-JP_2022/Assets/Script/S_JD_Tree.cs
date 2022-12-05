using System.Collections;
using UnityEngine;

public class S_JD_Tree : S_JD_Interact
{
    public Animator UpTreeAnim;
    public GameObject UpTree;
    public GameObject BottomTree;
    public float TimeOfCuttingWood = 2f;

    private GameObject Parent;

    public AudioSource FallingTree;

    private void Start()
    {
        interactionType = "GetWood";
        S_JD_GameManager.Instance.TreeNumber += 1;
    }

    protected override void Interaction()
    {
        if (S_JD_Player.Instance.WoodValue < 5)
        {
            StartCoroutine(LatenceWoodCutting());
            ReadyToCollect = false;
        } 
    }

    IEnumerator LatenceWoodCutting()
    {
        S_JD_Player.Instance.CutMode = true;
        S_JD_CanvasManager.Instance.SetInactivePressE();
        S_JD_Player.Instance.AvailableMouvement = false;
        if (S_JD_Player.Instance.WoodValue < 5)
        {

            if (S_JD_Player.Instance.elementType == S_JD_Player.ElementType.Wood)
            {
                S_JD_Player.Instance.WoodValue += 2;
                S_JD_CanvasManager.Instance.SetValueTree(S_JD_Player.Instance.WoodValue);
                S_JD_Player.Instance.PlayHeartParticle();
            }
            else
            {
                S_JD_Player.Instance.WoodValue += 1;
                S_JD_CanvasManager.Instance.SetValueTree(S_JD_Player.Instance.WoodValue);
            }
            S_JD_Player.Instance.CuttingWood.Play();
        }
        if (S_JD_Player.Instance.WoodValue > 5)
        {
            S_JD_Player.Instance.WoodValue = 5;
            S_JD_CanvasManager.Instance.SetValueTree(S_JD_Player.Instance.WoodValue);
        }
        yield return new WaitForSeconds(TimeOfCuttingWood);
        FallingTree.Play();
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
