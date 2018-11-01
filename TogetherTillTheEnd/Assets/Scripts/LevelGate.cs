using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class LevelGate : MonoBehaviour {
    private Animator anim;
    public bool opened;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        opened = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("allo");
        if (collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo")
        {
            Debug.Log("hey");
            opened = !opened;
            anim.SetBool("opened", opened);
        }
    }
}
