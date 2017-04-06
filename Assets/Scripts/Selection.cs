using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public int selected = 0;
    public string playerCount = "Joueurs : {0}";

    private Text[] texts;
    private bool canMove;
    private bool canChangePCount;
    private Camera cam;
 

    void Start () {

        cam = Camera.main;
        texts = gameObject.GetComponentsInChildren<Text>();
        canMove = true;
        canChangePCount = true;
    }

	void Update ()
    {
        cam.transform.position = new Vector3(
              cam.transform.position.x + Mathf.Cos(Time.time / 20) / 100,
              cam.transform.position.y + Mathf.Sin(Time.time)/500,
              cam.transform.position.z + Mathf.Sin(Time.time)/500
          );

        cam.transform.LookAt(new Vector3(0, 0, 5));
        texts[1].text = string.Format(playerCount, Config.playerCount);
        if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump"))
        {
            if (selected == 0)
                SceneManager.LoadScene("BattleRoyale");
        }

        if(selected == 1 && canChangePCount)
        {
            if (Input.GetAxis("Horizontal_1") > 0.5f && Config.playerCount < 4)
            {
                Config.playerCount++;
                StartCoroutine(WaitTogglePlayer());
            }
            else if (Input.GetAxis("Horizontal_1") < -0.5f && Config.playerCount > 2)
            {
                Config.playerCount--;
                StartCoroutine(WaitTogglePlayer());
            }
        }

        float d = Input.GetAxis("Vertical_All");

        if(Input.GetKey(KeyCode.DownArrow))
        {
            d = 1f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            d = -1f;
        }

        if (canMove && d >= 0.5f){
            selected--;
            StartCoroutine(Wait());
        }
        else if (canMove && d <= -0.5f)
        {
            selected++;
            StartCoroutine(Wait());
        }

   
        if (selected < 0)
        {
            selected = texts.Length - 1;
        }     
        else if (selected >= texts.Length)
            selected = 0;

		for(int i = 0; i < texts.Length; i++)
        {
            Text t = texts[i];
            t.fontStyle = i == selected ? FontStyle.Bold : FontStyle.Normal;
        }
	}

    IEnumerator Wait()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

    IEnumerator WaitTogglePlayer()
    {
        canChangePCount = false;
        yield return new WaitForSeconds(0.3f);
        canChangePCount = true;
    }
}
