using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour {

    //position marker to constrain movement
    [SerializeField]
    Vector3 triggeredPosition;
    Vector3 initialPosition;
    Vector3 intialToTriggered;

    //Should only be used in cases of multiples ACTIVA-BLES affected by same activator
    [SerializeField]
    GameObject instancedActivator;

    //should only be used in cases of multiple ACTIVA-TORS affected this Activable
    List<GameObject> activatorList = new List<GameObject>();
    public bool multiSwitch;

    //field setting movement speed
    [SerializeField]
    float movementSpeed;

    //bool setting state
    public bool triggered;

    // Use this for initialization
    void Start() {
        initialPosition = transform.position;
        triggered = false;
        intialToTriggered = (triggeredPosition).normalized;

        if (instancedActivator != null)
        {
            AttachToActivator(gameObject);
            AttachActivator(instancedActivator);
        }
    }

    // Update is called once per frame
    void Update() {

        if(triggered && transform.position.y <= triggeredPosition.y)
        {
            transform.Translate(intialToTriggered * movementSpeed * Time.deltaTime, Space.World);
        }

        else if (!triggered && transform.position.y >= initialPosition.y)
        {
            transform.Translate(-intialToTriggered * movementSpeed * Time.deltaTime, Space.World);
        }

    }

    //Should only be used in cases of multiples activators pointing to same activators
    void AttachToActivator(GameObject aGameObject)
    {
        instancedActivator.GetComponent<Activator>().AttachActivable(aGameObject);
    }

    public void AttachActivator(GameObject aGameObject)
    {
        activatorList.Add(aGameObject);
    }

    void triggerAction(bool aState)
    {
        triggered = aState;
    }

    //functions either checking the activator list to determine state
    //or change state to provided state (by only connector)
    public void checkState(bool aState)
    {
        bool finalState = false;

        foreach (GameObject i in activatorList)
        {
            if (i.GetComponent<Activator>().triggered)
            {
                finalState = true;
            }
        }

        triggered = finalState;
    }
}
