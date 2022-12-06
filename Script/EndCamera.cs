using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Animator CameraMain;

    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if(Player != null)
        {
            transform.position = Player.transform.position + offset;
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0,0),Mathf.Clamp(transform.position.y, -53.5f,-20f), Mathf.Clamp(transform.position.z, 345,340));
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0,0),transform.position.y, transform.position.z);
            if(transform.position.y <= -51.5f)
            {
                Player = null;
            }
        }
    }
    public void ActiveAnimationCamera()
    {
        CameraMain.Play("CameraMove");
    }
}
