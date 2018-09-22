﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 10;
	public float Damage = 10;
	public LayerMask whatToHit;

	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;

	float timeToSpawnEffect = 0;
	float effectSpawnRate = 10;

	float timeToFire = 0;
	Transform firePoint;
	Transform muzzlePos;

	//public GameObject myGameObject;

	// Use this for initialization
	void Awake () {
		firePoint = transform.Find ("FirePoint");
		muzzlePos = transform.Find ("MuzzlePos");
		if(firePoint == null)
		{
			Debug.LogError ("No, firepoint? WHAT?!!");
		}
		if (muzzlePos == null) 
		{
			Debug.LogError ("No muzzlePos? What?!!");
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Shoot ();
		if (fireRate == 0)
		{
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
			else 
			{
				if (Input.GetButton ("Fire1") && Time.time > timeToFire) 
				{
					timeToFire = Time.time + 1/fireRate;
					Shoot ();
				}

			} //End of else
		} //End of if (fireRate == 0)


	} //End of Update ()

	void Shoot ()
	{
		//Debug.Log ("Test");
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
		if(hit.collider != null){
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We Hit " + hit.collider.name + "and did " + Damage + " Damage.");
		}



	} //End of void Shoot

	//This is the effect that will make Bullet Trail appear
	void Effect(){
		Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = Instantiate (MuzzleFlashPrefab, muzzlePos.position, muzzlePos.rotation) as Transform;
		clone.parent = muzzlePos;
		float size = Random.Range(0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);

	}

}