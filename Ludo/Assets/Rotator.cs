using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 200.0f;
    public bool canRotate;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerTurnName == this.tag && !GameManager.allowDiceRoll && GameManager.diceRolledNumber == 6)
        {
            this.transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime);
        }
        else if (GameManager.playerTurnName == this.tag && !GameManager.allowDiceRoll && !this.GetComponentInParent<Players>().inside)
        {
            this.transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime);
        }
    }
}
