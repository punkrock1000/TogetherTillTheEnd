using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactActivator : Activator
{

    //list of object that are colliding 
    //(to make sure both player influencing the same swith does not create irregularities) 
    List<GameObject> currentlyColliding;

    //how many player are triggering the switch
    public int nbrOfPlayerTriggering;

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        nbrOfPlayerTriggering = 0;
        currentlyColliding = new List<GameObject>();
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            triggered = true;
            nbrOfPlayerTriggering++;
            Notify(triggered);
            currentlyColliding.Add(collision.gameObject);
        }
    }

    override protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            currentlyColliding.Remove(collision.gameObject);
            nbrOfPlayerTriggering--;

            //is there still a player close enough to activate the gate?
            if (nbrOfPlayerTriggering == 0)
            {
                triggered = false;

                if (!delayedSwitch)
                {
                    Notify(triggered);
                }

                else
                {
                    startDelay = true;
                }
            }
        }
    }
}
