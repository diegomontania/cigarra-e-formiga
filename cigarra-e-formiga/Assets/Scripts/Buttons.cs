using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour 
{
	public AudioSource audioSource;
	public AudioClip menuHover;
	public AudioClip menuClick;

	public void SoundHover ()
	{
		audioSource.PlayOneShot(menuHover);
	}

	public void SoundClick ()
	{
		audioSource.PlayOneShot(menuClick);
	}

    //botoes de menu, chamando enumerators para a troca de level ocorrer com um pequeno atraso e dar tempo do audio de "click" tocar.
    public void GoTo(string goToScene)
    {
        switch(goToScene)
        {
            case "gameScene":
                StartCoroutine(EnumratorGame());
                break;
            case "instructionScene":
                StartCoroutine(EnumratorInstructions());
                break;
            case "historyScene":
                StartCoroutine(EnumratorHistory());
                break;
            case "creditScene":
                StartCoroutine(EnumratorCredit());
                break;
            case "exit":
                StartCoroutine(EnumratorExit());
                break;
            case "back":
                StartCoroutine(EnumratorBack());
                break;
        }
    }

	public void NextHistory()
	{
		//adicionando valor a variavel para mudar a imagem da cena e contar a historia
		History.nextHistory ++;
	}

    //fazendo o audio de menu voltar a funcionar quando o jogador pressionar o botão de back nas cenas de ganhou e perdeu // pausando musica de perdeu ou ganhou
    public void BackGame()
    {
        //procurando o objeto de musicScenes que irá ser instanciado
                                            //nome do objeto no unity
        GameObject musicScenes = GameObject.Find("musicScenes");
        //nome do script // variavel para acessar o script pelo componente
        MusicScenes musicScript = musicScenes.GetComponent<MusicScenes>();

        MusicScenes.changeAudio = true;
        musicScript.audioSourceLooping.loop = true;
        StartCoroutine(EnumratorBack());
    }
	
	#region EnumratorsDosLeveis
	//enumerators para a troca de level ocorrer com um pequeno atraso e dar tempo do audio tocar.
	IEnumerator EnumratorGame()
	{
		yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("05 - GameScene");
	}

	IEnumerator EnumratorInstructions()
	{
		yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("03 - InstructionScene");
    }
	
	IEnumerator EnumratorHistory()
	{
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("02 - HistoryScene");
    }
	
	IEnumerator EnumratorCredit()
	{
		yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("04 - CreditScene");
	}

	IEnumerator EnumratorBack()
	{
		yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("01 - MenuScene");
	}

	IEnumerator EnumratorExit()
	{
		yield return new WaitForSeconds(0.3f);
		Application.Quit();
	}

	#endregion

}// fecha classe