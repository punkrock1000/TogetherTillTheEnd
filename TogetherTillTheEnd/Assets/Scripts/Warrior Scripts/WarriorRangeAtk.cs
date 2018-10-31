using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorRangeAtk : MonoBehaviour {

    float time = 2.0f;

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
        transform.Translate(Vector2.right * 10.0f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)    //For now deletes on any hit
    {
        if (col.gameObject.tag == "Monster") //Check for monster or object
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
        else       //If nothing uselful, delete
            Destroy(gameObject);
    }

}
