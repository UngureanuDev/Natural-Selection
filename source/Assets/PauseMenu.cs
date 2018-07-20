using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Photon.PunBehaviour {

    public GameObject PauseMenuObj;

    private void Start () {
        
    }

    private void Update () {
        if (Input.GetButtonDown("Cancel")) PauseMenuObj.SetActive(!PauseMenuObj.activeInHierarchy);
    }
    public void ClickDisconnect()
    {
        Debug.Log("Disconnecting");
        PhotonNetwork.LeaveRoom();
        PauseMenuObj.SetActive(!PauseMenuObj.activeInHierarchy);
        SceneManager.LoadScene(0);
    }

    public void ClickBackToMainMenu()
    {
        Debug.Log("Disconnecting");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
    public override void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer)
    {
        Debug.Log("disconnected");
        if (gameObject.GetComponent<TurnManager>().isGameRunning)
        {
            if ((string) photonPlayer.CustomProperties["Echipa"] == "Natura")
                gameObject.GetComponent<TurnManager>().BroadcastGameOver("Poluare",2);
            else
                gameObject.GetComponent<TurnManager>().BroadcastGameOver("Natura",2);
        }
        /*
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
        */
    }
}