using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stats : MonoBehaviour {

	public Slider Health;
	public Slider powerSlide;

	Animator anim;
	Controller cont;
	Camera cam;

	public Light downLight;
	public Light forLight;
	public Image GameOver;
	public ParticleSystem rain;
	public GameObject RestartButton;


	TextMeshProUGUI gameOverText;
	FieldOfView fov;

	public float power = 10;
	float healthValue;

	// Use this for initialization
	void Start () {
		fov = GetComponent<FieldOfView> ();
		Health.value = Health.maxValue;	
		healthValue = Health.value;
		anim = GetComponent<Animator> ();
		cont = GetComponent<Controller> ();
		cam = Camera.main;
		gameOverText = GameOver.gameObject.GetComponentInChildren<TextMeshProUGUI> ();
		gameOverText.alpha = 0;
		RestartButton.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		healthValue = Health.value;

		if (healthValue <= 0.01) {
			Die ();
		}

		PowerProblems ();

	}

	void PowerProblems () {
		power = Mathf.MoveTowards (power, 1, Time.deltaTime * 0.05f);
		float colRange = power / 10;
        fov.viewRadius = power;
        forLight.range = power;
		powerSlide.value = power;

		if (colRange > 0.5f) {
			float colRangeAdjust = (power - 5) / 5;
		
			powerSlide.fillRect.GetComponent<Image> ().color = Color.Lerp (Color.yellow, Color.cyan, colRangeAdjust);

		} else if (colRange < 0.5f) {
			float colRangeAdjust = power / 5;

			powerSlide.fillRect.GetComponent<Image> ().color = Color.Lerp (Color.red, Color.yellow, colRangeAdjust);
		}
	}

	public void takeDamage (float damageTaken) {
		Health.value -= damageTaken;
		healthValue = Health.value;
	}

	void Die (){
		anim.Play ("Die");
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombie");
        foreach (var zom in zombies)
        {
            zom.GetComponent<AudioSource>().volume = 0.0f;
        }
        StartCoroutine (widenLight ());
		StartCoroutine (dropCam ());
		forLight.enabled = false;
		cont.enabled = false; 

	}

	IEnumerator widenLight () {
		while (downLight.spotAngle != 70) {
			downLight.spotAngle = Mathf.MoveTowards (downLight.spotAngle, 70, 0.5f * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		yield break;
	}

	IEnumerator dropCam () {
		rain.Pause (true);
		while (cam.fieldOfView != 7) {
			cam.fieldOfView = Mathf.MoveTowards (cam.fieldOfView, 7, 1 * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		yield return new WaitForSeconds (1);
		StartCoroutine (FadeToWhite ());
		yield break;
	}

	IEnumerator FadeToWhite () {
		float alpha = GameOver.color.a;
		while (GameOver.color.a != 1) {
			Color newColor = new Color (1, 1, 1, Mathf.MoveTowards (alpha, 1f, 0.25f * Time.deltaTime));
			GameOver.color = newColor;
			yield return new WaitForEndOfFrame ();
		}
		yield return new WaitForSeconds (0.5f);
		while (gameOverText.alpha != 1) {
			gameOverText.alpha = Mathf.MoveTowards (gameOverText.alpha, 1, 0.15f * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		yield return new WaitForSeconds (1.5f);
		RestartButton.SetActive (true);
		yield break;
	}
}
