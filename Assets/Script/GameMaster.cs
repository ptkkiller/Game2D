﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public int spawnDelay = 2; 
	public Transform spawnPrefab;


	void Update (){
		//Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);	
	}



	public IEnumerator RespawnPlayer (){
		Debug.Log ("TODO: Add waiting for spawn sound");
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		Debug.Log ("TODO: Add Spawn Particles");
	}

	public static void KillPlayer (SecondPlayerScript secondPlayerScript){
		Destroy (secondPlayerScript.gameObject);
		Debug.Log ("Player Killed !!");
		gm.StartCoroutine (gm.RespawnPlayer() );
		Debug.Log ("Player Respawn !!");
	}

}
