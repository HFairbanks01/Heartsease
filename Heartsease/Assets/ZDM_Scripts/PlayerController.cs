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
    public LayerMask interactLayer;
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

    public List<ThoughtButton> thoughts;
    public List<string> negativeText;
    public List<string> positiveText;

    int random;

    public Animator stressMinorDarkness;
    public Animator stressMinorLight;

    bool stressed_Minor;

    public GameObject needle;
    public Quaternion needleTarget;

    public GameObject alleyUI;

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
            if (hit = Physics2D.OverlapCircle(this.transform.position + facingDirection, .2f, interactLayer))
            {
                if(hit.gameObject.tag == "Alleyway")
                {
                    alleyUI.SetActive(true);
                }
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

        if (Input.GetKeyDown(KeyCode.X) && !isBusy)
        {

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeStress(1);
        }


        needle.transform.rotation = Quaternion.RotateTowards(needle.transform.rotation, needleTarget, 10 * Time.deltaTime);
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
        //stressMeter.value = playerStress;
        needleTarget = Quaternion.Euler(0, 0, Mathf.Lerp(90, -90, playerStress / 100));
        if(playerStress >= 100f)
        {
            for (int i = 0; i < thoughts.Count; i++)
            {
                thoughts[i].gameObject.SetActive(false);
            }
            stressMinorLight.Play("VanishLight");
            StartCoroutine(Save());
        }
        else if(playerStress >= 90f)
        {
            if (!stressed_Minor)
            {
                stressMinorDarkness.Play("Fade");
                stressMinorLight.Play("FadeLight");
                stressed_Minor = true;
                StartCoroutine(Stressing());
            }
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

    public IEnumerator Stressing()
    {
        ChangeStress(1);
        random = Random.Range(0, thoughts.Count);
        if (thoughts[random].isPositive)
        {
            thoughts[random].gameObject.SetActive(true);
            thoughts[random].SetUp(positiveText[Random.Range(0, positiveText.Count)], negativeText[Random.Range(0, negativeText.Count)], this);
        }
        else
        {
            random = Random.Range(0, thoughts.Count);
            if (thoughts[random].isPositive)
            {
                thoughts[random].gameObject.SetActive(true);
                thoughts[random].SetUp(positiveText[Random.Range(0, positiveText.Count)], negativeText[Random.Range(0, negativeText.Count)], this);
            }
        }
        yield return new WaitForSecondsRealtime(1);
        if(playerStress >= 90 && playerStress < 100)
        {
            StartCoroutine(Stressing());
        }
        else if(playerStress < 100)
        {
            for(int i = 0; i < thoughts.Count; i++)
            {
                thoughts[i].gameObject.SetActive(false);
            }
            stressMinorDarkness.Play("Vanish");
            stressMinorLight.Play("VanishLight");
        }
    }

    public IEnumerator Save()
    {
        //Play Animation
        yield return new WaitForSecondsRealtime(3);
        ChangeStress(-25);
        ChangeHearts(-1);
    }

    public void AlleyYes()
    {
        random = Random.Range(0, 10);
        if(random > 5)
        {
            ChangeStress(25);
        }
    }

    public void AlleyNo()
    {
        alleyUI.SetActive(false);
    }
}