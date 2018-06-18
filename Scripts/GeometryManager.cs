using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryManager : MonoBehaviour {


	GameObject[] walls;
	// Use this for initialization
	void Start () {
		walls = GameObject.FindGameObjectsWithTag ("Wall");

		setWalls (walls);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setWalls (GameObject[] _walls) {
		foreach (var wall in _walls) {
			wall.transform.localScale = new Vector3 (wall.transform.localScale.x, 60, wall.transform.localScale.z);
			wall.transform.localPosition = new Vector3 (wall.transform.position.x, 30, wall.transform.position.z);

			wall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(wall.transform.localScale.x , wall.transform.localScale.z );
		}
	}
}
