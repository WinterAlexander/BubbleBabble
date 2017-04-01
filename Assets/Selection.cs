using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public int selected = 0;
    private Text[] texts;
    private bool canMove;

	void Start () {
      texts = gameObject.GetComponentsInChildren<Text>();
        Text[] titleRemover = new Text[texts.Length - 1];

        for(int i = 1; i < texts.Length; i++)
        {
            titleRemover[i - 1] = texts[i];
        }

        texts = titleRemover;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetButtonDown("Submit"))
        {
            if (selected == 0)
                SceneManager.LoadScene("BattleRoyale");
        }



        float d = Input.GetAxis("Vertical");

        if (d == 0)
            canMove = true;
        else if (canMove && d > 0){
            selected++;
            canMove = false;
        }
        else if (canMove && d < 0)
        {
            selected--;
            canMove = false;
        }

      

        if (selected < 0)
            selected = texts.Length - 1;
        else if (selected >= texts.Length)
            selected = 0;

		foreach(Text t in texts)
        {
            t.color = Color.black;
        }

        texts[selected].color = Color.cyan; 
	}
}
