using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Transform[] WayPoints;
    public bool inside = true;
    public bool canMove = false;
    public bool moving = false;
    public int index;
    public Transform initialPoint;
    private GameObject rotatingDisk;
    private BoxCollider2D gameCollider;
    public bool mustShrink = false;
    private void Start()
    {
        initialPoint = this.transform;
        rotatingDisk = GetComponentInChildren<Rotator>().gameObject;
        gameCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (GameManager.playerTurnName == this.tag)
        {
            gameCollider.enabled = true;
            rotatingDisk.SetActive(true);
        }
        else
        {
            if (!GameManager.allowDiceRoll)
            {
                gameCollider.enabled = false;
            }
            rotatingDisk.SetActive(false);
        }
    }
    private void OnMouseUpAsButton()
    {
        if (GameManager.playerTurnName == this.tag && !GameManager.allowDiceRoll)
        {
            canMove = true;
            Debug.Log("Clicked!!");
        }
    }
}
