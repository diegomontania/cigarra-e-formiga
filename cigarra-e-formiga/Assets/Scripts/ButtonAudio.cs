using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// <company>Harmonia Game Studio</company>
/// <game>Jogo Site</game>
/// <author>Diego Alves</author>
/// <gameobject>buttonAudio</gameobject>
/// <date>20/07/2015</date>
/// <explanation>Botao de audio no jogo, mudando sprites do botao e habilitando ou desabilitando o som</explanation>
/// <revisions>Diego Alves</revisions>
/// </summary>

public class ButtonAudio: MonoBehaviour 
{
	//imagem do botao de audio
	public Sprite soundEnable;
	public Sprite soundDesable;
	
	//variavel para mudança de imagem do botao de audio
	public static bool changeImageToDesable;

	//desabilitando som
	public static bool disableSound;

	//verificando se ja foi clicado
	public static bool clicked;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//mudando imagem usando a nova gui ("Image" nome do componente que o objeto precisa ter)
		if(changeImageToDesable == false)
		{
			gameObject.GetComponent<Image> ().sprite = soundEnable;
		}
		else
		{
			gameObject.GetComponent<Image> ().sprite = soundDesable;
		}

	}//fecha update
	
	public void ButtonSound()
	{
		clicked = true;

	}//fecha button audio
	
}//fecha classe