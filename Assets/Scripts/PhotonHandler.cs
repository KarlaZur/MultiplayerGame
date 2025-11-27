using UnityEngine;
using Photon.Pun;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PhotonHandler : MonoBehaviour
{
    public CharacterController characterController;

    public BasicPlatformerController basicPlatformerController;
    public GameObject cameraPlayer;
    public PhotonView photonView;

    public GameObject msmObject;

    private void Start()
    {

        msmObject = GameObject.FindGameObjectWithTag("txtMsm");
        if (!photonView.IsMine)
        {
            characterController.enabled = false;
            basicPlatformerController.enabled = false;
            cameraPlayer.SetActive(false);
        }
    }  

   public void Update()
{
    if (Input.GetKeyDown(KeyCode.M))
    {
        photonView.RPC("SendMsm", RpcTarget.All, "Hola mundo desde Photon!");
    }
}

    [PunRPC]
    public void SendMsm(string msm)
    {
        if (msmObject != null)
        {
            msmObject.GetComponent<TMP_Text>().text = msm;
        }
    }   
}
