using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public int Damage = 100;
    public bool penetraate;
	CapsuleCollider Player;
	public GameObject bloodSplat;
    Vector3 velocity;


	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<CapsuleCollider> ();
        velocity = (-transform.forward * 40);
	}

	void Update () {
        transform.Translate (Vector3.forward * 40 * Time.deltaTime);
		Physics.IgnoreLayerCollision(11,12);
       

	}

	void FixedUpdate () {
		Physics.IgnoreLayerCollision(11,12);
		Physics.IgnoreCollision (GetComponent<SphereCollider> (), Player);
	}


	void OnCollisionEnter(Collision col){
      
       
		if (col.transform.tag == "zombie") {
  
            GameObject blood = Instantiate(bloodSplat, col.contacts[0].point, Quaternion.identity) as GameObject;
            blood.transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);

            col.gameObject.GetComponent<ZombieScript> ().health -= Damage;

            if (!penetraate)
            {
                Destroy(this.gameObject);
            }

		} else{
			Destroy (this.gameObject);

		}

	} 
}
