using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour {


	public Transform player;
	public bool ready = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ready) {
			transform.position = new Vector3 (player.position.x, 50, player.position.z);
		}
	}
}
