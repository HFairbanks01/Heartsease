using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingGame : MonoBehaviour
{

    public float playerPosition;
    public GameObject box;
    public GameObject square;
    //public Transform spawnPoint;
    public int TickTime = 1;
    private float currentTime = 0;
    private float randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime >= 1)
        {
            randomNumber = Random.Range(0, 1);
            if (randomNumber == 0)
            {
                
            }
            else if (randomNumber == 1)
            {

            }
            currentTime = 0;
        }
    }

    public void SpawnBox()
    {
        //var newBox = Instantiate(square, spawnPoint.position, spawnPoint.rotation);
    }
}
