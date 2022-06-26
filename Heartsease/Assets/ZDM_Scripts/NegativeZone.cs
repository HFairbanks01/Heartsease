using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeZone : MonoBehaviour
{
    public PlayerController targetPlayer;

    public float zoneRange;

    public bool isActive, isCycling;

    void Update()
    {
        if (Vector2.Distance(this.transform.position, targetPlayer.transform.position) <= zoneRange)
        {
            if (!isActive)
            {
                if (!isCycling)
                {
                    StartCoroutine(HarmCycle());
                }
                isActive = true;
            }
        }
        else
        {
            isActive = false;
        }
    }

    public IEnumerator HarmCycle()
    {
        yield return new WaitForSecondsRealtime(2);

        if (isActive)
        {
            if (targetPlayer.playerStress < 90)
            {
                targetPlayer.ChangeStress(5);
            }
            isCycling = true;
            StartCoroutine(HarmCycle());
        }
        else
        {
            isCycling = false;
        }
    }
}
