using UnityEngine;

public class Graveyarded : MonoBehaviour
{
    private bool hovering;

    private void OnMouseEnter()
    {
        hovering = true;
    }
    private void OnMouseExit()
    {
        hovering = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && hovering)
        {
            PlayingCard card = gameObject.GetComponent<PlayingCard>();
            GameObject.Find("Preview").GetComponent<hideRightClick>().SetCard(card.data);
        }
    }
}