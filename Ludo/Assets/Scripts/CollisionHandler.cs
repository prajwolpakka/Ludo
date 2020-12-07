using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Transform origTrans;
    private List<Transform> playerList = new List<Transform>();

    private void Start()
    {
        origTrans = this.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerList.Add(collision.transform);
        if (playerList.Count == 1)
        {
            if (!GameManager.walkAnimationRunning)
            {
                collision.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        if (playerList.Count > 1)
        {
            foreach (Transform t in playerList)
            {
                t.GetComponent<Players>().mustShrink = true;
                t.localScale = new Vector2(0.75f, 0.75f);
            }
            if (playerList.Count == 2)
            {
                playerList[0].position = origTrans.position + new Vector3(0.2f, 0, 0);
                playerList[1].position = origTrans.position - new Vector3(0.2f, 0, 0);
            }
            if (playerList.Count == 3)
            {
                playerList[2].position = origTrans.position + new Vector3(0, 0.2f, 0);
            }
            if (playerList.Count == 4)
            {
                playerList[0].position = origTrans.position + new Vector3(0.2f, -0.2f, 0);
                playerList[1].position = origTrans.position - new Vector3(0.2f, -0.2f, 0);
                playerList[2].position = origTrans.position + new Vector3(0.25f, 0.2f, 0);
                playerList[3].position = origTrans.position - new Vector3(0.25f, 0.2f, 0);
            }
            Debug.Log("Something should happen here !");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerList.Remove(collision.transform);
    }
}
