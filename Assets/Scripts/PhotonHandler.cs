using UnityEngine;
using Photon.Pun;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;

public class PhotonHandler : MonoBehaviour
{
    public CharacterController characterController;

    public BasicPlatformerController basicPlatformerController;
    public GameObject cameraPlayer;
    public PhotonView photonView;



    private void Start()
    {
        if (!photonView.IsMine)
        {
            characterController.enabled = false;
            basicPlatformerController.enabled = false;
            cameraPlayer.SetActive(false);
        }
    }  

}
