using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public enum SceneStage
    {
        TrashKing,
        PlagueDoctor,
        Normal
    };

    public SceneStage stage;
    private int sceneNumber = 1;

    public static Manager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        if (sceneNumber == 1)
        {
            SceneManager.LoadScene("Stage2");
            sceneNumber = 2;
        }
        else if (sceneNumber == 2)
        {
            SceneManager.LoadScene("Plague Doctor");
            stage = SceneStage.PlagueDoctor;
        }
    }
}
