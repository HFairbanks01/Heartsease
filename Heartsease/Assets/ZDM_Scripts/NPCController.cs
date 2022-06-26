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
        if(player.playerHearts >= 1)
        {
            hasHeart = true;
            player.ChangeHearts(-1);
            player.ChangeHeartease(10f);
            StartCoroutine(Love(player));
        }
        else if (!hasHeart)
        {
            StartCoroutine(Talk(player));
        }
        else
        {
            StartCoroutine(Love(player));
        }
    }

    public IEnumerator Love(PlayerController target)
    {
        anime.gameObject.SetActive(true);
        anime.Play("Talking_Good");
        target.canMove = false;
        target.isBusy = true;
        yield return new WaitForSecondsRealtime(2.5f);
        anime.gameObject.SetActive(false);
        target.canMove = true;
        target.isBusy = false;
    }

    public IEnumerator Talk(PlayerController target)
    {
        target.canMove = false;
        target.isBusy = true;
        anime.gameObject.SetActive(true);
        anime.Play("Talking_Bad");
        target.ChangeHearts(0.25f);
        target.ChangeStress(5f);
        yield return new WaitForSecondsRealtime(2.5f);
        anime.gameObject.SetActive(false);
        target.canMove = true;
        target.isBusy = false;
    }
}
