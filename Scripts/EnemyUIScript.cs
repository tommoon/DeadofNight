using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIScript : MonoBehaviour {
    
    private ZombieScript zombiescript;
    private stats statistics;
    public Slider healthSlider;
    private Transform canvas;
    private Camera Cam_main;

    private GameObject player;
    FieldOfView fov;

    float angle;
    float radius;

    public float healthPanelOffset = 0.35f;


	// Use this for initialization
	void Start () {
        Cam_main = Camera.main;
        player = GameObject.FindWithTag("Player");
        fov = player.GetComponent<FieldOfView>();
        canvas = GetComponentInChildren<Canvas>().gameObject.transform;

        statistics = player.GetComponent<stats>();
        zombiescript = GetComponent<ZombieScript>();
	}
	
	// Update is called once per frame
	void Update () {
        canvas.LookAt(transform.position + Cam_main.transform.rotation * Vector3.forward, Cam_main.transform.rotation * Vector3.down);

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < fov.viewRadius)
        {
            Vector3 playerDirection = transform.position - player.transform.position;
            Vector3 playerForward = player.transform.forward;

            float VAngle = Vector3.Angle(playerDirection, playerForward);

            if (VAngle < (fov.viewAngle / 2))
            {

                healthSlider.gameObject.SetActive(true);
                healthSlider.value = zombiescript.health / (float)zombiescript.maxhealthUI;

            } else{
                healthSlider.gameObject.SetActive(false);
            }

        } else {
            healthSlider.gameObject.SetActive(false);
        }

       }
}
