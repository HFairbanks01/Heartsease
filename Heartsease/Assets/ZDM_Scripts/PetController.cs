using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayerController player;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.lastPoint.position, moveSpeed * Time.deltaTime);
    }
}
