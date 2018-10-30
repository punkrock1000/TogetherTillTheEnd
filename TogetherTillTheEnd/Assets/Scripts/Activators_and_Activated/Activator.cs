using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    // boolean making sure both player cannot mess the state
    public bool triggered;

    //List containing all GameObject Activated by this activator
    List<GameObject> ActivableList = new List<GameObject>();
    protected bool multiActivator;

    //Can Specify another object it activated
    [SerializeField]
    protected GameObject anActivables;

    //delay timer boolean and time value
    [SerializeField]
    protected bool delayedSwitch;
    protected bool startDelay;
    [SerializeField]
    protected float delayTimer;
    protected float timeSinceActivated;

    // Use this for initialization
    protected void Start()
    {
        triggered = false;
        multiActivator = false;
        startDelay = false;
        timeSinceActivated = 0;

        if (anActivables != null)
        {
            anActivables.GetComponent<Activable>().AttachActivator(gameObject);
            AttachActivable(anActivables);
        }
    }

    protected void Update()
    {
        if(startDelay)
        {
            timeSinceActivated += Time.deltaTime;
            if(timeSinceActivated >= delayTimer)
            {
                Notify(triggered);
                timeSinceActivated = 0;
            }
        }
    }

    //function for multi activator
    protected void Notify(bool newState)
    {
        if(multiActivator)
        {
            foreach (GameObject i in ActivableList)
            {
                i.GetComponent<Activable>().checkState(newState);
            }
        }

        if(anActivables != null)
        {
            anActivables.GetComponent<Activable>().checkState(newState);
        }

    }

    //function attaching Activables to this Activator, should only be used if the instance is activating multiples activatables
    public void AttachActivable(GameObject activable)
    {
        multiActivator = true;
        ActivableList.Add(activable);

    }

    //collision Parameter, changes depending on the Activator SwitchTypes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
