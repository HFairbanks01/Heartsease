using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{

    public float targetVelocity;
    public GameObject targetChest;
    public GroundingGame game;

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.up * targetVelocity * Time.deltaTime;
        if (Vector2.Distance(this.transform.position, targetChest.transform.position) <= 0.25 && targetVelocity <= 3 && Input.GetKeyDown(KeyCode.Q))
        {
            game.points += 1;
            Destroy(this.gameObject);
        }
        else if (Vector2.Distance(this.transform.position, targetChest.transform.position) <= 0.25 && targetVelocity >= 3 && Input.GetKeyDown(KeyCode.R))
        {
            game.points += 1;
            Destroy(this.gameObject);
        }
    }
}
