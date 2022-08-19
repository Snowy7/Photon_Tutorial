using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SyncedObject : MonoBehaviour, IPunObservable
{
    private Rigidbody rb;
    public float Health = 100f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (!PhotonNetwork.IsMasterClient) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Health -= 10f;
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //  We are sending data...
            stream.SendNext(Health);
        }
        else
        {
            // We are reading the data
            Health = (float)stream.ReceiveNext();
        }
    }
}
