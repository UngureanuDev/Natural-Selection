using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class hideRightClick : MonoBehaviour
{
    private CardData data;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image artworkImage;
    public GameObject frontImage;
    public GameObject manaImage;
    public Text costText;

    private bool hovering;
    public GameObject cardObj;

    public void SetCard(CardData card)
    {
        nameText.SetText(card.name);
        descriptionText.SetText(card.description);
        artworkImage.sprite = card.artwork;
        costText.text = card.cost.ToString();
        frontImage.GetComponent<SpriteRenderer>().sprite = card.front;
        manaImage.GetComponent<Image>().sprite = card.manaImage;
        cardObj.SetActive(true);
    }

    private void Update()
    {
        if ((Input.GetMouseButton(1) || Input.GetMouseButton(0)) && hovering) cardObj.SetActive(false);
    }

    void OnMouseOver()
    {
        //Debug.Log("Hovering");
        hovering = true;
    }

    /// <summary>
    /// resets everything upon moving the mouse away
    /// </summary>
    void OnMouseExit()
    {
        hovering = false;
    }
}