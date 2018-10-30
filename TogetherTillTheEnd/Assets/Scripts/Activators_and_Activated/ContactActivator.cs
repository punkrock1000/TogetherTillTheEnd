using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactActivator : Activator
{

    //list of object that are colliding 
    //(to make sure both player influencing the same swith does not create irregularities) 
    List<GameObject> currentlyColliding;

    //how many player are triggering the switch
    public int numbrOfPLayerTriggering;

    // Use this for initialization
    protected void Start()
    {
        base.Start();
        numbrOfPLayerTriggering = 0;
        currentlyColliding = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            triggered = true;
            numbrOfPLayerTriggering++;
            Notify(triggered);
            currentlyColliding.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            currentlyColliding.Remove(collision.gameObject);
            numbrOfPLayerTriggering--;

            //is there still a player close enough to activate the gate?
            if (numbrOfPLayerTriggering == 0)
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
