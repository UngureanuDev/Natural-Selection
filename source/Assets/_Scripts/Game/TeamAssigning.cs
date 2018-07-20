using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TeamAssigning : MonoBehaviour {

    PhotonPlayer Player1;
    PhotonPlayer Player2;

    void RandomizeTeams()
    {
        int echipa = Random.Range(1, 100);
        if(echipa % 2 == 0)
        {
            Hashtable CustomPropertiesToSet = new Hashtable() { { "Echipa", "Natura" } };
            Player1.SetCustomProperties(CustomPropertiesToSet);
            CustomPropertiesToSet = new Hashtable() { { "Echipa", "Poluare" } };
            Player2.SetCustomProperties(CustomPropertiesToSet);
        }
        else
        {
            Hashtable CustomPropertiesToSet = new Hashtable() { { "Echipa", "Poluare" } };
            Player1.SetCustomProperties(CustomPropertiesToSet);
            CustomPropertiesToSet = new Hashtable() { { "Echipa", "Natura" } };
            Player2.SetCustomProperties(CustomPropertiesToSet);
        }
    }

    /// inainte de start
    private void Awake()
    {
        if (!PhotonNetwork.player.IsMasterClient) return;
        Player1 = PhotonNetwork.playerList[0];
        Player2 = PhotonNetwork.playerList[1];
        RandomizeTeams();
    }
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        //Application.Quit();
    }

}
