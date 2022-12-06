using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFinish : MonoBehaviour
{
    private bool activeCube;

    private void OnCollisionEnter(Collision сollision)
    {
        if(сollision.gameObject.CompareTag("Saw") && activeCube == false)
        {
            activeCube = true;
            gameObject.tag = "FinishBoxCollider";
            gameObject.layer = 11;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            gameObject.GetComponent<Rigidbody>().mass = 10;
            //gameObject.GetComponent<MeshCollider>().isTrigger = false;
            Destroy(gameObject.GetComponent<CubeFinish>());
            //Destroy(gameObject.GetComponent<MeshCollider>());
            gameObject.transform.SetParent(null);
        }
    }
}
