using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GateScript : MonoBehaviour
{
    private bool GateActive;
    private MeshBuilder meshBuilderScript;
    private GameObject handObject;

    [SerializeField] private GameObject cubeGate;
    [SerializeField] private Material grenMaterial,redMaterial;
    [SerializeField] private TMP_Text textMesh;
    [SerializeField] private Color[] newColor;
    private string myText;
    private int countGate;

    private void Start()
    {
        GateActive = true;
        handObject = GameObject.FindWithTag("Player");
        meshBuilderScript = handObject.gameObject.GetComponent<MeshBuilder>();
        countGate = Int16.Parse(myText = textMesh.text);
        if(countGate > 0)
        {
            cubeGate.gameObject.GetComponent<MeshRenderer>().material = grenMaterial;
            textMesh.color = newColor[1];
        }
        else if(countGate < 0)
        {
            cubeGate.gameObject.GetComponent<MeshRenderer>().material = redMaterial;
            textMesh.color = newColor[0];
        }
    }

    private void OnTriggerEnter(Collider gateBox)
    {
        if(gateBox.gameObject.CompareTag("Player") && GateActive == true)
        {
            if(countGate > 0)
            {
                GateActive = false;
                meshBuilderScript.BuildBox(countGate, gateBox.gameObject);
            }
            else if(countGate < 0)
            {
                GateActive = false;
                int minusInt = Math.Abs(countGate);
                meshBuilderScript.destroyBox(minusInt);
            }
            
        }
    }
}
