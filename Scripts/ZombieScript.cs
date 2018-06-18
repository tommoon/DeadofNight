using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieScript : MonoBehaviour {

	public int health;
    public int maxhealthUI;
	public int strength;
	public float attackDelay;

	Animator anims;

	GameObject player;

	bool attacking = false;

	// Use this for initialization
	void Start () {
        maxhealthUI = health;
		anims = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {

			StartCoroutine(fallen ());
		}

	}

	void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
			if (!attacking)
				StartCoroutine (attackingAnims ());
		}
	}

	IEnumerator attackingAnims (){
		attacking = true;
		anims.Play ("attack");
		GetComponent<AudioSource> ().Play();
		while (!anims.GetCurrentAnimatorStateInfo (0).IsName ("attack")) {
			yield return null;
		}
		player.GetComponent<stats> ().takeDamage (0.2f);
		yield return new WaitForSeconds (attackDelay);
		attacking = false;  
	}

	IEnumerator fallen () {
		Collider[] cols = GetComponents<Collider> ();

		foreach (var col in cols) {
			col.enabled = false;
		}

		GetComponent<AIPath> ().canMove = false;
		anims.Play ("Death");

		yield return new WaitForSeconds (1.5f);

		Destroy (gameObject);
	}
}
