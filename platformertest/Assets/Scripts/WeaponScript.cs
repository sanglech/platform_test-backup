using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {


	public float fireRate=0;
	public float Damage=10;
	public LayerMask whatToHit;

	public Transform BulletTrailPrefab;

	float timeToFire=0;
	float timeToSpawnBullet=0;
	public float bulletSpawnrate=10;
	Transform firePoint;

	
	void Awake () {
		//Will search for childern, in this case will find the fire point of gun.
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError("Could not find firePoint! ");	
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Is this a single fire weapon, i.e. firerate=0
		if (fireRate == 0) 
		{
			// Is the fire button being held down?
			if(Input.GetButtonDown("Fire1")){
				Shoot();
			}
		}
		else
		{
			if(Input.GetButton("Fire1")&&Time.time>timeToFire)
			{
				timeToFire=Time.time+1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot(){
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x,firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition,mousePosition-firePointPosition, 100,whatToHit);
		if (Time.time >= timeToSpawnBullet) {
			Effect ();
			timeToSpawnBullet=Time.time +1/bulletSpawnrate;
		}

		// Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100);

		if (hit.collider != null) {
			Debug.DrawLine(firePointPosition,hit.point,Color.red);
			Debug.Log("We hit "+ hit.collider.name + " and did " + Damage+ " damage.");
		}
	}

	void Effect(){
		Instantiate (BulletTrailPrefab,firePoint.position,firePoint.rotation);

	}
}
