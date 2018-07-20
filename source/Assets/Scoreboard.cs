using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class Scoreboard : MonoBehaviour {

    public GameObject[] Tiles;
    private string echipa;

    public GameObject ScorNatura;
    public GameObject ScorPoluare;
    public GameObject ProgresNatura;
    public GameObject ProgresPoluare;

    private void Awake()
    {
        echipa = (string)PhotonNetwork.player.CustomProperties["Echipa"];
        ///setez scor initial la ambii playeri, 50.
        Hashtable CustomPropertiesToSet = new Hashtable() { { "Scor", "50" } };
        PhotonNetwork.player.SetCustomProperties(CustomPropertiesToSet);
    }

    public int CalculatePointsPlayer()
    {
        if(echipa == null) echipa = (string)PhotonNetwork.player.CustomProperties["Echipa"];
        int s = 0;
        Text text;
        foreach (GameObject tile in Tiles)
        {
            text = tile.GetComponentInChildren<Text>();
            int curent;
            int.TryParse(text.text,out curent);
            if (text.color == new Color(0, 1, 0) && echipa == "Natura" || 
                text.color == new Color(1, 0, 0) && echipa == "Poluare") s += curent;
        }
        return s;
    }

    public int CalculatePointsEnemy()
    {
        if (echipa == null) echipa = (string)PhotonNetwork.player.CustomProperties["Echipa"];
        int s = 0;
        Text text;
        foreach (GameObject tile in Tiles)
        {
            text = tile.GetComponentInChildren<Text>();
            int curent;
            int.TryParse(text.text,out curent);
            if (text.color == new Color(0, 1, 0) && echipa == "Poluare" ||
                text.color == new Color(1, 0, 0) && echipa == "Natura") s += curent;
        }
        return s;
    }

    public void AplicaScor(int quantif)
    {
        int scorPlayer, scorInamic;
        int avantaj = CalculatePointsPlayer() - CalculatePointsEnemy();
                int.TryParse((string)PhotonNetwork.player.CustomProperties["Scor"],out scorPlayer);
                int.TryParse((string) PhotonNetwork.otherPlayers[0].CustomProperties["Scor"],out scorInamic);
        scorPlayer = scorPlayer + avantaj * quantif;
        scorInamic = scorInamic - avantaj * quantif;
        Hashtable CustomPropertiesToSet = new Hashtable() { { "Scor", scorPlayer.ToString() } };
        PhotonNetwork.player.SetCustomProperties(CustomPropertiesToSet);
        ///calculez manual pentru celalalt, in caz ca nu l-a updatat inca
        if ((string)PhotonNetwork.player.CustomProperties["Echipa"]== "Natura") UpdateUI(scorPlayer, scorInamic,avantaj);
        else UpdateUI(scorInamic, scorPlayer,avantaj);
    }

    public void Initialize_UI()
    {
            ScorNatura.GetComponent<TextMeshProUGUI>().SetText("50");
            ScorPoluare.GetComponent<TextMeshProUGUI>().SetText("50");
            ProgresNatura.SetActive(false);
            ProgresPoluare.SetActive(false);
    }

    private void UpdateUI(int natura, int poluare, int avantaj)
    {
        ScorNatura.GetComponent<TextMeshProUGUI>().SetText(natura.ToString());
        ScorPoluare.GetComponent<TextMeshProUGUI>().SetText(poluare.ToString());
        if (echipa == "Natura")
        {
            
            if (avantaj > 0)
            {
                ProgresNatura.SetActive(true);
                ProgresPoluare.SetActive(true);
                ProgresNatura.GetComponent<TextMeshProUGUI>().SetText("+" + avantaj.ToString());
                ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText("-" + avantaj.ToString());
            }
            else if (avantaj < 0)
            {
                ProgresNatura.SetActive(true);
                ProgresPoluare.SetActive(true);
                ProgresNatura.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
                ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText("+" + (-avantaj).ToString());
            }
            else
            {
                ProgresNatura.SetActive(false);
                ProgresPoluare.SetActive(false);
                //ProgresNatura.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
                //ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
            }
        }
        else ///daca sunt din echipa poluare
        {
            if (avantaj > 0)
            {
                ProgresNatura.SetActive(true);
                ProgresPoluare.SetActive(true);
                ProgresNatura.GetComponent<TextMeshProUGUI>().SetText("-" + avantaj.ToString());
                ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText("+" + avantaj.ToString());
            }
            else if (avantaj < 0)
            {
                ProgresNatura.SetActive(true);
                ProgresPoluare.SetActive(true);
                ProgresNatura.GetComponent<TextMeshProUGUI>().SetText("+" + (-avantaj).ToString());
                ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
            }
            else
            {
                ProgresNatura.SetActive(false);
                ProgresPoluare.SetActive(false);
                //ProgresNatura.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
                //ProgresPoluare.GetComponent<TextMeshProUGUI>().SetText(avantaj.ToString());
            }
        }
    }

}