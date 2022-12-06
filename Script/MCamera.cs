using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCamera : MonoBehaviour
{
    public GameObject Player,meshFinish;
    public bool activeFrez;
    [SerializeField] private Animator CameraMain;

    [SerializeField] private Vector3 offset;

    private LevelManager levelManagerScript;
    private GameObject levelManagerObject;

    private void Start()
    {
        activeFrez = false;
        levelManagerObject = GameObject.FindWithTag("LevelManager");
        levelManagerScript = levelManagerObject.gameObject.GetComponent<LevelManager>();
    }
    private void LateUpdate()
    {
        if(Player != null)
        {
            transform.position = Player.transform.position + offset;
        }
        if(meshFinish != null)
        {
            transform.position = meshFinish.transform.position + offset;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0,0),transform.position.y, transform.position.z);
            if(transform.position.y <= 25f && activeFrez == true)
            {
                activeFrez = false;
                meshFinish = null;
            }
        }
    }
    public void ActiveAnimationCamera()
    {
        CameraMain.Play("FinishColbe");
        Invoke("ActivePanel",2.5f);
    }
    private void ActivePanel()
    {
        levelManagerScript.levelCompletePanel.SetActive(true);
        levelManagerScript.levelCurrentPanel.SetActive(false);
    }
}
