using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    public int playerNum = 1;

    int currenthealth = 3;

    GameObject health1;
    GameObject health2;
    GameObject health3;

    PlayerOne playerOne;
    PlayerTwo playerTwo;

    void Start ()
    {
        health1 = GameObject.Find("P" + playerNum + "-Health");
        health2 = GameObject.Find("P" + playerNum + "-Health2");
        health3 = GameObject.Find("P" + playerNum + "-Health3");

        if(playerNum == 1)
        {
            GameObject playerObject = GameObject.Find("Warrior");
            playerOne = playerObject.GetComponent<PlayerOne>();
        }
        else
        {
            GameObject playerObject = GameObject.Find("Mage");
            playerTwo = playerObject.GetComponent<PlayerTwo>();
        }
        
    }
	
	
	void Update ()
    {
        if (playerNum == 1)
            currenthealth = playerOne.health;
        else
            currenthealth = playerTwo.health;


        switch (currenthealth)
        {
            case 3:
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(true);
                break;
            case 2:
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(false);
                break;
            case 1:
                health1.SetActive(true);
                health2.SetActive(false);
                health3.SetActive(false);
                break;
            case 0:
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(false);
                break;
            default:
                break;
        }
	}
}
