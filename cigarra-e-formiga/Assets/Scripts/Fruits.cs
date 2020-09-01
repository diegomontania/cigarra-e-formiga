using UnityEngine;
using System.Collections;

public class Fruits : MonoBehaviour 
{
	void OnTriggerEnter2D (Collider2D other) 
	{
		//coletando frutas e dimuindo velocidade do jogador
		if(other.gameObject.tag == "player")
		{
			Player.velocityPlayer = Player.velocityPlayer - 0.5f;

			Destroy(gameObject);
		}
	}
}
