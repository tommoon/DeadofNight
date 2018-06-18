using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawner : MonoBehaviour {

	GameObject[] spawnLocations;

	public Transform power;
	public Transform HealthPrefab;

	// Use this for initialization
	void Start () {

		spawnLocations = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			spawnLocations[i] = transform.GetChild(i).gameObject;
		}

		dropPower ();
		dropHealth ();

	}

	public void dropPower () {
		
		float randomX = Random.Range (-5, 5);
		float randomY = Random.Range (-5, 5);

		int loc = Random.Range (0, spawnLocations.Length);

		float x = spawnLocations [loc].transform.position.x;
		float z = spawnLocations [loc].transform.position.z;

		Transform Power = Instantiate (power, new Vector3 (x + randomX, 2.5f, z + randomY), Quaternion.Euler(new Vector3(0,0,90))) as Transform;
	}

	public void dropHealth () {

		float randomX = Random.Range (-5, 5);
		float randomY = Random.Range (-5, 5);

		int loc = Random.Range (0, spawnLocations.Length);

		float x = spawnLocations [loc].transform.position.x;
		float z = spawnLocations [loc].transform.position.z;

		Transform Health = Instantiate (HealthPrefab, new Vector3 (x + randomX, 2.5f, z + randomY), Quaternion.identity) as Transform;
	}

}
