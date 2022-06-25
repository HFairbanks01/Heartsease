using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingCursor : MonoBehaviour
{
    public List<RectTransform> target;
    public RectTransform self;
    public bool hasStarted;

    public int index;

    bool isHold;

    public float holdRate;
    public float holdProgress;

    public PlayerController player;

    public Text instruction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            self.anchoredPosition = Vector3.MoveTowards(self.anchoredPosition, target[index].anchoredPosition, 180 * Time.deltaTime);
            if(Vector3.Distance(self.anchoredPosition, target[index].anchoredPosition) == 0)
            {
                if(index == 3 || index == 4 || index == 8 || index == 9)
                {
                    instruction.text = "Hold Z";
                    Debug.Log("Hold");
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        isHold = true;
                    }
                    if (Input.GetKeyUp(KeyCode.Z))
                    {
                        isHold = false;
                    }

                    if(holdProgress >= 100)
                    {
                        instruction.text = "";
                        if (index == 9)
                        {
                            Debug.Log("Done");
                            player.ChangeStress(-50);
                            hasStarted = false;
                            index = 0;
                            player.isBusy = false;
                            player.canMove = true;
                            player.breathingUI.SetActive(false);
                        }
                        else
                        {
                            isHold = false;
                            holdProgress = 0;
                            index += 1;
                        }
                    }
                }
                else
                {
                    instruction.text = "Press Z";
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Debug.Log("Press");
                        index += 1;
                        instruction.text = "";
                    }
                }

            }

            if (isHold)
            {
                holdProgress += holdRate * Time.deltaTime;
            }
            else
            {
                holdProgress = Mathf.Clamp(holdProgress - ((holdRate / 2) * Time.deltaTime), 0, 100);
            }
        }
    }

    public void Go(PlayerController targetPlayer)
    {
        hasStarted = true;
        player = targetPlayer;
    }
}
