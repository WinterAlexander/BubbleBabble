using Assets.Scripts;
using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckAlives : MonoBehaviour
{
    public float deadHeight = -2;
    public bool finished = false;
    public string winningText = "Joueur {0} wins!";
    private ArrayList players;
    private GameObject[] playerId;

	void Start()
	{
		players = new ArrayList(GameObject.FindGameObjectsWithTag("Player"));
        playerId = new GameObject[players.Count];
        
        for(int i = 0; i < players.Count; i++)
        {
            string name = ((GameObject)players[i]).name;
            int id = System.Int32.Parse(name[name.Length - 1].ToString());
            playerId[id - 1] = (GameObject)players[i];
        }
	}

	void Update()
	{
        if (finished)
            return;

		for(int i = 0; i < players.Count; i++)
		{
			GameObject player = players[i] as GameObject;

			if(player.transform.position.y < deadHeight)
			{
				player.GetComponent<PlayerComponent>().Kill();
				players.Remove(player);
				i--;

				if(players.Count == 1)
				{
                    for(int j = 0; j < playerId.Length; j++)
                    {
                        if (playerId[j].Equals(players[0]))
                        {
                            Win(j);
                            return;
                        }
                    }					
				}
			}
		}
	}

	public void Win(int winner)
	{
        finished = true;

        Freeze.FreezeFrame(5f);
        Freeze.FreezeComponent fc = Camera.main.gameObject.GetComponent<Freeze.FreezeComponent>();
        if(fc.action == null)
        {
            fc.action = new GoToMenuAction();
            GameObject winnerCanvas = GameObject.Find("WinnerCanvas");
            Text text = winnerCanvas.GetComponentInChildren<Text>();
            string couleur = "";
            
            switch(winner)
            {
                case 0:
                    couleur = "rouge";
                    break;
                case 1:
                    couleur = "bleu";
                    break;
                case 2:
                    couleur = "jaune";
                    break;
                case 3:
                    couleur = "vert";
                    break;
            }
            text.text = string.Format(winningText, couleur);
        }
                            
	}

	public ArrayList GetPlayers()
	{
		return players;
	}
}
