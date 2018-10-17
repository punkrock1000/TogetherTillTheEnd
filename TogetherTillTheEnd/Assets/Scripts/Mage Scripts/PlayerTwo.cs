using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MonoBehaviour //MAGE
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
    bool CanJump = true;

    //Gestion Abilities
    public GameObject RngAtk;
    public GameObject MeleeAtk;
    public bool hasTPAbility = false;
    public bool hasRangeAtk = false;
    public bool hasMeleeAtk = false;
    public bool hasShield = false;
    bool shieldActive = false;
    public GameObject tpShadow;
    public GameObject shield;

    //Gestion Atk
    float TIME_BETWEEN_SHOTS = 0.3f;
    float shotTimer = 0.0f;

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
        shotTimer -= Time.deltaTime;

        if (!isDead)
        {
            GestionInput();
        }

    }


    private void GestionInput()
    {
        if (Input.GetButton("LeftPlay2"))
        {
            isLookingRight = false;
            transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(-Vector2.right);
        }
        if (Input.GetButton("RightPlay2"))
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick2") > 0)
        {
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick2") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick2") < 0)
        {
            isLookingRight = false;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick2") * moveSpeed * Time.deltaTime);
            //FaceDirection(Vector2.left);
        }

        if (Input.GetButtonDown("JumpPlay2") && CanJump)
        {
            CanJump = false;
            rigidBody2D.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetButtonDown("TPAbilityPlay2") && hasTPAbility)
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
                StartCoroutine(setInvinsibility(2.0f));

                health -= nbOfDmg;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }
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
            CanJump = true;
    }

    void Teleport() //Not fully working
    {
        transform.position = new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z); //Simple Teleport
        //Instantiate(tpShadow, new Vector3(transform.position.x + 5.0f, transform.position.y, transform.position.z), transform.rotation); //Teleport Using Shadow
    }

    /*private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        spriteChild.rotation = rotation3D;
    }*/
}
