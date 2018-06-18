using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	
	public GameObject control;
	public NodeFinder nodefind;
	public pathfinder pathfind;

	public Material white;
	public Material blue;


	public List <Vector3> Path;
	int targetIndex;
	public float speed = 3;

	// Use this for initialization
	void Start () {
		control = GameObject.FindGameObjectWithTag ("Control");
		nodefind = control.GetComponent<NodeFinder> ();
		pathfind = control.GetComponent<pathfinder> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			List <Node> route = control.GetComponent<pathfinder> ().findPath (transform);
			foreach (var item in route) {
				Path.Add (item.transform.position);
			}

			StartCoroutine (FollowPath ());
		}


	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = Path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= Path.Count) {
					yield break;
				}
				currentWaypoint = Path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}
}
