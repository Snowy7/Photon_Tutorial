using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager2 : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        pv = GetComponent<PhotonView>();
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "Kills", 0 }});
    }

    void AddKill(int ActorNumber)
    {
        // use player properties to add kill to player
        Player player = PhotonNetwork.CurrentRoom.GetPlayer(ActorNumber);
        if (player == null) player = PhotonNetwork.LocalPlayer;
        Hashtable props = new Hashtable { { "Kills", (int)player.CustomProperties["Kills"] + 1 } };
        player.SetCustomProperties(props);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AddKill(PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }

    /*[Header("UI")]
    public TMP_Text numberText;

    public GameObject[] masterButtons;
    
    [Header("Values")]
    public int number;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        number = 0;
        pv = GetComponent<PhotonView>();

        bool isMaster = PhotonNetwork.IsMasterClient;
        foreach (var btn in masterButtons)
        {
            btn.SetActive(isMaster);
        }
    }

    public void Increase()
    {
        // number = 5
        // number + 1 = 6
        pv.RPC(nameof(RPC_ChangeNumber), RpcTarget.All, number + 1);
        //pv.RPC(nameof(RPC_ChangeNumber), RpcTarget.All, number + 1);
    }
    
    public void Decrease()
    {
        // number = 5
        // number - 1 = 4
        // parameters: int, string, byte, float, double, bool, char, short, long
        pv.RPC(nameof(RPC_ChangeNumber), RpcTarget.All, number - 1);
    }

    private void Update()
    {
        numberText.text = number.ToString();
    }

    [PunRPC]
    public void RPC_ChangeNumber(int newN = 0)
    {
        number = newN;
    }
    
    /*[PunRPC]
    public void RPC_Increase()
    {
        number++;
    }
    
    [PunRPC]
    public void RPC_Decrease()
    {
        number--;
    }#1#

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        foreach (var btn in masterButtons)
        {
            // ActorNumber = random number photon assigns to a player per room 
            btn.SetActive(PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber);
        }
        base.OnMasterClientSwitched(newMasterClient);
    }*/
}
