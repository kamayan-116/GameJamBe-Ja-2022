using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JD_TreeSpawner : MonoBehaviour
{
    public bool InGrowMode = true;
    public GameObject[] TreeList;
    public GameObject GrowTree;
    private GameObject ChildTree;
    public float[] RandomTimeToSpawn = { 10f, 45f };
    void Start()
    {
        if(InGrowMode)
        {
            Instantiate(GrowTree, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            ChildTree = Instantiate(TreeList[Random.Range(0, TreeList.Length)]);
            ChildTree.GetComponent<S_JD_Tree>().SetParent(gameObject);
        }
    }

    private void GrowingTree()
    {
        Instantiate(GrowTree, gameObject.transform.position, Quaternion.identity);

    }

    public IEnumerator LatenceGrowingTree()
    {
        yield return new WaitForSeconds(Random.Range(RandomTimeToSpawn[0], RandomTimeToSpawn[1]));
        GrowingTree();
    }
}
