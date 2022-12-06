using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGlider : MonoBehaviour
{
    private bool ActiveGrinder;

    private void Start()
    {
        ActiveGrinder = true;
        StartCoroutine(activeBool());
    }
    private IEnumerator activeBool()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0025f);
            ActiveGrinder = true;
        }
    }
    private void OnTriggerEnter(Collider сollision)
    {
        if(сollision.gameObject.CompareTag("BoxMesh"))
        {
            if(ActiveGrinder == true)
            {
                ActiveGrinder = false;
                crushObject(сollision.gameObject);
            }
            //crushObject(сollision.gameObject);
        }
    }

    private GameObject crushObject(GameObject newCrushCube)
    {
        GameObject newBox = newCrushCube;
        newBox.tag = "FinishBoxCollider";
        newBox.layer = 11;
        newBox.gameObject.AddComponent<Rigidbody>();
        newBox.gameObject.GetComponent<Rigidbody>().mass = 10;
        newBox.gameObject.transform.SetParent(null);
        
        return null;
    }
}
