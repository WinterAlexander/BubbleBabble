using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour {

    private static int itemsAllowed = 1;
    private static System.Random rand = new System.Random();
    private int currentItems;
    private GameObject[] spawners;
    public GameObject spawnedObject;
    public GameObject itemToSpawn;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        spawners = GameObject.FindGameObjectsWithTag("ItemSpawner");
        currentItems = 0;
	    foreach(GameObject g in spawners)
        {
            if (g.GetComponent<SpawnPowerUps>().spawnedObject != null)
                currentItems++;
        }

        Debug.Log(spawners.Length);
        if(currentItems < itemsAllowed)
        {
            bool spawn = rand.Next() % 500 == 0;
            itemToSpawn.GetComponent<PowerUp>().type = (PowerUpType)(rand.Next() % Enum.GetNames(typeof(PowerUpType)).Length - 1) + 1;
            if(spawn)
                spawnedObject = GameObject.Instantiate(itemToSpawn, gameObject.transform.position, Quaternion.identity);
        }
	}
}
