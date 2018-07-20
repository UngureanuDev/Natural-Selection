using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardData : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite front;
    public Sprite artwork;
    public Sprite back;
    public Sprite manaImage;

    public int cost;
    /// <summary>
    /// 1 - spell
    /// 2 - action
    /// </summary>
    public int type;
    public int valueFoame;
    public int valueStres;
    public int valueIgiena;
    public int ID_Swap;

}