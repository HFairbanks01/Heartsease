using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerHearts;
    public float playerHeartease;
    public float playerStress;

    public Slider stressMeter;

    public float moveSpeed= 7f;
    public float movePenalty = 0f;
    public Transform nextPoint, lastPoint;
    public LayerMask obstacleLayer;

    public Vector3 facingDirection;
    public LayerMask npcLayer;

    Collider2D hit;

    public float cryDuration;

    public float stressPenalty;

    public bool isBusy = false;
    public bool canMove = true;

    public GameObject checkInBox;
    public Text checkInText;
    public List<string> checkInLines;

    public GameObject breathingUI;
    public BreathingCursor breathingCursor;

    public GameObject node;

    public List<string> negativeText;
    public List<string> positiveText;

    int random;

    void Update()
    {
        Debug.Log(canMove);
        transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, (moveSpeed - movePenalty) * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPoint.position) == 0 && canMove)
        {

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(nextPoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, obstacleLayer))
                {
                    lastPoint.position = nextPoint.position;
                    nextPoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    facingDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(nextPoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, obstacleLayer))
                {
                    lastPoint.position = nextPoint.position;
                    nextPoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    facingDirection = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (hit = Physics2D.OverlapCircle(this.transform.position + facingDirection, .2f, npcLayer))
            {
                hit.gameObject.GetComponent<NPCController>().Interact(this);
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && !isBusy)
        {
            if(playerStress < 75)
            {
                isBusy = true;
                if(playerStress < 50)
                {
                    random = Random.Range(0, checkInLines.Count);
                    checkInText.text = checkInLines[random];
                    checkInBox.SetActive(true);
                }
                else
                {
                    checkInText.text = "I feel stressed. Maybe I should try Breathing or Grounding";
                    checkInBox.SetActive(true);
                }
            }
            else
            {
                StartCoroutine(Cry());
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && !isBusy)
        {
            isBusy = true;
            canMove = false;
            breathingUI.SetActive(true);
            breathingCursor.Go(this);
        }
    }

    public void ChangeHearts(float value)
    {
        float heartCheck = playerHearts;
        while(heartCheck >= 1)
        {
            heartCheck -= 1;
        }
        if(heartCheck + value >= 1)
        {
            ChangeHeartease(10f);
        }
        playerHearts = Mathf.Clamp(playerHearts + value, 0, 5);
        
    }

    public void ChangeHeartease(float value)
    {
        playerHeartease = Mathf.Clamp(playerHeartease + value, 0, 100);
    }

    public void ChangeStress(float value)
    {
        playerStress = Mathf.Clamp(playerStress + stressPenalty + value, 0, 100);
        stressMeter.value = playerStress;
        if(playerStress >= 100f)
        {

        }else if(playerStress >= 90f)
        {

        }
    }

    public IEnumerator Cry()
    {
        isBusy = true;
        movePenalty = 2f;
        stressPenalty = 5f;
        yield return new WaitForSecondsRealtime(cryDuration);
        movePenalty = 0f;
        stressPenalty = 0f;
        isBusy = false;
    }

    public void closeCheckIn()
    {
        checkInBox.SetActive(false);
        isBusy = false;
    }
}