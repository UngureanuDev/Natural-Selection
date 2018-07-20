using UnityEngine;
using UnityEngine.UI;

public class Verificare : MonoBehaviour {

    public GameObject imageObj;

    private void Start () {
        Image bg = imageObj.GetComponent<Image>();
        if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
            bg.color = new Color32(30, 150, 66, 255);
        else if((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
            bg.color = new Color32(150, 30, 30, 255);
    }
}