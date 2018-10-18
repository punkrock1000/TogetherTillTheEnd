using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BasePlayer //MAGE
{
    public bool hasShield = false;
    bool shieldActive = false;
    public GameObject tpShadow;
    public GameObject shield;

    //Gestion Atk
    float TIME_BETWEEN_SHOTS = 0.3f;
    float shotTimer = 0.0f;

    public override void Start()
    {
        base.Start();
        //spriteChild = transform.Find("Player1");
    }

    public override void Update()
    {
        base.Update();
        shotTimer -= Time.deltaTime;
    }

    public override void GestionInput()
    {
        if (Input.GetButton("LeftPlay2") && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            //FaceDirection(-Vector2.right);
        }
        if (Input.GetButton("RightPlay2") && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick2") > 0 && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick2") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick2") < 0 && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = false;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick2") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.left);
        }

        if (Input.GetButtonDown("JumpPlay2") && CanJump == 1)
        {
            CanJump = 0;
            rigidBody2D.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetButtonDown("TPAbilityPlay2") && hasAbility)
        {
            Teleport();
        }

        if (Input.GetButtonDown("TestingButton2"))
        {
            TakeDamage(1, false);
        }

        if (Input.GetButtonDown("MageShield") && !shieldActive && hasShield)
        {
            StartCoroutine(SpawnShield());
        }

        if (Input.GetButton("MageRangeAtk") && hasRangeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS;
            if(isLookingRight)
                Instantiate(RngAtk, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
            else
                Instantiate(RngAtk, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));

        }

        if (Input.GetButtonDown("MageMeleeAtk") && hasMeleeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS;
            if (isLookingRight)
                Instantiate(MeleeAtk, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
            else
                Instantiate(MeleeAtk, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));

        }

    }

    void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if (invincibilityFrames <= 0)
        {
            if (!(shieldActive && !IsPhysical))    //If the atk is not physical and the shild is active, then dmg block
            {
                StartCoroutine(SetInvicibility(2.0f));

                health -= nbOfDmg;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }
            }
        }
    }

    IEnumerator SpawnShield()
    {
        shieldActive = true;
        GameObject tempShield = Instantiate(shield, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(2.0f);
        shieldActive = false;
        Destroy(tempShield);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "PlayerOne")
            CanJump = 1;
    }

    void Teleport() //Not fully working
    {
        transform.position = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z); //Simple Teleport
        //Instantiate(tpShadow, new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z), transform.rotation); //Teleport Using Shadow
    }
}
