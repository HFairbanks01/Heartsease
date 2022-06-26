using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingGame : MonoBehaviour
{

    public GameObject qSquare;
    public GameObject rSquare;
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

    void Start()
    {
        leftChest.SetActive(true);
        rightChest.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime >= 1)
        {
            randomNumber = Random.Range(0, 2);
            
            if (randomNumber == 0 && points < 3)
            {
                newBox = Instantiate(qSquare, leftSpawnPoint.position, leftSpawnPoint.rotation);
                newBox.GetComponent<Velocity>().targetVelocity = leftVelocity;
                newBox.GetComponent<Velocity>().targetChest = leftChest;
                newBox.GetComponent<Velocity>().game = this;
                currentTime = 0;
            }
            else if (randomNumber == 1 && points < 3)
            {
                newBox = Instantiate(rSquare, rightSpawnPoint.position, rightSpawnPoint.rotation);
                newBox.GetComponent<Velocity>().targetVelocity = rightVelocity;
                newBox.GetComponent<Velocity>().targetChest = rightChest;
                newBox.GetComponent<Velocity>().game = this;
                currentTime = 0;
            }
            
            if (points >= 3)
            {
                Destroy(leftChest);
                Destroy(rightChest);
            }
        }

        
    }

}