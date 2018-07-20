using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    Vector3 initialPosition;
    string echipa;

    public bool shouldWork;
    public bool hovering;
    public bool dragging;

    private PlayingCard card;

    private Vector3 offset;
    private Vector3 screenPosition;

    public GameObject GameManager;
    GameObject preview;

    [SerializeField]
    private GameObject selectedZone;

    public void ResetInitialValues()
    {
        initialPosition = transform.position;
    }
    public void ResetInitialValues(Vector3 pos)
    {
        initialPosition = pos;
    }

    /// <summary>
    /// initialization
    /// </summary>
    void Start ()
    {
        GameManager = GameObject.Find("GameManager");
        preview = GameObject.Find("Preview");
        echipa = (string)PhotonNetwork.player.CustomProperties["Echipa"];
        ResetInitialValues();
        card = gameObject.GetComponent<PlayingCard>();
	}


    /// <summary>
    /// actual moving logic, based on the state of the card.
    /// </summary>
    private void Update()
    {
        ///click dreapta pentru preview
        if ((Input.GetMouseButtonDown(1)  || (Input.GetMouseButtonUp(0)) && !dragging) && hovering)
        {
            if (card == null) card = gameObject.GetComponent<PlayingCard>();
            if(card.EchipaCarte == echipa) preview.GetComponent<hideRightClick>().SetCard(card.data);
        }
        ///daca lasa cartea din mana
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;
            ///aplic efect pe zona
            if (selectedZone != null)
            {
                ///verific daca poate sa joace cartea
                Debug.Log("Trying to apply effect.");
                    if (GameManager.GetComponent<TurnManager>().isMyTurn)
                    {
                        Debug.Log("It's my turn, applying effect");
                        Debug.Log("ID: " + card.GetComponent<PlayingCard>().ID + "Type: " + card.GetComponent<PlayingCard>().type + 
                            "spells left:" + GameManager.GetComponent<TurnManager>().SpellsLeftThisTurn + "actions left" + GameManager.GetComponent<TurnManager>().ActionsLeftThisTurn);
                        if (card.GetComponent<PlayingCard>().type == 1 &&
                            GameManager.GetComponent<TurnManager>().TryToPlaySpell())
                        {
                            Debug.Log("Applying effect " + card.GetComponent<PlayingCard>().ID);
                            switch(card.GetComponent<PlayingCard>().ID)
                            {
                            case 0:
                                //calatorie in timp
                                GameManager.GetComponent<TurnManager>().Set_quant(2);
                                //selectedZone.GetComponent<Zone>().AddValue(card.value);
                                break;
                            case 1:
                                //oprire timp
                                GameManager.GetComponent<TurnManager>().Set_quant(0);
                                break;
                            case 2:
                                //actiune extra
                                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                {
                                    //GameManager.GetComponent<TurnManager>().AddSpellsLeft(1);
                                    GameManager.GetComponent<TurnManager>().AddActionsLeft(1);
                                }
                                else GameManager.GetComponent<TurnManager>().AddSpellsLeft(2); //1 pentru cartea data, si inca una

                                break;
                            case 3:
                                GameObject.Find("Cards").GetComponent<CardsManager>().DrawCard();
                                break;
                            case 4:
                                ///daca celalalt jucator are cel putin o carte in mana
                                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if(GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 5:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 6:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 7:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 8:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 9:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                break;
                            case 10:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    if (GameObject.Find("GameManager").GetComponent<Statistics>().Mana_Natura.Count > 0)
                                        GameManager.GetComponent<TurnManager>().ChangeData((Random.Range(1, 100) % GameManager.GetComponent<Statistics>().Mana_Natura.Count), card.ID);
                                }
                                
                                break;
                            case 11:
                                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Poluare")
                                {
                                    GameObject.Find("Parametri").GetComponent<Parametri>().IncearcaScadeHP();
                                }
                                break;
                        }
                        selectedZone.GetComponent<Zone>().SetSelected(false);
                        card.DestroyCard();
                        }
                        else if (card.GetComponent<PlayingCard>().type == 2 &&
                            GameManager.GetComponent<TurnManager>().TryToPlayAction())
                        {
                            Debug.Log("Applying effect " + card.GetComponent<PlayingCard>().ID);
                        switch (card.GetComponent<PlayingCard>().ID)
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                GameObject.Find("Cards").GetComponent<CardsManager>().DrawCard();
                                break;
                            case 4:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame,card.data.valueStres,card.data.valueIgiena);
                                break;
                            case 5:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                            case 6:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                            case 7:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                            case 8:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                            case 9:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                            case 10:
                                if ((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                                    GameObject.Find("Parametri").GetComponent<Parametri>().AddToStats(card.data.valueFoame, card.data.valueStres, card.data.valueIgiena);
                                break;
                        }
                        selectedZone.GetComponent<Zone>().SetSelected(false);
                        card.DestroyCard();
                    }
                    }
            }
        }
        if(dragging)
        {
            
            if(echipa == card.EchipaCarte)
            {
                Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
                transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
            }
            
        }
        else if (hovering)
        {
            if(card.EchipaCarte == echipa)
            transform.position = Vector3.Lerp(transform.position, initialPosition + new Vector3(0, 0.69f, 0), 0.3f);
            else
            transform.position = Vector3.Lerp(transform.position, initialPosition - new Vector3(0, 0.69f, 0), 0.3f);
        }
    }

    /// <summary>
    /// hovering
    /// </summary>
    void OnMouseOver()
    {
        if (!shouldWork) return;
        hovering = true;
    }

    /// <summary>
    /// resets everything upon moving the mouse away
    /// </summary>
    void OnMouseExit()
    {
        initialPosition = card.GetDestination();
        hovering = false;
        transform.position = initialPosition;
    }

    /// <summary>
    /// clicking initialization for dragging
    /// </summary>
    void OnMouseDown()
    {
        if (!shouldWork) return;
        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
    }

    /// <summary>
    /// for dragging with the mouse
    /// </summary>
    private void OnMouseDrag()
    {
        if (!shouldWork) return;
        hovering = false;
        dragging = true;
    }

    /// <summary>
    /// moving the card on a zone
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Zone"))
        {
            if(selectedZone == null)
            {
                selectedZone = collision.gameObject;
            }
            else
            {
                if(Vector3.Distance(transform.position,collision.transform.position) < Vector3.Distance(transform.position, selectedZone.transform.position))
                {
                    selectedZone.GetComponent<Zone>().SetSelected(false);
                    selectedZone = collision.gameObject;
                }
            }
            selectedZone.GetComponent<Zone>().SetSelected(true);
        }
    }

    /// <summary>
    /// moving a card away from a zone
    /// </summary>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Zone"))
        {
            collision.transform.GetComponent<Zone>().SetSelected(false);
            if(selectedZone == collision.gameObject)
            {
                selectedZone = null;
            }
        }
    }
}
