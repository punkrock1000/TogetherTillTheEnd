using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBox : MonoBehaviour {
    [SerializeField]
    Vector2 direction = Vector2.right;

    public Vector2 GetDirection()
    {
        return direction;
    }
}
