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
        Text[] titleRemover = new Text[texts.Length - 1];
        
        for(int i = 1; i < texts.Length; i++)
        {
            titleRemover[i - 1] = texts[i];
        }
     
        texts = titleRemover;
        canMove = true;
    }

	void Update ()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if (selected == 0)
                SceneManager.LoadScene("BattleRoyale");
            else if(selected == 1)
                SceneManager.LoadScene("Tutorial");
            else if (selected == 2)
                SceneManager.LoadScene("Options");
        }

        float d = Input.GetAxis("Vertical_All");
     

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

    IEnumerator Wait()
    {
        canMove = false;
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
}
