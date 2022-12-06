using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using MVoxelizer;
using TMPro;

public class MeshBuilder : MonoBehaviour
{
    public GameObject[] meshObject;
    public GameObject bagObject;

    [SerializeField] private Material newMatirial;
    public int countBox;
    private bool gateActive;
    private GameObject[] newArray;
    private int NewCountArray;

    private void Start()
    {
        
    }
    public void DetectedLenght()
    {
        DOTween.SetTweensCapacity(1000, 700);
        meshObject = new GameObject[bagObject.transform.childCount];

        for(int i = 0; i < meshObject.Length; i++)
        {
            meshObject[i] = bagObject.transform.GetChild(i).gameObject;
        }

        Array.Sort<GameObject>(meshObject, new Comparison<GameObject>((i1, i2) => i1.transform.position.y.CompareTo(i2.transform.position.y)));
        countBox = 0;
        gateActive = true;
    }
    public GameObject builObject(GameObject newObject)
    {
        Sequence sequence = DOTween.Sequence();
        for(int b = 0; b < meshObject.Length; b++)
        {
            if(meshObject[b].transform.childCount != 1)
            {
                countBox++;
                GameObject flyBox = newObject;
                GameObject pp = Instantiate(meshObject[b], flyBox.transform.position, flyBox.transform.rotation);
                float localScaleObject = bagObject.transform.localScale.x;
                pp.transform.localScale = new Vector3(localScaleObject,localScaleObject,localScaleObject);
                pp.gameObject.GetComponent<MeshRenderer>().enabled = true;
                pp.gameObject.AddComponent<MeshCollider>().convex = true;
                pp.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                Destroy(pp.gameObject.GetComponent<Voxel>());
                pp.gameObject.layer = 7;
                
                pp.tag = "BoxMesh";
                Destroy(flyBox.gameObject);
                //flyBox.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //Destroy(flyBox.gameObject.GetComponent<BoxCollider>());
                float localPositionY = meshObject[b].transform.position.y / 3f;
                pp.transform.SetParent(meshObject[b].transform);
                sequence.Append(pp.transform.DOLocalJump(new Vector3 (0,0,0), localPositionY, 1, 1f,false))
                .OnComplete(()=> pp.transform.localRotation = Quaternion.Euler(0,0,0));
                b = meshObject.Length + 1;
            }
        }
        return null;
    }
    public void BuildBox(int bBoxInt, GameObject positionGate)
    {
        if(gateActive == true)
        {
            gateActive = false;
            for(int b = 0; b < bBoxInt; b++)
            {
                gateBuilBox(positionGate);
            }
            Invoke("invokeGateActive", 1f);
        }
        
    }
    public GameObject gateBuilBox(GameObject newBuildBox)
    {
        Sequence sequence = DOTween.Sequence();
        for(int g = 0; g < meshObject.Length; g++)
        {
            if(meshObject[g].transform.childCount != 1)
            {
                countBox++;
                GameObject newBoxObject = Instantiate(meshObject[g], newBuildBox.transform.position, newBuildBox.transform.rotation);
                float localScaleObject = bagObject.transform.localScale.x;
                newBoxObject.transform.localScale = new Vector3(localScaleObject,localScaleObject,localScaleObject);
                newBoxObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
                newBoxObject.gameObject.AddComponent<MeshCollider>().convex = true;
                newBoxObject.gameObject.GetComponent<MeshCollider>().isTrigger = true;
                Destroy(newBoxObject.gameObject.GetComponent<Voxel>());
                newBoxObject.gameObject.layer = 7;
                
                newBoxObject.tag = "BoxMesh";
                float localPositionY = meshObject[g].transform.position.y / 6f;
                newBoxObject.transform.SetParent(meshObject[g].transform);
                sequence.Append(newBoxObject.transform.DOLocalJump(new Vector3 (0,0,0), localPositionY, 1, 1f,false))
                .OnComplete(()=> newBoxObject.transform.localRotation = Quaternion.Euler(0,0,0));
                g = meshObject.Length + 1;
            }
        }
        return null;
    }
    
    public void destroyBox(int BoxMinus)
    {
        if(gateActive == true)
        {
            gateActive = false;
            newArray = null;
            newArray = new GameObject[meshObject.Length];
            for(int d = 0; d < meshObject.Length; d++)
            {
                if(meshObject[d].transform.childCount >= 1)
                {
                    newArray[d] = meshObject[d].transform.GetChild(0).gameObject;
                }
            }
            SortArray();
            Array.Sort<GameObject>(newArray, new Comparison<GameObject>((d1, d2) => d2.transform.position.y.CompareTo(d1.transform.position.y)));

            if(BoxMinus > newArray.Length)
            {
                NewCountArray = newArray.Length;
            }
            else if(BoxMinus <= newArray.Length)
            {
                NewCountArray = BoxMinus;
            }
            for(int b = 0; b < NewCountArray; b++)
            {
                countBox--;
                GameObject newDestroy = newArray[b];
                newDestroy.tag = "Untagged";
                newDestroy.gameObject.layer = 0;
                newDestroy.gameObject.AddComponent<Rigidbody>().AddForce(new Vector3(0,6,0), ForceMode.Impulse);
                newDestroy.gameObject.GetComponent<Rigidbody>().mass = 1;
                newDestroy.gameObject.GetComponent<MeshCollider>().isTrigger = false;
                newDestroy.gameObject.transform.SetParent(null);
                Destroy(newDestroy.gameObject, 2f);
                newArray[b] = null;
            }
            newArray = null;
            Invoke("invokeGateActive", 1f);
        }
    }
    private void invokeGateActive()
    {
        gateActive = true;
    }
    private void SortArray()
    {
        List<GameObject> gameObjectList = new List<GameObject>(newArray);
        gameObjectList.RemoveAll(x => x == null);
        newArray = gameObjectList.ToArray();
        Debug.Log(newArray.Length + "sort");
    }
}
