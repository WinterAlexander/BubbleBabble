using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAlives : MonoBehaviour
{
	public float deadHeight = -2;

	private ArrayList players;

	void Start()
	{
		players = new ArrayList(GameObject.FindGameObjectsWithTag("Player"));
	}

	void Update()
	{
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
		
	}

	public ArrayList GetPlayers()
	{
		return players;
	}
}
