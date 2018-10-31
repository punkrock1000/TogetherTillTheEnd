using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBerserk : MonoBehaviour {

    public GameObject PlayerOne;

    int frames = 2;

    void Start()
    {
        PlayerOne = GameObject.Find("Warrior");
    }

    void FixedUpdate()
    {
        while (false)
        {
            transform.Translate(Vector2.left * .2f);

        }

        PlayerOne.transform.position = this.transform.position;
        Destroy(gameObject);
    }

}
