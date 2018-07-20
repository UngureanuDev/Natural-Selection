using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventPopup : MonoBehaviour
{

    public GameObject EventObj;
    public GameObject Titlu;
    public GameObject Descriere;


    public void PopUp(string titlu, string descriere, float timp)
    {
        Titlu.GetComponent<TextMeshProUGUI>().SetText(titlu);
        Descriere.GetComponent<TextMeshProUGUI>().SetText(descriere);
        Invoke("activeaza", timp);
    }

    void activeaza()
    {
        EventObj.SetActive(true);
    }
}