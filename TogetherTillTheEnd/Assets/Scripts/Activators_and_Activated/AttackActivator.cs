﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Switch only activated by magical attacks
public class AttackActivator : Activator
{
    //Type of Attack used to trigger switch (MagicAtk or ???)
    [SerializeField]
    string triggerType;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(triggerType))
        {
            triggered = true;
            Notify(triggered);
        }
    }

    override protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(triggerType))
        {
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

