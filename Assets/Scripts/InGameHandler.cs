using UnityEngine;
using Photon.Pun;
public class InGameHandler : MonoBehaviour
{
    public Transform spawnPsition;
    private void Start()
    {
        PhotonNetwork.Instantiate("Player", spawnPsition.position, Quaternion.identity);
    }
}