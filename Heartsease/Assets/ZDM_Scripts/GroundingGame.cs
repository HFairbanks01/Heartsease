using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingGame : MonoBehaviour
{

    public GameObject box;
    public GameObject square;
    public GameObject newBox;
    public GameObject leftChest;
    public GameObject rightChest;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float leftVelocity;
    public float rightVelocity;
    public int TickTime = 1;
    public float currentTime = 0;
    public int points;
    private float randomNumber;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime >= 1)
        {
            randomNumber = Random.Range(0, 2);
            
            if (randomNumber == 0)
            {
                newBox = Instantiate(square, leftSpawnPoint.position, leftSpawnPoint.rotation);
                newBox.GetComponent<Velocity>().targetVelocity = leftVelocity;
                newBox.GetComponent<Velocity>().targetChest = leftChest;
                newBox.GetComponent<Velocity>().game = this;
                currentTime = 0;
            }
            else if (randomNumber == 1)
            {
                newBox = Instantiate(square, rightSpawnPoint.position, rightSpawnPoint.rotation);
                newBox.GetComponent<Velocity>().targetVelocity = rightVelocity;
                newBox.GetComponent<Velocity>().targetChest = rightChest;
                newBox.GetComponent<Velocity>().game = this;
                currentTime = 0;
            }
            
        }

        
    }

}