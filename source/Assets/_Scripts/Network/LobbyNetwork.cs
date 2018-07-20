using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : Photon.PunBehaviour
{

    private void Start()
    {
        Debug.Log("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Connected to master.");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby.");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
    }
}
