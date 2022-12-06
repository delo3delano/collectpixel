using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishCount : MonoBehaviour
{
    [SerializeField] private Animator startTextAnimator;
    [SerializeField] private TMP_Text countFinishBoxText;
    public int CountFinishInt;
    private bool endL;
    private MCamera EndCameraScript;
    [SerializeField] private GameObject endCameraObject;
    [SerializeField] private GameObject countFactor;

    private void Start()
    {
        endL = true;
        CountFinishInt = 0;
        countFinishBoxText.text = CountFinishInt.ToString();
        EndCameraScript = endCameraObject.gameObject.GetComponent<MCamera>();
        countFactor.transform.Rotate(0f, 0f, 245f);
    }

    private void OnTriggerEnter(Collider finishBox)
    {
        if(finishBox.gameObject.CompareTag("FinishBoxCollider"))
        {
            if(endL == true)
            {
                if(countFactor.transform.rotation.eulerAngles.z >= -230)
                {
                    float newPosZ = countFactor.transform.rotation.eulerAngles.z;
                    newPosZ += -0.135f;
                    Vector3 rotationVector = new Vector3(0, 0, newPosZ);
                    countFactor.transform.rotation = Quaternion.Euler(rotationVector);
                }
                

                CancelInvoke("finishBoxEnd");
                Invoke("finishBoxEnd", 4f);
                finishBox.tag = "Untagged";
                CountFinishInt++;
                countFinishBoxText.text = CountFinishInt.ToString();
                startTextAnimator.Play("CountGo");
            }
            
        }
    }

    private void finishBoxEnd()
    {
        endL = false;
        Debug.Log("Done");
        EndCameraScript.ActiveAnimationCamera();
        NewCountFactor();
    }

    private void NewCountFactor()
    {
        if(CountFinishInt >= 0 && CountFinishInt < 225)
        {
            CountFinishInt = CountFinishInt * 2;
            Debug.Log("x2");
        }
        else if(CountFinishInt >= 225 && CountFinishInt < 450)
        {
            CountFinishInt = CountFinishInt * 4;
            Debug.Log("x4");
        }
        else if(CountFinishInt >= 450 && CountFinishInt < 675)
        {
            CountFinishInt = CountFinishInt * 6;
            Debug.Log("x6");
        }
        else if(CountFinishInt >= 675)
        {
            CountFinishInt = CountFinishInt * 8;
            Debug.Log("x8");
        }
        countFinishBoxText.text = CountFinishInt.ToString();
    }
}
