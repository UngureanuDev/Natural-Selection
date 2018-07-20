using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{

    public int turnNumber { get; private set; }
    public bool isMyTurn;
    PhotonView photonView;
    private string echipa;

    public Button EndTurnButton;
    public GameObject GameManager;
    public GameObject Cards;
    //public GameObject Scoreboard;
    public GameObject parametriObj;
    public GameObject AudioManager;

    public Text ActionsLeftText;
    public Text SpellsLeftText;

    public bool isGameRunning;

    public int ActionsLeftThisTurn;
    public int SpellsLeftThisTurn;

    public GameObject GameOverObj;
    public GameObject GameOverBackground;
    public GameObject GameOverWinLose; /// ai castigat/ai pierdut
    public GameObject GameOverDescription;

    private int quant;
    public GameObject Quant_Text;

    public GameObject turnNumberText;

    public void SyncParametri(int foame, int stres, int igiena, int progresFoame, int progresStres, int progresIgiena)
    {
        photonView = PhotonView.Get(this);
        photonView.RPC("SyncParametriRPC", PhotonTargets.All, foame,stres,igiena, progresFoame, progresStres, progresIgiena);
    }
    [PunRPC]
    public void SyncParametriRPC(int foame, int stres, int igiena, int progresFoame, int progresStres, int progresIgiena)
    {
        parametriObj.GetComponent<Parametri>().SyncParameters(foame,stres,igiena,progresFoame, progresStres, progresIgiena);
    }
    [PunRPC]
    public void SetQuant(int x)
    {
        quant = x;
        if (x == 1)
        {
            Quant_Text.SetActive(false);
            return;
        }
        Quant_Text.SetActive(true);
        Quant_Text.GetComponent<TextMeshProUGUI>().SetText("x" + x.ToString());
    }
    public void Set_quant(int x)
    {
        photonView.RPC("SetQuant", PhotonTargets.All, x);
    }
    
    void Start()
    {
        isGameRunning = true;
        photonView = PhotonView.Get(this);
        Set_quant(1);
        turnNumber = 1;
        echipa = (string) PhotonNetwork.player.CustomProperties["Echipa"];
        if (echipa == "Natura") isMyTurn = true;
        else isMyTurn = false;
        if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare") ActionsLeftText.transform.parent.gameObject.SetActive(false);
        else ActionsLeftText.transform.parent.gameObject.SetActive(true);
        UpdateVariables();
        for(int i = 0; i < 5; i++) Invoke("drawCard", i * 0.5f);
        //Scoreboard.GetComponent<Scoreboard>().Initialize_UI();
        //if(PhotonNetwork.player.IsMasterClient)photonView.RPC("EndTurn",PhotonTargets.All);
    }

    void drawCard()
    {
        Cards.GetComponent<CardsManager>().DrawCard();
    }

    public void scadeHp(int boala)
    {
        photonView.RPC("scadeHpRPC", PhotonTargets.All, boala);
    }
    [PunRPC]
    public void scadeHpRPC(int boala)
    {
        parametriObj.GetComponent<Parametri>().ScadeHP(boala);
    }

    public void ChangeData(int indexHand, int newID)
    {
        photonView.RPC("ChangeDataRPC", PhotonTargets.All, indexHand,newID);
    }

    public void BroadcastGameOver(string echipaCastigatoare, int winID)
    {
        photonView.RPC("GameOver", PhotonTargets.All, echipaCastigatoare, winID);
    }

    [PunRPC]
    public void ChangeDataRPC(int indexHand, int newID)
    {
        GameManager.GetComponent<Statistics>().Mana_Natura[indexHand].ChangeData(newID);
    }

    [PunRPC]
    public void EndTurn()
    {
        Debug.Log("Ending Turn");
        //Scoreboard.GetComponent<Scoreboard>().AplicaScor(quant);
        if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare" && isMyTurn)
        {
            parametriObj.GetComponent<Parametri>().ApplyChangesParameter(quant);
            Set_quant(1);
            turnNumber++;
        }
        else if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura" && !isMyTurn) {
            Set_quant(1);
            turnNumber++;
        }
        if (turnNumber == 31)
        {
            turnNumber--;
            BroadcastGameOver("Natura",1);
        }
        /// variabile
        isMyTurn = !isMyTurn;
        UpdateVariables();
    }

    void UpdateVariables()
    {
        if (isMyTurn)
        {
            ActionsLeftThisTurn = 1;
            SpellsLeftThisTurn = 1;
            /// buton

            Cards.GetComponent<CardsManager>().DrawCard();
            EndTurnButton.interactable = true;
            EndTurnButton.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            EndTurnButton.GetComponentInChildren<TextMeshProUGUI>().SetText("End turn");
        }
        else
        {
            ActionsLeftThisTurn = 0;
            SpellsLeftThisTurn = 0;
            ///buton
            EndTurnButton.interactable = false;
            EndTurnButton.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            EndTurnButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Not your turn");
        }
        ActionsLeftText.text = ActionsLeftThisTurn.ToString();
        SpellsLeftText.text = SpellsLeftThisTurn.ToString();
        turnNumberText.GetComponent<TextMeshProUGUI>().SetText("Turn: " + turnNumber + "/30");
    }

    /// <summary>
    /// tries to play an action, return false if it succeeds.
    /// must be called with if()
    /// </summary>
    /// <returns></returns>
    public bool TryToPlayAction()
    {
        if (ActionsLeftThisTurn <= 0) return false;
        ActionsLeftThisTurn--;
        ActionsLeftText.text = ActionsLeftThisTurn.ToString();
        return true;
    }

    public bool TryToPlaySpell()
    {
        if (SpellsLeftThisTurn <= 0) return false;
        SpellsLeftThisTurn--;
        SpellsLeftText.text = SpellsLeftThisTurn.ToString();
        return true;
    }

    public void AddSpellsLeft(int x)
    {
        SpellsLeftThisTurn += x;
        SpellsLeftText.text = SpellsLeftThisTurn.ToString();
    }
    public void AddActionsLeft(int x)
    {
        ActionsLeftThisTurn += x;
        ActionsLeftText.text = ActionsLeftThisTurn.ToString();
    }

    public void PressEndTurn()
    {
        photonView.RPC("EndTurn", PhotonTargets.All);
    }

    [PunRPC]
    public void GameOver(string echipaCastigatoare, int winID)
    {
        isGameRunning = false;
        GameOverObj.SetActive(true);
        if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == echipaCastigatoare)
        {
            GameOverBackground.GetComponent<Image>().color = new Color32(81,193,60,255);
            GameOverWinLose.GetComponent<TextMeshProUGUI>().SetText("Congratulations! You've won the match!");
            if(winID == 1) GameOverDescription.GetComponent<TextMeshProUGUI>().SetText("You've won the match " + turnNumber + " rounds");
            else if (winID == 2)
                GameOverDescription.GetComponent<TextMeshProUGUI>()
                    .SetText("Your opponent has abandoned after " + turnNumber + " rounds.");
            ///win
        }
        else
        {
            //lose
            GameOverBackground.GetComponent<Image>().color = new Color32(193, 60, 60, 255);
            GameOverWinLose.GetComponent<TextMeshProUGUI>().SetText("Game over! You've lost.");
            if (winID == 1) GameOverDescription.GetComponent<TextMeshProUGUI>().SetText("Your opponent has won the game after " + turnNumber + " rounds");
            else if (winID == 2)
                GameOverDescription.GetComponent<TextMeshProUGUI>()
                    .SetText("You have abandoned the match after " + turnNumber + " rounds.");
        }
    }
}