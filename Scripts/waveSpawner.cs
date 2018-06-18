using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waveSpawner : MonoBehaviour {

	public enum spawnState
	{
		spawning, waiting, counting
	}

	[System.Serializable]
	public class Wave
	{


		public int Acount;
		public int Bcount;
		public int Ccount;

	}

	public Transform ZombieA;
	public Transform ZombieB;
	public Transform ZombieC;

	public Wave[] waves;
	public Vector2[] spawnAreas;

	float waveCountdown;
	int countdownRounddown;
	public float timeBetweenWaves;
	private float SearchCountdown = 1f;
	int nextWave = 0;

	spawnState state = spawnState.counting;

	public TextMeshProUGUI Announcer;
	public TextMeshProUGUI countDown;
	public float fadeSpeed;
	bool started = false;

	// Use this for initialization
	void Start () {
		waveCountdown = 10f;
		Announcer.alpha = 0;
		countDown.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (state == spawnState.waiting) {
			if (!enemiesLeft ()) {
				started = false;
				WaveCompleted ();
			} else {
				return;
			}
		}

		if (waveCountdown <= 0) {
			if (state != spawnState.spawning) {
				StartCoroutine (Spawn (waves [nextWave]));
			}
		} else {
			waveCountdown -= Time.deltaTime;
			int waveNum = nextWave + 1;


			if (waveCountdown > 7 && nextWave != 0) {
				Announcer.alpha = 1;
				Announcer.text = "Congratulations";
			}

			if (waveCountdown <= 7 && waveCountdown > 3) {
				Announcer.alpha = 1;
				Announcer.text = "Wave " + waveNum.ToString ();
			}

			if (waveCountdown <= 3 && waveCountdown > 0 && !started) {
				StartCoroutine (TheFinalCountdown());
			} 

		}
	}

	void WaveCompleted () {
		state = spawnState.counting;
		Announcer.alpha = 1;
		waveCountdown = timeBetweenWaves;
		Announcer.text = "Wave Complete";
		if (nextWave + 1 > waves.Length - 1) {
			nextWave = 0;
			Debug.Log ("ROUNDCOMPLETE");
		} else {
			nextWave++;
		}
	}

	public bool enemiesLeft () {
		SearchCountdown -= Time.deltaTime;
		if (SearchCountdown <= 0f) {
			SearchCountdown = 1;
			if (GameObject.FindGameObjectWithTag ("zombie") == null) {
				return false;
			} 
		}
		return true;
	}


	IEnumerator Spawn (Wave wave) {
		Announcer.alpha = 1;
		Announcer.text = "They are coming...";
		state = spawnState.spawning;
		StartCoroutine (AnnouncerFade ());

		if (wave.Acount > 0) {
			for (int i = 0; i < wave.Acount; i++) {
				int randomSpawnArea = Random.Range (0, 4);
				Vector2 point = spawnAreas [randomSpawnArea];
				float randomX = Random.Range (-5f, 5f);
				float randomY = Random.Range (-5f, 5f);
				Vector3 loc = new Vector3 (point.x + randomX, 4, point.y + randomY);
				Transform zombie = Instantiate (ZombieA, loc, Quaternion.Euler (new Vector3 (0, 90, 0)), transform) as Transform;

				yield return new WaitForSeconds (1f);
			}
		}

		if (wave.Bcount > 0) {
			for (int i = 0; i < wave.Bcount; i++) {
				int randomSpawnArea = Random.Range (0, 4);
				Vector2 point = spawnAreas [randomSpawnArea];
				float randomX = Random.Range (-5f, 5f);
				float randomY = Random.Range (-5f, 5f);
				Vector3 loc = new Vector3 (point.x + randomX, 4, point.y + randomY);
				Transform zombie = Instantiate (ZombieB, loc, Quaternion.Euler (new Vector3 (0, 90, 0)), transform) as Transform;

				yield return new WaitForSeconds (1f);
			}
		}

		if (wave.Ccount > 0) {
			for (int i = 0; i < wave.Ccount; i++) {
				int randomSpawnArea = Random.Range (0, 4);
				Vector2 point = spawnAreas [randomSpawnArea];
				float randomX = Random.Range (-5f, 5f);
				float randomY = Random.Range (-5f, 5f);
				Vector3 loc = new Vector3 (point.x + randomX, 4, point.y + randomY);
				Transform zombie = Instantiate (ZombieC, loc, Quaternion.Euler (new Vector3 (0, 90, 0)), transform) as Transform;

				yield return new WaitForSeconds (1f);
			}
		}

		state = spawnState.waiting;

		yield break;
	}

	IEnumerator AnnouncerFade () {
		while (Announcer.alpha != 0) {
			Announcer.alpha = Mathf.MoveTowards (Announcer.alpha, 0, fadeSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
	}



	IEnumerator TheFinalCountdown () {
		
		countDown.alpha = 1;

		started = true;

		StartCoroutine (AnnouncerFade ());

		int counter = 3;
		while (counter > 0) {
			countDown.text = counter.ToString();
			counter--;
			yield return new WaitForSeconds (1);
		}
		countDown.alpha = 0;
	}


}
