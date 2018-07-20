using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : Photon.PunBehaviour
{

    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }
    public void CreateRoomClick()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2};

        if(PhotonNetwork.CreateRoom(RoomName.text, roomOptions,TypedLobby.Default))
        {
            Debug.Log("Create room succesfully sent.");
        }
        else
        {
            Debug.Log("create room failed to send ");
        }
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("create room failed: " + codeAndMsg[1]);
        MoveCameraLeft();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created succesfully");
        MoveCameraRight();
    }

    public void MoveCameraLeft()
    {
        Camera.main.transform.position = new Vector3(-768, 358, -10);

    }
    void MoveCameraRight()
    {
        Camera.main.transform.position = new Vector3(506, 358, -10);
    }
}
