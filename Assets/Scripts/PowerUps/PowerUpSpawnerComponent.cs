using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnerComponent : MonoBehaviour
{

    public int itemsAllowed = 1, spawnTime = 200;
    private System.Random rand = new System.Random();

    private int currentItems;
    private GameObject[] spawners;

    public GameObject spawnedObject;
    public GameObject itemToSpawn;
    private static PowerUpType lastItemType = PowerUpType.NONE;
    void Start()
    {
    }

    void Update()
    {
        spawners = GameObject.FindGameObjectsWithTag("ItemSpawner");
        currentItems = 0;
        foreach (GameObject g in spawners)
        {
            if (g.GetComponent<PowerUpSpawnerComponent>().spawnedObject != null)
                currentItems++;
        }

        if (currentItems < itemsAllowed 
            && spawnedObject == null            
            && rand.Next(spawnTime) == 0)
        {          
            spawnedObject = GameObject.Instantiate(itemToSpawn, gameObject.transform.position, Quaternion.identity);
            PowerUpType newItemType;
            do
            {
                newItemType = (PowerUpType)(rand.Next(Enum.GetNames(typeof(PowerUpType)).Length - 1)) + 1;              
            } while (newItemType == lastItemType);
            lastItemType = newItemType;
            spawnedObject.GetComponent<PowerUpItem>().type = newItemType;
        }
    }
}