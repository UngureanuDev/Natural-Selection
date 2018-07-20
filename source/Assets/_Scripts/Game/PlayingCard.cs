using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayingCard : MonoBehaviour
{
    #region variabile
    public CardData data;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public GameObject frontImage;
    public Image artworkImage;
    public GameObject backImage;
    public GameObject manaImage;
    public Text costText;
    public GameObject cardBack;
    public GameObject cardFront;
    private CardsManager cardManager;

    public GameObject poofParticle;

    private bool intoarsa;
    public string EchipaCarte { get; private set; }
    private string echipaPlayer;

    private Vector3 destination;
    public int ID;
    public int type;

    //public int value { get; private set; }

    private Hover hover;
    #endregion

    public void SetDestination(Vector3 dest)
    {
        dest.z = 0;
        destination = dest;
    }


    /// <summary>
    /// intoarce cartea in functie de parametru
    /// </summary>
    /// <param name="inSus">True daca trebuie intoarsa cu fata in sus, false daca trebuie intoarsa cu fata in jos</param>
    public void Intoarce(bool inSus)
    {
        if (inSus)
        {
            cardFront.SetActive(true);
            cardBack.SetActive(false);
        }
        else
        {
            cardBack.SetActive(true);
            cardFront.SetActive(false);
        }
    }

    private void Awake()
    {
        Debug.Log("Carte spawnata la " + transform.position.x + " , " + transform.position.y + " , " +
                  transform.position.z);
        GameObject Cards = GameObject.Find("Cards");
        cardManager = Cards.GetComponent<CardsManager>();
        hover = gameObject.GetComponent<Hover>();
        echipaPlayer = (string) PhotonNetwork.player.CustomProperties["Echipa"];

        /// daca e cartea playerului, incepe neintoarsa 
        if (echipaPlayer == EchipaCarte)
        {
            intoarsa = false;
        }
        /// altfel, apare pe teren intoarsa cu spatele, in mana oponentului
        else
        {
            intoarsa = true;
        }
    }

    private void Update()
    {
        if (!hover.hovering && !hover.dragging) transform.position = Vector3.Lerp(transform.position, destination, 0.15f);
        if (Vector3.Distance(transform.position, destination) < 0.2f) hover.shouldWork = true;
        else hover.shouldWork = false;

        ///DEBUG
    }

    public void SetID(int id)
    {
        ID = id;
        if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura") data = cardManager.cardInfo_Natura[id];
        else data = cardManager.cardInfo_Poluare[id];
        Debug.Log(data);
        //value = data.value;
        type = data.type;
        Debug.Log("Setting id " + id + " , data:" + data);
        SetGraphics(data);
    }

    public void SetID_Enemy(int id)
    {
        if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura") data = cardManager.cardInfo_Poluare[id];
        else data = cardManager.cardInfo_Natura[id];
        Debug.Log(data);
        //value = data.value;
        Debug.Log("Setting id " + id + " , data:" + data);
        SetGraphics(data);
    }

    public void SetEchipa(string s)
    {
        EchipaCarte = s;
    }

    void SetGraphics(CardData data)
    {
        nameText.SetText(data.name);
        descriptionText.SetText(data.description);
        artworkImage.sprite = data.artwork;
        costText.text = data.cost.ToString();
        backImage.GetComponent<SpriteRenderer>().sprite = data.back;
        frontImage.GetComponent<SpriteRenderer>().sprite = data.front;
        manaImage.GetComponent<Image>().sprite = data.manaImage;

    }
    public Vector3 GetDestination()
    {
        return destination;
    }

    public void ChangeData(int newID)
    {
        FindObjectOfType<AudioManager>().Play("Woosh");
        GameObject particles = Instantiate(poofParticle,transform.position,transform.rotation,transform);
        Destroy(particles, 5f);
        data = GameObject.Find("Cards").GetComponent<CardsManager>().cardInfo_Poluare[newID];

        nameText.SetText(data.name);
        artworkImage.sprite = data.artwork;
        costText.text = data.cost.ToString();
        manaImage.GetComponent<Image>().sprite = data.manaImage;

        switch (data.ID_Swap)
        {
            case 4:
                ///Hamburger
                type = 2;
                //descriptionText.text = "";
                data.description = "A hamburger is a sandwich consisting of one or more cooked patties of ground meat placed inside a bun. Increases your satiety and decreases your hygiene.";
                break;
            case 5:
                ///suc
                type = 2;
                data.description = "Soda is a beverage made with chemicals and sweetners. It incredecreases thirst and increases hygiene.";
                break;
            case 6:
                ///chipsuri
                type = 2;
                data.description = "Chips are a type of snack food, typically made from potatoes. Eating chips increases satiety and decreases hygiene.";
                break;
            case 7:
                ///maraton jocuri video
                type = 2;
                data.description = "Playing video games for an extended period of time is bad for your health. Increases your morale and decreases your satiety and hygiene.";
                break;
            case 8:
                ///lenevit
                type = 2;
                data.description = "Wasting time is not a good idea in any situation, but it increases your morale while decreasing your hygiene and satiety.";
                break;
            case 9:
                ///fumat
                type = 2;
                data.description = "Smoking is a practice in which a substance is burned and inhaled into the bloodstream. it increases morale and decreases hygiene and satiety.";
                break;
            case 10:
                ///maraton seriale
                type = 2;
                data.description = "Watching a TV show for a long time increases for morale, while decreasing satiety and hygiene.";
                break;
           
        }
        descriptionText.SetText(data.description);
    }


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision:" + collision.transform.name);
        if (!hover.dragging) return;
        else
        {
            Image image = collision.transform.GetComponent<Image>();
            image.color = new Color32(0, 0, 0, 255);
        }
    }
    */

    public void DestroyCard()
    {
        GameObject.Find("Cards").GetComponent<CardsManager>().RemoveFromList(this);
        MoveToGraveyard();
    }
    public void MoveToGraveyard()
    {
        ///mut spre graveyard
        ///        if (EchipaCarte == "Natura" && (string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")

        //SetDestination(GameObject.Find("GraveyardedParent").transform.position);
        //transform.parent = GameObject.Find("GraveyardedParent").transform;
            SetDestination(new Vector3(3f, 0f, -49f));
///        else SetDestination(new Vector3(7f, 0f, -49f));
        ///sterg cartea de dinainte

        Graveyarded[] oldGraveyardedArray = FindObjectsOfType<Graveyarded>();
        foreach (Graveyarded oldGraveyarded in oldGraveyardedArray)
        {
            if (oldGraveyarded != null) //&& oldGraveyarded.gameObject.GetComponent<PlayingCard>().EchipaCarte == EchipaCarte)
            {
                Destroy(oldGraveyarded.gameObject, 0.5f);
                SpriteRenderer[] rends = oldGraveyarded.gameObject.GetComponents<SpriteRenderer>();
                foreach (SpriteRenderer sp in rends) sp.sortingOrder--;
            }
        }
        ///nu mai poate face hover
        Destroy(gameObject.GetComponent<Hover>());
        ///intorc cartea cu fata in sus
        if (echipaPlayer != EchipaCarte) Intoarce(true);
        
        gameObject.AddComponent<Graveyarded>();
    }
}