using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWatcher : MonoBehaviourPunCallbacks
{
    public static SceneWatcher Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }


    public override void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        base.OnEnable();
    }
    
    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        base.OnDisable();
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 0)
        {
            // We are in a game scene
            // Instantiate the player
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        }

    }
}
