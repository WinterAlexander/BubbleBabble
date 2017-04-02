using Assets.Scripts;
using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAlives : MonoBehaviour
{
	public float deadHeight = -2;
    public bool finished = false;
	private ArrayList players;

	void Start()
	{
		players = new ArrayList(GameObject.FindGameObjectsWithTag("Player"));
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
					Win(players[0] as GameObject);
					return;
				}
			}
		}
	}

	public void Win(GameObject winner)
	{
        finished = true;
        Freeze.FreezeFrame(5f);
        Freeze.FreezeComponent fc = Camera.main.gameObject.GetComponent<Freeze.FreezeComponent>();
        if(fc.action == null)
            fc.action = new GoToMenuAction();                
	}

	public ArrayList GetPlayers()
	{
		return players;
	}
}
