using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedBullet : MonoBehaviour {
    Vector2 direction;
    [SerializeField]
    float speed = 5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}

    public void directionSetter(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        
        if (direction.x < 0)
        {
            FaceDirection(Vector2.left);
        }
        else
        {
            FaceDirection(Vector2.right);
        }
        
    }

    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            if (collision.gameObject.CompareTag("PlayerOne"))
            {
                collision.gameObject.GetComponent<Warrior>().TakeDamage(1, false);
            }
            else
            {
                collision.gameObject.GetComponent<Mage>().TakeDamage(1, false);
            }

            Destroy(gameObject);
        }
    }
        
}
