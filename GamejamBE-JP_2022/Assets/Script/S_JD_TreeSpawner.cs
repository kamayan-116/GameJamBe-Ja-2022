using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_TreeSpawner : MonoBehaviour
{
    public bool InGrowMode = true;
    public GameObject[] TreeList;
    public GameObject GrowTree;
    private GameObject ChildTree;
    public float[] RandomTimeToSpawn = { 2f, 3f };
    void Start()
    {
        if(InGrowMode)
        {
            ChildTree = Instantiate(GrowTree, gameObject.transform.position, gameObject.transform.rotation);
            ChildTree.GetComponent<S_JD_GrowTree>().SetParent(gameObject);
        }
        else
        {
            ChildTree = Instantiate(TreeList[Random.Range(0, TreeList.Length)], gameObject.transform.position, gameObject.transform.rotation);
            ChildTree.GetComponent<S_JD_Tree>().SetParent(gameObject);
        }
    }

    private void GrowingTree()
    {
        ChildTree = Instantiate(GrowTree, gameObject.transform.position, gameObject.transform.rotation);
        ChildTree.GetComponent<S_JD_GrowTree>().SetParent(gameObject);
    }

    public IEnumerator LatenceGrowingTree()
    {
        float _Waitingtime = Random.Range(RandomTimeToSpawn[0], RandomTimeToSpawn[1]);
        GrowingTree();
        yield return new WaitForSeconds(_Waitingtime);
    }
}
