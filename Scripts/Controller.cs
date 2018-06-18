using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [System.Serializable]
    public class weapon{
        public string name;
        public bool penetrate;

        public int clipSize;
        public int roundsAvailable;
        public int dmg;
        public int spread;

        public float rateOfFire;
        public float reloadTime;

        [Range(0,60)]
        public float spreadAngle;
    }

    public weapon[] weapons;

    public int selectedWeapon = 0;

    weapon currentWeapon;
    public string currentWeaponString;

	public float moveSpeed = 4f;
	public Transform bullet;

    //current weapon stuff:
    public int currentclipsize;
    public int currentroundsavailable;
    public int currentdamage;
    public int spreadnum;

    public float ROF;
    public float currentreloadtime;
    public float spreadangle;
    private float nextTimeTofire = 0f;

	Rigidbody RB;
	Camera viewCamera;
	Vector3 velocity;
	Animator anims;

	public Light muzzleFlasher; 

	public bool ready = false;

	void Start () {
		RB = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
		anims = GetComponent<Animator> ();
		muzzleFlasher.enabled = false;
        weapons[0].roundsAvailable = 1000;
        selectWeapon();
	}

	void Update () {
		if (ready) {
            //begin
			Vector3 mousePos = viewCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
			transform.LookAt (mousePos + Vector3.up * transform.position.y);

            //move
			velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
			

            //shooting
            if (Input.GetMouseButton (0) && Time.time >= nextTimeTofire) {
                

                nextTimeTofire = Time.time + ROF;
				shoot ();

			}

			if (velocity.sqrMagnitude == 0) {
				anims.Play ("stand");
			} else {
				anims.Play ("Copwalk");
			}


            //selectingWeapon
            if(Input.GetKey(KeyCode.Alpha2)){
                selectedWeapon = 1;
                selectWeapon();
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
                selectWeapon();
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                selectedWeapon = 2;
                selectWeapon();
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                selectedWeapon = 3;
                selectWeapon();
            }
		}
	}

	void FixedUpdate() {

		/*
		transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical"));
		transform.Translate(Vector3.left * moveSpeed * Input.GetAxis("Horizontal"));
		*/

		RB.MovePosition (RB.position + velocity * Time.fixedDeltaTime);
	}

	void shoot (){

        if(spreadnum <= 0)
        {
		    Transform shot = Instantiate (bullet, transform.localPosition + new Vector3 (0, 4.3f, 0) + (2.5f * transform.forward), transform.rotation) as Transform;
            shot.GetComponent<BulletScript>().Damage = currentdamage;
        }else
        {
            float spreader = spreadangle / spreadnum;
            Quaternion startDir = Quaternion.Euler(0, -spreadangle / 2f, 0) * transform.rotation;

            for (int i = 0; i < spreadnum; i++)
            {

                Transform shot = Instantiate(bullet, transform.localPosition + new Vector3(0, 4.3f, 0) + (2.5f * transform.forward), startDir) as Transform;
                shot.GetComponent<BulletScript>().Damage = currentdamage;
                startDir *= Quaternion.Euler(Vector3.up * spreader); 
            }
        }
        StartCoroutine (muzzleFlash ());
	}

	IEnumerator muzzleFlash (){
		muzzleFlasher.enabled = true;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		yield return new WaitForSeconds (0.1f);
		muzzleFlasher.enabled = false;
		yield break;
	}

    void selectWeapon (){
        int i = 0;
        foreach (var gun in weapons)
        {
            if(i == selectedWeapon){
                currentclipsize = gun.clipSize;
                currentroundsavailable = gun.roundsAvailable;
                currentdamage = gun.dmg;
                spreadnum = gun.spread;

                ROF = gun.rateOfFire;
                currentreloadtime = gun.reloadTime;
                spreadangle = gun.spreadAngle;

                currentWeapon = weapons[i];
                currentWeaponString = weapons[i].name;
            }


            i++;
        }

    }
}
