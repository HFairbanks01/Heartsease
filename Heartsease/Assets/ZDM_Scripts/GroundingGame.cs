using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingGame : MonoBehaviour
{

    public GameObject box;
    public GameObject square;
    public GameObject newBox;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float leftVelocity;
    public float rightVelocity;
    public int TickTime = 1;
    public float currentTime = 0;
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
            randomNumber = Random.Range(0, 2);
            Debug.Log("Worked");
            
            if (randomNumber == 0)
            {
                newBox = Instantiate(square, leftSpawnPoint.position, leftSpawnPoint.rotation);
                newBox.transform.position -= Vector3.up * leftVelocity * Time.deltaTime;
                currentTime = 0;
            }
            else if (randomNumber == 1)
            {
                newBox = Instantiate(square, rightSpawnPoint.position, rightSpawnPoint.rotation);
                newBox.transform.position -= Vector3.up * rightVelocity * Time.deltaTime;
                currentTime = 0;
            }
            
        }

        
    }

}