using UnityEngine;
using System.Collections;

public class Bee : MonoBehaviour 
{
	//velocidade abelha
	public float velocityEnemy;

	//posicao maxima e minima de movimentacao da abelha
	public float maxPositionX;
	public float minPositionX;

	void Start()
	{
		
	}

	void Update () 
	{
		//movimentacao dos inimigos
		Moviment();
	}

	void Moviment()
	{
		//movimentando abelha
		this.transform.Translate(velocityEnemy * Time.deltaTime, 0, 0f);

		//se chegar a posicao maxima a abelha sera flipada
		if(this.transform.localPosition.x >= maxPositionX)
		{
			//Flipando personagem, como ele esta sendo flipado aqui automaticamente ele ira andar para o lado oposto
			transform.eulerAngles = new Vector2 (0, 180);
		}
		else if(this.transform.localPosition.x <= minPositionX)
		{
			transform.eulerAngles = new Vector2 (0, 0);
		}
	}


	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "player")
		{
			
		}
	}

}//fecha classe
