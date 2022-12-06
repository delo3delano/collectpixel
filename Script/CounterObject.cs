using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CounterObject : MonoBehaviour
{
    public GameObject[] finishObject,newAraay;

    private void Start()
    {
        
        finishObject = new GameObject[gameObject.transform.childCount];

        for(int i = 0; i < finishObject.Length; i++)
        {
            finishObject[i] = gameObject.transform.GetChild(i).gameObject;
        }
        for(int r = 0; r < finishObject.Length; r++)
        {
            if(finishObject[r].transform.childCount != 1)
            {
                Destroy(finishObject[r].gameObject);
                finishObject[r] = null;
            }
        }
        ResearchArray();
    }

    private void ResearchArray()
    {
        List<GameObject> gameObjectList = new List<GameObject>(finishObject);
        gameObjectList.RemoveAll(x => x == null);
        finishObject = gameObjectList.ToArray();
        newAraay = new GameObject[finishObject.Length];
        for(int n = 0; n < newAraay.Length; n++)
        {
            newAraay[n] = finishObject[n];
            newGameObject(newAraay[n].transform.GetChild(0).gameObject);
            Destroy(newAraay[n].gameObject);
        }
    }
    private GameObject newGameObject(GameObject objectArray)
    {
        GameObject myGameObject = objectArray;
        myGameObject.transform.parent = gameObject.transform;
        myGameObject.layer = 0;
        myGameObject.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        //myGameObject.gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //myGameObject.gameObject.AddComponent<SphereCollider>().isTrigger = false;
        //myGameObject.gameObject.AddComponent<CubeFinish>();
        return null;
    }
}
