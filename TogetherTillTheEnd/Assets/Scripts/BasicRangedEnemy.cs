using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRangedEnemy : MonoBehaviour {
    bool mBusy = false;
    [SerializeField]
    Transform mMage;
    [SerializeField]
    Transform mWarrior;
    [SerializeField]
    GameObject mProjectilePrefab;
    Transform mTarget;
    bool mTargettingMage = false;
    Animator mAnimator;
    bool visible = false;
    Vector2 direction;
    // Use this for initialization
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update () {
        if (!mBusy && visible)
        {
            if (targetChoice())
            {
                mTarget = mMage;
            }
            else
            {
                mTarget = mWarrior;
            }
            direction = ((mTarget.position - transform.position).x < 0) ? Vector2.left : Vector2.right;
            FaceDirection(direction);
            attack(mTarget.transform.position - transform.position);
        }
	}

    bool targetChoice()
    {
        Vector2 mageDiff = mMage.transform.position - transform.position;
        Vector2 warrDiff = mWarrior.transform.position - transform.position;

        if (mageDiff.magnitude < warrDiff.magnitude)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void attack(Vector2 direction)
    {
        mAnimator.SetBool("isAttacking", true);
    }

    void projectileCreation()
    {
        GameObject projectileObject = Instantiate(mProjectilePrefab, transform.position, Quaternion.identity) as GameObject;
        BasicRangedBullet basicRangedBullet = projectileObject.GetComponent<BasicRangedBullet>();
        basicRangedBullet.directionSetter(mTarget.transform.position - transform.position);
    }

    void attackEnd()
    {
        mBusy = false;
        mAnimator.SetBool("isAttacking", false);
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }
}
