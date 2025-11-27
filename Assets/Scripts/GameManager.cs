using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Intancias de Scripts")]
    public WindowHandler windowHandler;


    [Header("Indicadores")]
    public TMP_Text textIndicador;
    public TMP_Text textNameSala;
    public Transform contentPlayers;


    [Header("prefabs")]
    public GameObject nickNamePlayer;


    [Header("Bones de menu")]
    public GameObject btnConnet;
    public GameObject btnStart;

    private int countPlayer = 0;


    private void Start()
    {
        btnConnet.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    public void ConnetPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void CreatePlayer(string namePlayer)
    {
        PhotonNetwork.NickName = namePlayer;
    }

    //verificamos la coneccion a internet
    public override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Conectado a Photon");
        textIndicador.text = "Conectado correctamente...";
    }
    //verificamos la coneccion al servidor de Photon
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        textIndicador.text = "Bienvenido " + PhotonNetwork.NickName;
        btnStart.GetComponent<Button>().interactable = false;
        btnConnet.SetActive(true);

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("Desconectado de Photon por: " + cause.ToString());
        textIndicador.text = "Desconectado de Photon...";
    }

    public void CreateRoom()
    {
        string user = PhotonNetwork.NickName;
        string nameRoom = "sala1";

        RoomOptions optionRoom = new RoomOptions();
        optionRoom.IsVisible = true;
        optionRoom.MaxPlayers = 5;
        optionRoom.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(nameRoom, optionRoom, TypedLobby.Default, null);
    }



    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        windowHandler.EnabledWindow(1);
        StartCoroutine(UpdateTextSala());
        Debug.Log("Estamos conectados a la sala "
                  + PhotonNetwork.CurrentRoom.Name
                  + " | Bienvenido: " + PhotonNetwork.NickName);
    }

    public void StartScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InGame");
        }
    }

    IEnumerator UpdateTextSala()
    {
        while (true)
        {
            textNameSala.text =
                $"Nombre sala: {PhotonNetwork.CurrentRoom.Name} - " +
                $"#Jugadores: {PhotonNetwork.CurrentRoom.PlayerCount}";

            yield return new WaitForSeconds(0.2f);
            if (PhotonNetwork.CurrentRoom.Players.Count > countPlayer)
            {
                countPlayer = PhotonNetwork.CurrentRoom.Players.Count;
                foreach (Transform child in contentPlayers)
                    Destroy(child.gameObject);
                foreach (var item in PhotonNetwork.CurrentRoom.Players)
                {
                    GameObject nickName = Instantiate(nickNamePlayer, contentPlayers);
                    nickName.GetComponent<TMP_Text>().text = item.Value.NickName;
                }
            }

        }


    }

}



