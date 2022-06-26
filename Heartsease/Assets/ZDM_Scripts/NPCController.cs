using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool hasHeart;
    public bool heartConsumed;

    public string npc_Name;

    public Animator anime;
    public void Interact(PlayerController player)
    {
        if (!hasHeart)
        {
            anime.Play("Talking");
            player.ChangeHearts(0.25f);
            player.ChangeStress(5f);
        }

        /*
        if (hasHeart)
        {
            if (!heartConsumed)
            {
                player.ChangeHearts(0.25f);
                heartConsumed = true;
            }
            else
            {
                player.ChangeHearts(0.25f);
                player.ChangeHeartease(10f);
            }
        }
        else
        {
            player.ChangeHearts(0.25f);
            player.ChangeStress(5f);
        }
        */
    }
}
