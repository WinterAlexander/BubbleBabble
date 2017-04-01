using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selection : MonoBehaviour {

    public int selected = 0;
    public float titleAnimationSpeed;

    private Text[] texts;
    private bool canMove;
    private Text title;

	void Start () {
        texts = gameObject.GetComponentsInChildren<Text>();
        title = texts[0];
        title.transform.Rotate(0, 0, -titleAnimationSpeed * 60);
        Text[] titleRemover = new Text[texts.Length - 1];
        
        for(int i = 1; i < texts.Length; i++)
        {
            titleRemover[i - 1] = texts[i];
        }
     
        texts = titleRemover;
    }

	void Update ()
    {
        title.transform.Rotate(0, 0, Mathf.Sin(Time.time) * titleAnimationSpeed);
        title.transform.localScale = new Vector3(Mathf.Abs(Mathf.Sin(Time.time)) * 0.2f + 0.80f, 
                                                Mathf.Abs(Mathf.Sin(Time.time)) * 0.2f + 0.80f);
        if(Input.GetButtonDown("Submit"))
        {
            if (selected == 0)
                SceneManager.LoadScene("BattleRoyale");
        }

        float d = Input.GetAxis("Vertical_All");

        if (d == 0)
            canMove = true;
        else if (canMove && d < 0){
            selected++;
            canMove = false;
        }
        else if (canMove && d > 0)
        {
            selected--;
            canMove = false;
        }    

        if (selected < 0)
            selected = texts.Length - 1;
        else if (selected >= texts.Length)
            selected = 0;

		for(int i = 0; i < texts.Length; i++)
        {
            Text t = texts[i];
            t.color = i == selected ? Color.cyan : Color.black;

            t.transform.localScale = new Vector3(i == selected ? Mathf.Abs(Mathf.Sin(Time.time)) * 0.2f + 1f : 1,
                                                 i == selected ? Mathf.Abs(Mathf.Sin(Time.time)) * 0.2f + 1f : 1);
        }
	}
}
