using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMeleePowerUp : MonoBehaviour {

    public PlayerTwo playerTwo;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Mage");
        playerTwo = tempPlayer.GetComponent<PlayerTwo>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerTwo")
        {
            playerTwo.hasMeleeAtk = true;
            Destroy(gameObject);
        }

    }
}
