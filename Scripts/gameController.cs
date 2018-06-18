using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	Camera cam;
	GameObject player;

	public GameObject MainMenuPanel;
	public GameObject LittleBook;
	public GameObject Options;
	public GameObject Credits;
	public GameObject HowToPlay;
	public GameObject backButton;
	public GameObject stats;
	public GameObject Title;

	public ParticleSystem rain;

	public Slider volume;




	void Start () {
		Time.timeScale = 0;
        MainMenuPanel.SetActive(true);
        Title.SetActive(true);
		cam = Camera.main;
        cam.transform.position = new Vector3(-10.14f, 10.45f, 3.98f);
        cam.transform.rotation = Quaternion.Euler(20.32f, -217.23f, 0);

		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<Controller> ().enabled = false;
	}

	void Awake () {
		stats.SetActive (false);
	}
	void Update () {
		AudioListener.volume = volume.value;
	}


		

	public void restartGame () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}



	public void startGame () {
		MainMenuPanel.SetActive (false);
		Title.SetActive (false);
		stats.SetActive (true);
		StartCoroutine (startGameMoves ());
	}

	public void OpenOptions () {
		MainMenuPanel.SetActive (false);
		LittleBook.SetActive (true);
		Options.SetActive (true);
		backButton.SetActive (true);
	}

	public void OpenCredits () {
		MainMenuPanel.SetActive (false);
		LittleBook.SetActive (true);
		Credits.SetActive (true);
		backButton.SetActive (true);
	}

	public void OpenHTP () {
		MainMenuPanel.SetActive (false);
		LittleBook.SetActive (true);
		HowToPlay.SetActive (true);
		backButton.SetActive (true);
	}

	public void BackToMainMenu () {
		MainMenuPanel.SetActive (true);
		LittleBook.SetActive (false);
		Credits.SetActive (false);
		HowToPlay.SetActive (false);
		Options.SetActive (false);
		backButton.SetActive (false);
	}

	public void SwitchOffRain (bool value) {
		rain.gameObject.SetActive (value);
	}
	IEnumerator startGameMoves () {
		Time.timeScale = 1;
		player.GetComponent<Animator> ().Play ("Copwalk");
		while(cam.transform.position.y <= 49.6f && cam.transform.rotation.x <= 89.6f){
			player.transform.position = Vector3.MoveTowards (player.transform.position, new Vector3 (0, 0, 0), Time.deltaTime * 4);

			cam.transform.position = Vector3.Slerp(cam.transform.position, new Vector3 (0, 50, 0), Time.deltaTime * 1.2f);

			cam.transform.rotation = Quaternion.Slerp (cam.transform.rotation, Quaternion.Euler (90, 0, 0), Time.deltaTime * 1.2f);
			yield return new WaitForEndOfFrame();
		}
		player.GetComponent<Controller> ().enabled = true;
		player.GetComponent<Controller> ().ready = true;
		cam.GetComponent<follower> ().ready = true;
		cam.transform.rotation = Quaternion.Euler (90, 0, 0);

		yield break;
	}


}
