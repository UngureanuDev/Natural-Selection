using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    #region declarations
    public float offsetY = -2.75f;
    public GameObject CardTemplate;

    public GameObject GameManager;
    private Statistics statistics;
    private List<PlayingCard> hand;

    public int[] cardsCountByID_Natura;
    public int[] cardsCountByID_Poluare;

    public CardData[] cardInfo_Natura;
    public CardData[] cardInfo_Poluare;
    PhotonView photonView;

    [SerializeField]
    string echipa;

    #endregion

    void Start()
    {
        //ArrangeCards();
        statistics = GameManager.GetComponent<Statistics>();
        photonView = PhotonView.Get(this);

        echipa = (string)PhotonNetwork.player.CustomProperties["Echipa"];
    }

    public bool DrawCard()
    {

        /// lista cartilor pe care o are playerul in mana
        List<PlayingCard> hand;
        if (echipa == "Natura") hand = statistics.Mana_Natura;
        else hand = statistics.Mana_Poluare;

        /// numarul de carti ramase in deck-ul playerului
        int deck;
        if (echipa == "Natura") deck = statistics.deck_Natura;
        else deck = statistics.deck_Poluare;

        /// daca ai numarul maxim de carti, nu poti trage
        if (hand.Count >= 10) return false;
        /// daca nu mai ai carti in pachet, nu poti trage
        if (deck <= 0) return false;

        Debug.LogWarning("Jucatorul poate sa traga carte");

        /// aleg un id random din cartile ramase in pachet
        bool ok;
        int id;
        do
        {
            ok = true;
        /// alege un id random dintre toate
            if (echipa == "Natura")
            {
                id = Random.Range(0, cardsCountByID_Natura.Length);
                if (id >= cardsCountByID_Natura.Length) ok = false;
            }
            else
            {
                id = Random.Range(0, cardsCountByID_Poluare.Length);
                if (id >= cardsCountByID_Poluare.Length) ok = false;
            }
        /// daca nu mai sunt in pachet carti de tipul ales, alege iar
            if (echipa == "Natura" && cardsCountByID_Natura[id] <= 0) ok = false;
            else if (echipa == "Poluare" && cardsCountByID_Poluare[id] <= 0) ok = false;
        } while (ok == false);


        /// spawneaza cartea
        Vector3 pos = transform.position;
        pos.z = 0;
        GameObject InstantiatedCard = Instantiate(CardTemplate,pos,Quaternion.identity,transform);
        PlayingCard card = InstantiatedCard.GetComponent<PlayingCard>();
        /// initializeaza cartea cu id-ul ales
        card.SetID(id);
        card.SetEchipa(echipa);
        /// actualizeaza valorile
        if (echipa == "Natura")
        {
        /// scade numarul de carti de tipul ala ramase in deck
            cardsCountByID_Natura[id]--;
        /// scade numarul total de carti ramase in deck
            statistics.deck_Natura--;
        /// adauga cartea  spawnata in mana playerului
            statistics.Mana_Natura.Add(card);
        }
        else
        {
            cardsCountByID_Poluare[id]--;
            statistics.deck_Poluare--;
            statistics.Mana_Poluare.Add(card);
        }
        /// rearanjeaza cartile in mana playerului
        ArrangeCards();
        /// anunta oponentul ca playerul a tras o carte si specifica id-ul
        photonView.RPC("DrawCardEnemy", PhotonTargets.Others,id);
        return true;
    }

    public void ArrangeCards()
    {
        float y = -4.92f;

        List<PlayingCard> hand;
        if (echipa == "Natura") hand = statistics.Mana_Natura;
        else hand = statistics.Mana_Poluare;

        for (int i = 0; i < hand.Count; i++)
        {
            float x = -5.52f + i * 1.24f;
            PlayingCard card = hand[i].GetComponent<PlayingCard>();
            card.SetDestination(new Vector3(x, y, 0f));
        }
    }

    public void ArrangeCardsEnemy()
    {
        float y = 4.92f;
        List<PlayingCard> hand;
        if (echipa == "Natura") hand = statistics.Mana_Poluare;
        else hand = statistics.Mana_Natura;

        for(int i = 0; i < hand.Count; i++)
        {
            float x = -5.52f + i * 1.24f;
            PlayingCard card = hand[i].GetComponent<PlayingCard>();
            card.SetDestination(new Vector3(x, y, 0f));
        }
    }
    private void Update()
    {
        /// DEBUG
        //if(Input.GetButtonDown("Jump")) if(!DrawCard()) Debug.Log("Playerul nu poate trage carte");
    }
    public void RemoveFromList(PlayingCard card)
    {
        int index;
        if (echipa == "Natura") index = statistics.Mana_Natura.IndexOf(card);
        else index = statistics.Mana_Poluare.IndexOf(card);

        /// remove din lista playerului
        if (echipa == "Natura")
        {
            statistics.Mana_Natura.Remove(card);
        }
        else
        {
            statistics.Mana_Poluare.Remove(card);
        }
        ///remove din lista inamicului
        if (echipa == "Natura") photonView.RPC("RemoveCardEnemy", PhotonTargets.Others, index);
        else photonView.RPC("RemoveCardEnemy", PhotonTargets.Others, index);

    }
    /// <summary>
    /// Anunta oponentul ca playerul a tras o carte, pentru sincronizare
    /// </summary>
    /// <param name="id">id-ul cartii trase</param>
    [PunRPC]
    void DrawCardEnemy(int id)
    {
        ///TODO: sa schimb template-ul in cartea oponentului
        Vector3 pos = transform.position;
        pos.z = 0;
        GameObject InstantiatedCard = Instantiate(CardTemplate, pos , Quaternion.identity, transform);
        PlayingCard card = InstantiatedCard.GetComponent<PlayingCard>();
        card.transform.Rotate(new Vector3(0, 0, 180f));
        card.SetID_Enemy(id);
        card.Intoarce(false);
        if (echipa == "Poluare")
        {
            card.SetEchipa("Natura");
            cardsCountByID_Natura[id]--;
            statistics.Mana_Natura.Add(card);
            statistics.deck_Natura--;
        }
        else
        {
            card.SetEchipa("Poluare");
            cardsCountByID_Poluare[id]--;
            statistics.Mana_Poluare.Add(card);
            statistics.deck_Poluare--;
        }
        ArrangeCardsEnemy();
    }
    [PunRPC]
    void RemoveCardEnemy(int index)
    {
        Debug.Log("Distrug cartea cu indexul " + index);
        PlayingCard carte;
        //if (echipa == "Natura") statistics.Mana_Poluare.RemoveAt(index);
        //else statistics.Mana_Natura.RemoveAt(index);
        if (echipa == "Natura")
        {
            carte = statistics.Mana_Poluare[index];
            statistics.Mana_Poluare.Remove(carte);
        }
        else
        {
            carte = statistics.Mana_Natura[index];
            statistics.Mana_Natura.Remove(carte);
        }
        carte.MoveToGraveyard();
    }
}