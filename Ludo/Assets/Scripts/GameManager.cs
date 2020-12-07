using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int numOfPlayer = 4;
    public static int playerTurn = 1;
    public static int diceRolledNumber;
    public static bool allowDiceRoll = true;
    public static string playerTurnName = "RED";
    public Players[] players;
    private static readonly string[] names = { "RED", "GREEN", "YELLOW", "BLUE" };
    public GameObject[] dice;
    public GameObject[] turnIndicators;

    public static int redOutCount = 0;
    public static int greenOutCount = 0;
    public static int yellowOutCount = 0;
    public static int blueOutCount = 0;
    public static bool walkAnimationRunning = false;
    private int wayPointIndex = 0;

    //Update is called once per frame
    private void Start()
    {
        UpdateTurnWiseStuff();
    }
    void FixedUpdate()
    {
        if (walkAnimationRunning)
        {
            foreach (Players p in players)
            {
                if (p.moving && !allowDiceRoll)
                {
                    if (p.index + diceRolledNumber < p.WayPoints.Length)
                    {
                        Debug.Log("Possible");
                    }
                    p.transform.position = Vector3.MoveTowards(p.transform.position, p.WayPoints[p.index + wayPointIndex].position, 10.0f * Time.deltaTime);
                    if (p.transform.position == p.WayPoints[p.index + wayPointIndex].position)
                    {
                        if (wayPointIndex < diceRolledNumber)
                        {
                            wayPointIndex++;
                            Debug.Log(wayPointIndex);
                        }
                        else
                        {
                            p.moving = false;
                            p.canMove = false;
                            walkAnimationRunning = false;
                            p.index += wayPointIndex;
                            wayPointIndex = 0;
                            if (diceRolledNumber == 6)
                            {
                                allowDiceRoll = true;
                            }
                            else
                            {
                                allowDiceRoll = true;
                                NextPlayerTurn();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach (Players p in players)
            {
                if (p.tag == playerTurnName)
                {
                    p.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    if (p.canMove && !allowDiceRoll)
                    {
                        if (diceRolledNumber == 6 && p.inside)
                        {
                            p.transform.position = p.WayPoints[0].position;
                            allowDiceRoll = true;
                            p.canMove = false;
                            p.inside = false;
                            if (p.tag == "RED")
                            {
                                redOutCount++;
                            }
                            else if (p.tag == "GREEN")
                            {
                                greenOutCount++;
                            }
                            else if (p.tag == "YELLOW")
                            {
                                yellowOutCount++;
                            }
                            else if (p.tag == "BLUE")
                            {
                                blueOutCount++;
                            }
                        }
                        else if (!p.inside)
                        {
                            walkAnimationRunning = true;
                            p.moving = true;
                        }
                    }
                }
                else
                {
                    if (!p.mustShrink)
                    {
                        p.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                }
            }
            if (!allowDiceRoll && diceRolledNumber != 6)
            {
                if ((redOutCount == 0 && playerTurnName == "RED") || (greenOutCount == 0 && playerTurnName == "GREEN"))
                {
                    NextPlayerTurn();
                }
                else if ((yellowOutCount == 0 && playerTurnName == "YELLOW") || (blueOutCount == 0 && playerTurnName == "BLUE"))
                {
                    NextPlayerTurn();
                }
                foreach (Players p in players)
                {
                    if (playerTurnName == "RED" && !p.inside && redOutCount == 1 && p.tag == "RED" && diceRolledNumber != 6)
                    {
                        p.canMove = true;
                    }
                    if (playerTurnName == "GREEN" && !p.inside && greenOutCount == 1 && p.tag == "GREEN" && diceRolledNumber != 6)
                    {
                        p.canMove = true;
                    }
                    if (playerTurnName == "YELLOW" && !p.inside && yellowOutCount == 1 && p.tag == "YELLOW" && diceRolledNumber != 6)
                    {
                        p.canMove = true;
                    }
                    if (playerTurnName == "BLUE" && !p.inside && blueOutCount == 1 && p.tag == "BLUE" && diceRolledNumber != 6)
                    {
                        p.canMove = true;
                    }
                }
            }
        }
    }
    public void NextPlayerTurn()
    {
        dice[playerTurn - 1].SetActive(false);
        allowDiceRoll = true;
        playerTurn = playerTurn % numOfPlayer + 1;
        playerTurnName = names[playerTurn - 1];

        UpdateTurnWiseStuff();
    }
    private void UpdateTurnWiseStuff()
    {
        foreach (Players p in players)
        {

        }
        for (int i = 0; i < 4; i++)
        {
            if (dice[i].tag == playerTurnName)
            {
                dice[i].SetActive(true);
                turnIndicators[i].SetActive(true);
            }
            else
            {
                dice[i].SetActive(false);
                turnIndicators[i].SetActive(false);

            }
        }
    }

}
