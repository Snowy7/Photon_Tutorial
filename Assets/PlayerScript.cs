using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{

    public Animator anim;
    public bool isOffline;
    public float speed = 10f;

    public TMP_Text username;

    public int kills;
    
    public float health = 100f;
    public string NickName;

    private void Start()
    {
        
        if (isOffline)
        {
            username.text = "Offline";
        }
        else
        {
            NickName = photonView.Owner.NickName;
            username.text = NickName + health;
        }
        
        
    }

    void Update()
    {
        username.text = NickName + health;
        if (!this.photonView.IsMine && !isOffline) { return; }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move(x, y);
        AnimationsUpdate(x, y);
    }

    private void AnimationsUpdate(float x, float y)
    {
        if (x != 0 || y != 0)
        {
            anim.SetBool("NotIdle", true);
        }
        else
        {
            anim.SetBool("NotIdle", false);
        }
    }

    void Move(float x, float y)
    {
        
        Vector2 velocity = new Vector2(x, y);
        velocity *= speed;
        velocity *= Time.deltaTime;
        transform.Translate(velocity);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == photonView.Owner)
        {
            if (changedProps.ContainsKey("Kills"))
            {
                kills = (int)changedProps["Kills"];
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //  We are sending data...
            stream.SendNext(health);
        }
        else
        {
            // We are reading the data
            health = (float)stream.ReceiveNext();
        }
    }
    
    private void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage(10);
    }
}
