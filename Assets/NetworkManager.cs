using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField playerNameInput;
    public TMP_Text in_game_text;
    
    [Header("Menus")]
    public GameObject loadingMenu;
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject loginMenu;
    
    #region functions
    
    private void Start()
    {
        loginMenu.SetActive(true);
        loadingMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
    }

    public void Connect()
    {
        loadingMenu.SetActive(true);
        loginMenu.SetActive(false);
        
        var playerName = playerNameInput.text;
        
        if (playerName == string.Empty)
        {
            playerName = "Player" + Random.Range(1000, 99999);
        }

        // Connect photon
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public void JoinRoom()
    {
        mainMenu.SetActive(false);
        loadingMenu.SetActive(true);
        gameMenu.SetActive(false);
        // join random room ... if no room, create one
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    
    #endregion

    #region Photon Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
        base.OnConnectedToMaster();
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined LOBBY");
        loadingMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        loadingMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        in_game_text.text = "Welcome " + PhotonNetwork.NickName + " to the Game :)";
     
        // Load the game scene IF we are the master client
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel(1);
        
        base.OnJoinedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        loadingMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        loadingMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        Debug.LogWarning("Failed to join room [" + returnCode + "]: "  + message);
        base.OnJoinRandomFailed(returnCode, message);
    }

    #endregion
    
}
