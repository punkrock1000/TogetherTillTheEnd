using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour // WARRIOR
{
    //Gestion Movement
    float moveSpeed = 5.5f;
    bool isLookingRight = true;

    //Gestion Life
    public int health = 3;
    bool isDead = false;
    float invincibilityFrames = 0.0f;

    SpriteRenderer spriteRenderer;

    //Gestion Jump
    public float jumpForce = 550.0f;
    int CanJump = 1;
    float secondJumpTimer;

    //Abilities
    public bool HasDoubleJumpAbility;

    //External Refrences
    Rigidbody2D rigidBody2D;
    Transform spriteChild;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteChild = transform.Find("Player1");
    }

    void Update()
    {
        invincibilityFrames -= Time.deltaTime;

        if(!isDead)
        {
            if (CanJump == 1 && HasDoubleJumpAbility)
                secondJumpTimer -= Time.deltaTime;
            else
                secondJumpTimer = 0.2f;

            GestionInput();
        }
        
    }


    private void GestionInput()
    {
        if (Input.GetButton("LeftPlay1"))
        {
            isLookingRight = false;
            transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(-Vector2.right);
        }
        if (Input.GetButton("RightPlay1"))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick1") > 0)
        {
            isLookingRight = false;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick1") < 0)
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.left);
        }

        if (Input.GetButtonDown("JumpPlay1") && CanJump >= 1)
        {
            if(HasDoubleJumpAbility && CanJump == 1)
            {
                if(secondJumpTimer <= 0)
                {
                    CanJump--;
                    rigidBody2D.AddForce(Vector2.up * jumpForce);
                }
            }
            else
            {
                CanJump--;
                rigidBody2D.AddForce(Vector2.up * jumpForce);
            }
            
        }

        if (Input.GetButtonDown("TestingButton"))
        {
            TakeDamage(1, true);
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PlayerTwo")
            if (HasDoubleJumpAbility)
                CanJump = 2;
            else
                CanJump = 1;
    }

    void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if(invincibilityFrames <= 0 && true) // "true" will be replace by shield check later
        {
            StartCoroutine(setInvinsibility(1.5f));
            
            health -= nbOfDmg;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
            }
        }
    }

    IEnumerator setInvinsibility(float time)
    {
        invincibilityFrames = time;
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(time);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    /*private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        spriteChild.rotation = rotation3D;
    }*/
}
