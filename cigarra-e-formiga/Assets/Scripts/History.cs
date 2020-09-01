using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class History : MonoBehaviour 
{
	//valor para mudar a imagem da cena que ira contar a historia
	public static int nextHistory;

	//objeto de imagem que sera modicado
	public Image imageDefault;

	//sprites da historia
	public Sprite history1;
	public Sprite history2;
	public Sprite history3;
	public Sprite history4;
	public Sprite history5;

	//botoes que apareceram ou desapareceram conforme a necessidade
	public GameObject buttonBack;
	public GameObject buttonNext;

	// Use this for initialization
	void Start () 
	{
		//valor inicial para ja comecar com a primeira historia
		nextHistory = 1;

		//reativando botoes padrao
		buttonNext.SetActive(true);
		buttonBack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(nextHistory)
		{
			case 1:
				imageDefault.sprite = history1;
				break;
			case 2:
				imageDefault.sprite = history2;
				break;
			case 3:
				imageDefault.sprite = history3;
				break;
			case 4:
				imageDefault.sprite = history4;
				break;
			case 5:
				imageDefault.sprite = history5;
				break;
		}

		//mantendo valor maximo da variavel de historia e ativando botao de back
		if(nextHistory >= 5)
		{
			nextHistory = 5;
			buttonBack.SetActive(true);
			buttonNext.SetActive(false);
		}
	}
}
