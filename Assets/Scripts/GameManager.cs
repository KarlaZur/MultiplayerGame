using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Intancias de Scripts")]
    public WindowHandler windowhandler;

    [Header("Indicadores")]
    public TMP_Text textIndicador;
    public TMP_Text textNameSala;
   public Transform contentPlayers;



    [Header("prefabs")]
    public GameObject nickNamePlayer;



    [Header("Bones de menu")]
    public GameObject btnConnet;
    public GameObject  btnStart;


    private void Start()
    {
        btnConnet.SetActive(false);
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
        string  user = PhotonNetwork.NickName;
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
        windowhandler.EnableWindow(1);
        textNameSala.text = $"Nombre de la sala: {PhotonNetwork.CurrentRoom.Name}- #Jugadores: {PhotonNetwork.CurrentRoom.PlayerCount}";
        Debug.Log("Usuario " + PhotonNetwork.NickName + " se ha unido a la sala " + PhotonNetwork.CurrentRoom.Name);
    }
}


