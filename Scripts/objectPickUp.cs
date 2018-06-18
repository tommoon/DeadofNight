using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPickUp : MonoBehaviour {

	itemSpawner spawner;

	// Use this for initialization
	void Start () {
		spawner = GameObject.FindGameObjectWithTag ("ItemSpawner").GetComponent<itemSpawner> ();
	}

	void AudioOn(){
		GetComponent<AudioSource> ().volume = 1;
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (transform.up, 60f * Time.deltaTime);
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			playSound ();
			if (gameObject.tag == "Power") {
				col.gameObject.GetComponent<stats> ().power = 20;
				spawner.dropPower ();
			}

			if (gameObject.tag == "Health") {
				col.gameObject.GetComponent<stats> ().Health.value = 1;
				spawner.dropHealth ();
			}
			Destroy (gameObject);

		}
	}

	void playSound () {
		GetComponent<AudioSource> ().volume = 1;
		GetComponent<AudioSource> ().Play ();
	}
}
