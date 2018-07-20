using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
	public void OnClickStartMatch()
    {
        Debug.Log("Start Match clicked!");
        if (PhotonNetwork.isMasterClient)
        {
            if (PhotonNetwork.playerList.Length != 2) return;
            Debug.Log("There are enough players. Starting..");
            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }
}
