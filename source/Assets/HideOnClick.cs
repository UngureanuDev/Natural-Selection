using UnityEngine;

public class HideOnClick : MonoBehaviour
{

    public bool hideOnRightClick;
    public bool hideOnLeftClick;
    private bool hovering;

    private void Update()
    {
        if (hideOnRightClick && Input.GetMouseButton(1) && hovering) gameObject.SetActive(false);
        if (hideOnLeftClick && Input.GetMouseButton(0) && hovering) gameObject.SetActive(false);
    }

    void OnMouseOver()
    {
        hovering = true;
    }
    void OnMouseExit()
    {
        hovering = false;
    }
}