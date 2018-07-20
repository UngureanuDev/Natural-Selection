using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Launcher : Photon.PunBehaviour {

    public Text statusText;
    public Text roomPlayerCount;
    public Text masterPlayerCount;
    string _gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = false;
    }

    // Use this for initialization
    void Start () {
        statusText.text = "Trying to connect to a server";
        Connect();
	}
    private void LateUpdate()
    {
        //masterPlayerCount.text = "Players online:" + PhotonNetwork.countOfPlayers.ToString();
        //roomPlayerCount.text = PhotonNetwork.countOfPlayersOnMaster.ToString() + "/2 player(s)";
    }
    public void Connect()
    {
        if(PhotonNetwork.connected)
        {
            //we need to attempt joining a random room. if it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //we must first connect to Photon Online Server
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
        }
    }
    #region Photon.PunBehaviourCallBacks
    public override void OnConnectedToMaster()
    {
        statusText.text = "Finding an opponent.";
        Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnectedFromPhoton()
    {
        statusText.text = "Disconnected from photon servers.";
        Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, " +
            "so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        statusText.text = "No existing room found, creating one.";
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 }, null);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        statusText.text = "Joined room";
    }
    public override void OnLobbyStatisticsUpdate()
    {
        Debug.Log("STATISTICS UPDATE");
        masterPlayerCount.text = "Players online:" + PhotonNetwork.countOfPlayers.ToString();
        roomPlayerCount.text = PhotonNetwork.countOfPlayersOnMaster.ToString() + "/2 player(s)";
    }

    #endregion
}
