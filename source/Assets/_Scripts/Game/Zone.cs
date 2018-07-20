using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour {

    [SerializeField]
    private Material Selected;
    [SerializeField]
    private Color32[] Types;
    [SerializeField]
    private Text valueText;
    private Color32 defaultColor;

    private bool selected;

    private SpriteRenderer Rend;

    public int value { get; private set; }

    public void SetValues(int type, int v)
    {
        /// 0 - air, 1 - earth, 2 - water
        Rend = gameObject.GetComponent<SpriteRenderer>();
        Debug.Log("Setting " + type + " , " + v);
        defaultColor = Types[type];
        Rend.color = defaultColor;
        value = v;
        UpdateText();
    }

    public void UpdateValue(int val)
    {
        Debug.Log("Updating values");
        value = val;
        UpdateText();
    }

    public void SetSelected(bool ok)
    {
        if(ok)
        {
            Debug.Log("da");
            if (selected) return;
            Color color = defaultColor;
            color.r *= 0.5f;
            color.g *= 0.5f;
            color.b *= 0.5f;
            Rend.color = color;
            selected = true;
        }
        else
        {
            if (!selected) return;
            Rend.color= defaultColor;
            selected = false;
        }
    }

    public void UpdateText()
    {
        valueText.text = Mathf.Abs(value).ToString();
        if(value < 0)
        {
            valueText.enabled = true;
            valueText.color = Color.red;
        }
        else if(value > 0)
        {
            valueText.enabled = true;
            valueText.color = Color.green;
        }
        else
        {
            valueText.enabled = false;
            //valueText.color = Color.black;
        }
    }
    public void AddValue(int x)
    {
        value += x;
        UpdateText();
        GameObject.Find("Tiles").GetComponent<TileManager>().RefreshValues();
    }
}