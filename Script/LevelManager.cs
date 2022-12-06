using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objectMesh = new GameObject[20];
    [SerializeField] private GameObject[] objectLevel = new GameObject[20];

    private MeshBuilder MeshBuilderScript;
    private GameObject MeshBuilderObject;

    [SerializeField] private int levelNubmer;
    [SerializeField] private TMP_Text currentLevelText,panelLevelText,boxCount;

    public GameObject levelCompletePanel;
    [SerializeField] private GameObject levelGameOverPanel;
    public GameObject levelCurrentPanel,countPanel;

    private void Start()
    {
        levelCompletePanel.SetActive(false);
        levelGameOverPanel.SetActive(false);
        levelCurrentPanel.SetActive(true);
        //countPanel.SetActive(true);
    }

    private void LateUpdate()
    {
        //boxCount.text = MeshBuilderScript.countBox.ToString();
    }
    public void startGame()
    {
        MeshBuilderObject = GameObject.FindWithTag("Player");
        MeshBuilderScript = MeshBuilderObject.gameObject.GetComponent<MeshBuilder>();
        if(PlayerPrefs.GetInt("LVLPART") == 0)
        {
            PlayerPrefs.SetInt("LVLPART", PlayerPrefs.GetInt("LVLPART") + 1);
        }

        levelNubmer = PlayerPrefs.GetInt("LVLPART");
        currentLevelText.text = "LEVEL " + levelNubmer.ToString();
        panelLevelText.text = levelNubmer.ToString();
        BuilderSetting();
    }

    private void BuilderSetting()
    {
        for(int i = 0; i < levelNubmer; i++)
        {
            if(i == levelNubmer - 1)
            {
                MeshBuilderScript.bagObject = objectMesh[i];
                objectMesh[i].SetActive(true);
                objectLevel[i].SetActive(true);
            }
            
        }
        MeshBuilderScript.DetectedLenght();
    }

    public void NextLevel()
    {
        if(PlayerPrefs.GetInt("LVLPART") >= 20)
        {
            PlayerPrefs.DeleteKey("LVLPART");
        }
        else
        {
            PlayerPrefs.SetInt("LVLPART", PlayerPrefs.GetInt("LVLPART") + 1);
            SceneManager.LoadScene(0);
        }
    }
}
