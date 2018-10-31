﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBerserkPowerUp : MonoBehaviour
{

    public Warrior playerOne;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Warrior");
        playerOne = tempPlayer.GetComponent<Warrior>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
             Destroy(gameObject);
        }

    }

}