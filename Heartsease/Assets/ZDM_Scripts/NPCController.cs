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
            StartCoroutine(Talk(player));
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

    public IEnumerator Talk(PlayerController target)
    {
        anime.gameObject.SetActive(true);
        anime.Play("Talking_Bad");
        target.ChangeHearts(0.25f);
        target.ChangeStress(5f);
        yield return new WaitForSecondsRealtime(2.5f);
        anime.gameObject.SetActive(false);
    }
}
