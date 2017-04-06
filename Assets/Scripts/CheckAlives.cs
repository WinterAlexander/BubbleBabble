using Assets;
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
    public string winningText = "{0} a gagné!";
    private ArrayList players;
    private GameObject[] playerId;

	void Start()
	{
        GetComponent<AudioSource>().time = Config.musicTime;
        GameObject[] images = GameObject.FindGameObjectsWithTag("UIImage");
        players = new ArrayList(GameObject.FindGameObjectsWithTag("Player"));

        playerId = new GameObject[Config.playerCount];
        
        for(int i = 0; i < players.Count; i++)
        {
            string name = ((GameObject)players[i]).name;
            int id = System.Int32.Parse(name[name.Length - 1].ToString());
            if (id <= Config.playerCount)
            {
                playerId[id - 1] = (GameObject)players[i];            
            }
            else
            {

                for(int j = 0; j < images.Length; j++)
                {
                    string imName = images[j].name;
                    
                    int imId = int.Parse(imName[imName.Length - 1].ToString());
           
                    if (imId == id - 1)
                    {
                        GameObject.Destroy((GameObject)images[j].GetComponent<Transform>().parent.gameObject);
                        break;
                    }
                }
                GameObject.Destroy((GameObject)players[i]);
                players.Remove(players[i]);
                i--;      
            }
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

        Freeze.FreezeFrame(3f);
        Freeze.FreezeComponent fc = Camera.main.gameObject.GetComponent<Freeze.FreezeComponent>();
        if(fc.action == null)
        {
            GoToSceneAction sceneAction = new GoToSceneAction();
            sceneAction.scene = "BattleRoyale";
            fc.action = sceneAction;
            
            GameObject winnerCanvas = GameObject.Find("WinnerCanvas");
            Text text = winnerCanvas.GetComponentInChildren<Text>();
            string couleur = "";
            
            //TODO Redo This
            switch(winner)
            {
                case 0:
                    couleur = "Rouge";
                    break;
                case 1:
                    couleur = "Bleu";
                    break;
                case 2:
                    couleur = "Jaune";
                    break;
                case 3:
                    couleur = "Vert";
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
