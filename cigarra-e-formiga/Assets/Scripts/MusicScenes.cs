using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicScenes : MonoBehaviour 
{
    //musica do MenuScene e GameScene
    public AudioSource audioSourceLooping; //audioSourceNoLooping;

    //audios de menu e de jogo
    public AudioClip audioMenu, audioGame, audioGameOver, audioGameWin;

    //correção de som
    public static bool playSound;
    public static bool changeAudio;

	void Awake()
	{
		//evitando que o objeto se destrua
		DontDestroyOnLoad(gameObject);

	}//feacha awake
	
	// Use this for initialization
	void Start () 
	{
        //variaveis no valor padrão
        playSound = true;
        changeAudio = false;

	}//fecha start
	
	// Update is called once per frame
	void Update () 
	{
        //condição extra para corrigir o bug de som ao terminar o jogo, quando o jogo for reiniciado ele começar com o som de menu e não com o som anterior
        if(changeAudio == true)
        {
            audioSourceLooping.clip = audioMenu;
            audioSourceLooping.Play();
            changeAudio = false;
        }

		//condicao para pausar o som vinda de outro script
		if(ButtonAudio.clicked == true)
		{	
			//tirando do pause
			if (audioSourceLooping.mute == true)
			{
				audioSourceLooping.mute = false;

				//mudando imagem - animacao
				ButtonAudio.changeImageToDesable = false;
			}
			else //pausando
			{
				audioSourceLooping.mute = true;
				ButtonAudio.changeImageToDesable = true;
			}

			ButtonAudio.clicked = false;
		}

        //condições para tocar os audios corretos em cada cena
        if (SceneManager.GetActiveScene().name == "01 - MenuScene")
        {
            if (playSound == true)
            {
                audioSourceLooping.clip = audioMenu;
                audioSourceLooping.Play();
                playSound = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "05 - GameScene")
        {
            if(playSound == true)
            {
                audioSourceLooping.clip = audioGame;
                audioSourceLooping.Play();
                playSound = false;
            }
            
        }
        if (SceneManager.GetActiveScene().name == "06 - OverScene")
        {
            if (playSound == true)
            {
                audioSourceLooping.clip = audioGameOver;
                audioSourceLooping.loop = false; //desabilitando looping do audiosorce para tocar a musica de perdeu uma vez
                audioSourceLooping.Play();
                playSound = false;
            }
        }
        if (SceneManager.GetActiveScene().name == "07 - WinScene")
        {
            if (playSound == true)
            {
                audioSourceLooping.clip = audioGameWin;
                audioSourceLooping.loop = false; //desabilitando looping do audiosorce para tocar a musica de ganhou uma vez
                audioSourceLooping.Play();
                playSound = false;
            }
        }

    }//fecha update

}//fecha classe