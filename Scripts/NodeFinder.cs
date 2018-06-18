using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeFinder : MonoBehaviour {

	public Transform[] doors;
	public Transform player;


	public Material blue;
	public Material white;

	// Use this for initialization
	void Start () {
		doors = GetComponentsInChildren<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		Transform playernode = NearestPlayerNode (player);
		for (int i = 0; i < doors.Length; i++) {
			MeshRenderer doormesh = doors [i].gameObject.GetComponent<MeshRenderer> ();
			if (doors [i] == playernode) {
				doormesh.material = white;
			} else {
				if (doors [i].gameObject.GetComponent<MeshRenderer> () != null) {
					doormesh.material = blue;
				}
			}
		}
	}
		

	public Transform NearestPlayerNode(Transform origin){
		Transform closestTarg = null;
		float closestsqrdist = Mathf.Infinity;

		foreach (var door in doors) {
			Vector3 directionToTarg = origin.position - door.position;
			float sqrtotarg = directionToTarg.sqrMagnitude;
			if (sqrtotarg < closestsqrdist) {
				RaycastHit hit;
				if(Physics.Raycast(door.position, directionToTarg, out hit)) {
					if (hit.collider.name == origin.gameObject.name) {
						closestTarg = door;
						closestsqrdist = sqrtotarg;
					}
				}
			}
		}
		return closestTarg;
	}
}
