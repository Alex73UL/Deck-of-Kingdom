using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defence;
    public bool CanAttack;

    public Card(string name, string logoPath, int attack, int defence)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defence = defence;
        CanAttack = false;
    }

    public void ChangeAttackState(bool can)
    {
        CanAttack = can;
    }
}
public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("zubenko", "Sprites/Cards/zubenko", 5, 5));
        CardManager.AllCards.Add(new Card("antonio", "Sprites/Cards/antonio", 4, 3));
        CardManager.AllCards.Add(new Card("elon", "Sprites/Cards/elon", 3, 3));
        CardManager.AllCards.Add(new Card("imagine", "Sprites/Cards/imagine", 2, 1));
        CardManager.AllCards.Add(new Card("ricardo", "Sprites/Cards/ricardo", 5, 2));
    }

}
