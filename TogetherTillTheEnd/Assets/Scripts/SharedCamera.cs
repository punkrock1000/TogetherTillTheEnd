using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCamera : MonoBehaviour
{

    private GameObject warrior;
    private GameObject mage;

    //private Vector2 minimumSize;

    public Camera cam;

    [SerializeField]
    private float zoomSpeed = 1.5f;

    [SerializeField]
    public float boundariesOffset = 2.0f;

    private bool dezoomed = false;

    // The small orthographic size is the one set directly before starting the game.
    private float smallOrthographicSize;

    [SerializeField]
    private float bigOrthographicSize = 17f;

    [SerializeField]
    private float triggerDistancePlayers = 18f;

    // Use this for initialization
    void Start()
    {
        warrior = FindObjectOfType<Warrior>().gameObject;
        mage = FindObjectOfType<Mage>().gameObject;
        cam = GetComponent<Camera>();

        smallOrthographicSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPositionAndSize();
    }

    private void SetCameraPositionAndSize()
    {
        // Get position of players
        Vector3 warriorPos = warrior.transform.position;
        Vector3 magePos = mage.transform.position;

        // Set the camera position as the average between both of those positions
        transform.position = new Vector3((warriorPos.x + magePos.x) * 0.5f, transform.position.y, transform.position.z);

        if (!dezoomed && (PlayerReachedRightBoundary(warriorPos.x) || PlayerReachedRightBoundary(magePos.x)))
        {
            dezoomed = true;
        }
        else if (AtTriggerDistancePlayers(warriorPos.x, magePos.x))
        {
            dezoomed = false;
        }

        if (dezoomed)
            DezoomCamera();
        else
            ZoomCamera();
    }

    public bool PlayerReachedRightBoundary(float playerXPos)
    {
        return GetCameraRightBoundary() <= playerXPos;
    }

    public float GetCameraRightBoundary()
    {
        return transform.position.x + (cam.orthographicSize * cam.aspect) - boundariesOffset;
    }

    public bool PlayerReachedLeftBoundary(float playerXPos)
    {
        return GetCameraLeftBoundary() >= playerXPos;
    }

    public float GetCameraLeftBoundary()
    {
        return transform.position.x - (cam.orthographicSize * cam.aspect) + boundariesOffset;
    }

    public bool AtTriggerDistancePlayers(float warriorXPos, float mageXPos)
    {
        return Mathf.Abs(warriorXPos - mageXPos) <= triggerDistancePlayers;

    }

    public void DezoomCamera()
    {
        if (cam.orthographicSize >= bigOrthographicSize)
            return;
        else
            cam.orthographicSize += Time.deltaTime * zoomSpeed;
    }

    public void ZoomCamera()
    {
        if (cam.orthographicSize <= smallOrthographicSize)
            return;
        else
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
    }
}
