using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] sides;
    private SpriteRenderer rend;
    AudioSource audioPlayer;
    bool diceRollCooldown = false;
    private void Start()
    {
        rend = this.GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();
        rend.sprite = sides[0];
    }
    private void OnMouseUpAsButton()
    {
        if (GameManager.allowDiceRoll && !diceRollCooldown && !GameManager.walkAnimationRunning)
        {
            StartCoroutine("RollDie");
            diceRollCooldown = true;
            Debug.Log("Dice Rolled!");
            Invoke("DiceCooldownReset", 0.8f);
        }
    }
    private IEnumerator RollDie()
    {
        audioPlayer.Play();
        int num = 0, prevNum = 10;
        for (int i = 0; i < 10; i++)
        {
            num = Random.Range(1, 7);
            while (num == prevNum)
            {
                num = Random.Range(1, 7);
            }
            //num = 6;
            rend.sprite = sides[num];
            prevNum = num;
            yield return new WaitForSeconds(0.04f);
        }
        GameManager.diceRolledNumber = num;
    }
    private void DiceCooldownReset()
    {
        diceRollCooldown = false;
        GameManager.allowDiceRoll = false;
    }
    public void ResetDice()
    {
        rend.sprite = sides[0];
    }
}
