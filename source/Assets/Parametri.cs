using UnityEngine;
using TMPro;

public class Parametri : MonoBehaviour
{

    public int Foame;
    public int Stres;
    public int Igiena;
    public int ProgresFoame;
    public int ProgresStres;
    public int ProgresIgiena;
    public int HP;

    public GameObject TextFoame;
    public GameObject TextStres;
    public GameObject TextIgiena;
    public GameObject TextProgresFoame;
    public GameObject TextProgresStres;
    public GameObject TextProgresIgiena;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public GameObject failParticles;
    public GameObject successParticles;

    public GameObject GameManager;

    private void Start()
    {
        HP = 3;
        if((string)PhotonNetwork.player.CustomProperties["Echipa"] == "Natura") GameManager.GetComponent<TurnManager>().SyncParametri(20,20,20,-1,-1,-1);
        Update_UI();
    }

    public void ApplyChangesParameter(int quant)
    {
        Igiena = Igiena + quant * ProgresIgiena;
        Stres = Stres + quant * ProgresStres;
        Foame = Foame + quant * ProgresFoame;
        ProgresFoame = -1;
        ProgresIgiena = -1;
        ProgresStres = -1;
        GameManager.GetComponent<TurnManager>().SyncParametri(Foame, Stres, Igiena, ProgresFoame, ProgresStres, ProgresIgiena);
        Update_UI();
    }

    public void AddToStats(int addFoame, int addStres, int addIgiena)
    {
        /// am schimbat din Foame in ProgresFoame, si asa mai departe.
        ProgresFoame += addFoame;
        ProgresStres += addStres;
        ProgresIgiena += addIgiena;
        GameManager.GetComponent<TurnManager>().SyncParametri(Foame, Stres, Igiena,ProgresFoame, ProgresStres,ProgresIgiena);
    }

    public void SyncParameters(int foame, int stres, int igiena, int progresFoame, int progresStres, int progresIgiena)
    {
        Foame = foame;
        Stres = stres;
        Igiena = igiena;
        ProgresFoame = progresFoame;
        ProgresStres = progresStres;
        ProgresIgiena = progresIgiena;
        Update_UI();
    }

    public void ScadeHP(int boala)
    {
        ///animatie de reusita
        ///afisez pe ecran, in functie de boala
        switch (boala)
        {
            case -1: ///da fail
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack failed", "The spell cast by your enemy did not affect you.", 2);
                }
                else
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack failed", "You did not affect the enemy with the spell.", 2);
                }
                
                break;
            case 0:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "You’ve been infected with Hepatitis A. Hepatitis A is a contagious disease, which appears in conditions of poor, inadequate hygiene. You have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has been infected with Hepatitis A. Hepatitis A is a contagious disease, which appears in conditions of poor, inadequate hygiene. The opponent has lost a life.", 2);
                }
                HP--;
                ///hepatita
                break;
            case 1:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "Your stress level and alimentation led you to diabetes. It's one of the most widespread transmissible chronic diseases. The body does no longer produce insuline, or it doesn't use it efficiently. You  have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has contacted diabetes. It's one of the most widespread transmissible chronic diseases. The body does no longer produce insuline, or it doesn't use it efficiently. The opponent has lost a life.", 2);
                }
                HP--;
                ///diabet
                break;
            case 2:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The high level of stress caused you a heart attack. A heart attack occurs when blood flow decreases or stops to a part of the heart, often leading to death. You have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has suffered a heart attack. A heart attack occurs when blood flow decreases or stops to a part of the heart, often leading to death. The opponent has lost a life.", 2);
                }
                HP--;
                ///infarct
                break;
            case 3:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "Your high level of stress lead to the development of Alzheimer's disease. Alzheimer’s is a brain disease that slowly destroys brain cells. Gradually, the person loses his remembering capacity, bodily functions are lost, and it leads to dementia and ultimately to death. You have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has contacted Alzheimer's disease. Alzheimer’s is a brain disease that slowly destroys brain cells. Gradually, the person loses his remembering capacity, bodily functions are lost, and it leads to dementia and ultimately to death. The opponent has lost a life.", 2);
                }
                HP--;
                ///alzheimer
                break;
            case 4:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "Your stress level and alimentation lead you to obesity. Obesity is the condition of being much too heavy for one’s height, so that one’s health is affected. You have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has developed obesity. Obesity is the condition of being much too heavy for one’s height, so that one’s health is affected. The opponent has lost a life", 2);
                }
                HP--;
                ///obezitate
                break;
            case 5:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The stress that you experience and your unhealthy diet lead you to a condition called gastritis. The gastritis is the inflammation of the gastric mucosa, manifested through pain and bleeding. You have lost a life", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has contacted gastritis. The gastritis is the inflammation of the gastric mucosa, manifested through pain and bleeding. The opponent has lost a life.", 2);
                }
                HP--;
                ///gastrita
                break;
            case 6:
                if ((string) PhotonNetwork.player.CustomProperties["Echipa"] == "Natura")
                {
                    Destroy(Instantiate(failParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The unhealthy diet, rich in E’s, has caused gastric ulcer. The gastric ulcer is a lesion on the digestive lining of the stomach, manifested through pain, burns and cramps. You have lost a life.", 2);
                }
                else
                {
                    Destroy(Instantiate(successParticles, Vector3.zero, Quaternion.identity, transform), 10f);
                    GameManager.GetComponent<EventPopup>().PopUp("Attack successful", "The enemy has contacted gastric ulcer. The gastric ulcer is a lesion on the digestive lining of the stomach, manifested through pain, burns and cramps. The opponent has lost a life.", 2);
                }
                HP--;
                ///ulcer
                break;
        }
        ///daca s-a scazut
        if (boala != -1)
        {
            if (HP == 2)
            {
                Heart3.AddComponent<Rigidbody2D>();
                Destroy(Heart3, 5);
            }
            else if (HP == 1)
            {
                Heart2.AddComponent<Rigidbody2D>();
                Destroy(Heart2, 5);
            }
            else if (HP == 0)
            {
                Heart1.AddComponent<Rigidbody2D>();
                Destroy(Heart1, 5);
                ///gameover, castiga boala
                Invoke("PollutionWin",2f);
            }
        }
        Update_UI();
    }

    void PollutionWin()
    {
        GameManager.GetComponent<TurnManager>().BroadcastGameOver("Poluare",1);
    }

    public void IncearcaScadeHP()
    {
        ///verific care parametru ii mai mic, si atac in functie de ala
        int minim;
        if (Foame < Stres) minim = Foame;
        else minim = Stres;
        if (minim > Igiena) minim = Igiena;

        if (minim > 20) ///ii sanatos
        {
            ///animatie de fail
            GameManager.GetComponent<TurnManager>().scadeHp(-1);
            return;
        }
        int sansa = (20 - minim) * 5;
        if (Random.Range(1, 100) > sansa)
        {
            ///animatie de fail
            GameManager.GetComponent<TurnManager>().scadeHp(-1);
            return;
        }


        int boala = 0;
        ///aleg o boala random
        if (minim == Igiena)
        {
            ///hepatita, 
            boala = 0;
        }
        else if (minim == Stres)
        {
            ///diabet, infarct, Alzheimer
            boala = Random.Range(1, 100) % 3 + 1; ///1,2,3
        }
        else if (minim == Foame)
        {
            ///obezitate, gastrita, ulcer
            boala = Random.Range(1, 100) % 3 + 4; ///4,5,6
        }
        GameManager.GetComponent<TurnManager>().scadeHp(boala);
        return;
    }

    public void Update_UI()
    {
        Debug.Log("Updating UI");
        /*
        TextFoame.GetComponent<TextMeshProUGUI>().SetText(Foame.ToString());
        TextStres.GetComponent<TextMeshProUGUI>().SetText(Stres.ToString());
        TextIgiena.GetComponent<TextMeshProUGUI>().SetText(Igiena.ToString());
        if(ProgresFoame >= 0) TextProgresFoame.GetComponent<TextMeshProUGUI>().SetText("+" + ProgresFoame.ToString() + ")");
        else TextProgresFoame.GetComponent<TextMeshProUGUI>().SetText("(" + ProgresFoame.ToString() + ")");
        if (ProgresStres >= 0) TextProgresStres.GetComponent<TextMeshProUGUI>().SetText("(+" + ProgresStres.ToString() + ")");
        else TextProgresStres.GetComponent<TextMeshProUGUI>().SetText("(" + ProgresStres.ToString() + ")");
        if (ProgresIgiena >= 0) TextProgresIgiena.GetComponent<TextMeshProUGUI>().SetText("(+" + ProgresIgiena.ToString() + ")");
        else TextProgresIgiena.GetComponent<TextMeshProUGUI>().SetText("(" + ProgresIgiena.ToString() + ")");
        */
        if (ProgresFoame >= 0) TextFoame.GetComponent<TextMeshProUGUI>().SetText(Foame.ToString() + " (+" + ProgresFoame.ToString() +")");
        else TextFoame.GetComponent<TextMeshProUGUI>().SetText(Foame.ToString() + " (" + ProgresFoame.ToString() + ")");

        if (ProgresIgiena >= 0) TextIgiena.GetComponent<TextMeshProUGUI>().SetText(Igiena.ToString() + " (+" + ProgresIgiena.ToString() +")");
        else TextIgiena.GetComponent<TextMeshProUGUI>().SetText(Igiena.ToString() + " (" + ProgresIgiena.ToString() + ")");

        if (ProgresStres >= 0) TextStres.GetComponent<TextMeshProUGUI>().SetText(Stres.ToString() + " (+" + ProgresStres.ToString() +")");
        else TextStres.GetComponent<TextMeshProUGUI>().SetText(Stres.ToString() + " (" + ProgresStres.ToString() + ")");
        /*
        if (HP >= 1) Heart1.SetActive(true);
        else Heart1.SetActive(false);
        if (HP >= 2) Heart2.SetActive(true);
        else Heart2.SetActive(false);
        if (HP >= 3) Heart3.SetActive(true);
        else Heart3.SetActive(false);
        */


    }
}