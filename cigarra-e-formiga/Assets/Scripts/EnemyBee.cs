using UnityEngine;
using System.Collections;

public class EnemyBee : MonoBehaviour 
{
    //velocidade abelha
    public float velocityBee;

	//posicao maxima e minima de movimentacao da abelha
	public float maxPositionX;
	public float minPositionX;

	void Start()
	{
		velocityBee = 3;
	}

	void Update () 
	{
		//movimentacao dos inimigos
		Moviment();
	}

	void Moviment()
	{
		//movimentando abelha
		this.transform.Translate(velocityBee * Time.deltaTime, 0, 0f);

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
}//fecha classe
