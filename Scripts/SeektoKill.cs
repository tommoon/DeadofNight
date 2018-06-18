using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeektoKill : MonoBehaviour {

	public GameObject control;
	public GameObject Player;
	public NodeFinder nodefind;
	public pathfinder pathfind;



	public List <Vector3> Path;
	int targetIndex;

	public int inteligence;
	public float speed;

	// Use this for initialization
	void Start () {
		control = GameObject.FindGameObjectWithTag ("Control");
		Player = GameObject.FindGameObjectWithTag ("Player");
		nodefind = control.GetComponent<NodeFinder> ();
		pathfind = control.GetComponent<pathfinder> ();
	 	Seek ();

	}

	void Seek () {


		List <Node> route = control.GetComponent<pathfinder> ().findPath (transform);
		foreach (var item in route) {

			Path.Add (item.transform.position);
		}
			
			RaycastHit hit;
	
			if (Physics.Raycast (transform.position,Player.transform.position - transform.position, out hit)) {
			if (hit.collider.gameObject != Player) {
				
				StartCoroutine(FollowPath());
							
			} else if (hit.collider.gameObject == Player){
				Debug.Log("GOTCHA");
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = Path[0];
		while (true) {
			if (transform.position == currentWaypoint) {
				RaycastHit hit;
				List <Node> altRoute = control.GetComponent<pathfinder> ().findPath (transform);

				if (Physics.Raycast (transform.position, Player.transform.position - transform.position, out hit)) {
					if (hit.collider.gameObject != Player) {

						targetIndex++;
						foreach (var item in altRoute) {
							Debug.Log (item.name + " " + targetIndex);
						}
						if (targetIndex >= Path.Count) {
				
							yield break;
			
						}
				
						currentWaypoint = Path [targetIndex];
			
					}
				}
			}

			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}
}
