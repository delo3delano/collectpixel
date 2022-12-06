using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeshControll : MonoBehaviour
{
    [SerializeField] private float speedForward,speedLeftRight,posX,speed;
    private MeshBuilder meshScript;
    private bool finishActive;
    [SerializeField] private Vector3 finishPositionObject;
    private MCamera mCameraScript;
    [SerializeField] private Animator cameraAnimaor;
    [SerializeField] private GameObject myCamera;

    private void Start()
    {
        finishActive = true;
        meshScript = gameObject.GetComponent<MeshBuilder>();
        mCameraScript = myCamera.gameObject.GetComponent<MCamera>();
        
    }
    private void Update()
    {
        MovePlayer();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x * speed * Time.deltaTime, 0 , 0);
            Time.timeScale = 1f;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -posX, posX),transform.position.y, transform.position.z);
    }

    private void MovePlayer()
    {
        transform.position += Vector3.forward * speedForward * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speedLeftRight * Time.deltaTime, Space.World);
        } 
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speedLeftRight * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider triggerBox)
    {
        if(triggerBox.gameObject.CompareTag("NullBox"))
        {
            triggerBox.gameObject.tag = "BoxMesh";
            meshScript.builObject(triggerBox.gameObject);
        }
        if(triggerBox.gameObject.CompareTag("Finish"))
        {
            if(finishActive == true)
            {
                Sequence sequence = DOTween.Sequence();
                finishActive = false;
                speedForward = 0;
                speedLeftRight = 0;
                GameObject bagBuilder;
                CounterObject newCounterScript;
                bagBuilder = meshScript.bagObject;

                bagBuilder.transform.SetParent(null);

                bagBuilder.tag = "CounterObject";
                
                newCounterScript = bagBuilder.AddComponent<CounterObject>();
                bagBuilder.gameObject.AddComponent<Rigidbody>().isKinematic = true;
                mCameraScript.meshFinish = bagBuilder;
                mCameraScript.Player = null;
                sequence.Append(bagBuilder.transform.DOLocalJump(finishPositionObject, finishPositionObject.y, 1, 2.5f, false)).OnComplete(()=>
                bagBuilder.gameObject.GetComponent<Rigidbody>().isKinematic = false);
                cameraAnimaor.Play("RotateCamera");

                sequence.Join(bagBuilder.transform.DOLocalRotate(new Vector3(0f,0,0f), 2.5f));
                bagBuilder.AddComponent<BoxCollider>().size = new Vector3(0.1f,0.1f,0.1f);
                bagBuilder.layer = 15;

                Invoke("ActiveBoolCamera",2.5f);
            }
        }
    }
    private void ActiveBoolCamera()
    {
        mCameraScript.activeFrez = true;
    }
}
