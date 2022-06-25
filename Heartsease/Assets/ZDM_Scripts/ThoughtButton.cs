using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtButton : MonoBehaviour
{
    public Text thoughtText;
    public string PositiveThought;

    public bool isPositive = true;

    public PlayerController player;

    public void SetUp(string Positive, string Negative, PlayerController newPlayer)
    {
        player = newPlayer;
        PositiveThought = Positive;
        thoughtText.text = Negative;
        isPositive = false;
    }

    public void Change()
    {
        player.ChangeStress(-2);
        thoughtText.text = PositiveThought;
        isPositive = true;
    }
}
