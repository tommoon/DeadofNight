using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour {

	public Material SeeThrough;
	public Material Target;

	MeshRenderer mesh;
	Light flash;

	// Use this for initialization
	void Start () {
		flash = GetComponentInChildren<Light> ();
		flash.enabled = false;
		mesh = GetComponent<MeshRenderer> ();
		mesh.material = SeeThrough;

		Invoke("lightning", 1f);
	}
	
	void lightning()
	{
		float randomTime = Random.Range (10, 60);

		StartCoroutine (lightningAction ());

		Invoke("lightning", randomTime);

	}

	IEnumerator lightningAction () {
		GetComponent<AudioSource> ().Play();
		flash.enabled = true;
		mesh.material = Target;
		yield return new WaitForSeconds (0.5f);

		flash.enabled = false;
		mesh.material = SeeThrough;

		yield break;
	}
}
