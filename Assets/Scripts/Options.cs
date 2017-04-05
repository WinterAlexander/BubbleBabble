using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public string playerCountText = "Nombre de joueurs : {0}";
    public string volumeText = "Volume : {0}";
    private int selection = 0;
    private bool isInBlock = false;
    private float blockingTime = 0.25f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Submit") || Input.GetButton("Jump") || Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3"))
        {
            SceneManager.LoadScene("Menu");
        }

        if (!isInBlock)
        {
            switch(selection)
            {
                case 0:
                    if (Input.GetAxis("Horizontal_1") > 0.5f && Config.playerCount < 4)
                    {
                        Config.playerCount++;
                        StartCoroutine(Wait());
                    }
                    else if (Input.GetAxis("Horizontal_1") < -0.5f && Config.playerCount > 2)
                    {
                        Config.playerCount--;
                        StartCoroutine(Wait());
                    }
                    break;
            }         
        }
        gameObject.GetComponentInChildren<Text>().text = string.Format(playerCountText, Config.playerCount);
    }

    IEnumerator Wait()
    {
        isInBlock = true;
        yield return new WaitForSeconds(blockingTime);
        isInBlock = false;
    }
}
