using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeScript : MonoBehaviour
{
    [SerializeField] private Vector3 b_NewForce;
    private MeshBuilder meshScript;
    private GameObject mainControllObject;
    [SerializeField] private GameObject _animation;

    private void Start()
    {
        mainControllObject = GameObject.FindWithTag("Player");
        meshScript = mainControllObject.gameObject.GetComponent<MeshBuilder>();
        _animation.GetComponent<Animator>().enabled = false;
        float randTime = Random.Range(0f,1.5f);
        if(randTime >= 0f && randTime < 0.5f)
        {
            randTime = 0.5f;
        }
        else if(randTime >= 0.5f && randTime < 1f)
        {
            randTime = 1f;
        }
        else if(randTime >= 1f && randTime <= 1.5f)
        {
            randTime = 1.5f;
        }
        Invoke("ActiveAnimator", randTime);
    }
    private void ActiveAnimator()
    {
        _animation.GetComponent<Animator>().enabled = true;
    }
    private void OnTriggerEnter(Collider sawBladeObject)
    {
        if(sawBladeObject.gameObject.CompareTag("BoxMesh"))
        {
            crushObject(sawBladeObject.gameObject);
        }
    }
    public GameObject crushObject(GameObject newCrushCube)
    {
        meshScript.countBox--;
        GameObject newBox = newCrushCube;
        newBox.tag = "Untagged";
        newBox.gameObject.layer = 0;
        newBox.gameObject.AddComponent<Rigidbody>().AddForce(b_NewForce, ForceMode.Impulse);
        newBox.gameObject.GetComponent<Rigidbody>().mass = 3;
        newBox.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        newBox.gameObject.transform.SetParent(null);
        Destroy(newBox.gameObject, 2f);
        return null;
    }
}
