using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayerController player;
    public bool hasHeart;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.lastPoint.position, moveSpeed * Time.deltaTime);
    }

    public void Interact(PlayerController player)
    {
        if(player.playerHearts >= 1)
        {
            hasHeart = true;
            player.ChangeHearts(-1);
            player.ChangeHeartease(10f);
        }
        else{

        }
    }
}
