using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	//pontos do jogador
	public static int fruitsInBag;
	public static int fruitsInHouse;
	public static float time;
	public Text textInBag;
	public Text textInHouse;
	public Text textTime;

    //imagem de pause no inspector
    public GameObject imagePause, easterEgg;

    //objetos de vida do jogador na hud
    public Image lifeHud;
    public Sprite[] spriteLife;

    //pause do jogo
    public static bool pauseGame;

	// Use this for initialization
	void Start () 
	{
        //variaveis no valor padrão
        fruitsInHouse = 0;
		fruitsInBag = 0;
		time = 120f;
        MusicScenes.playSound = true;
        pauseGame = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Atualizando frutas que estao na bag // tempo // e itens que estao em casa
		textInBag.text = "Mochila: " + fruitsInBag;
		textInHouse.text = "Estoque: " + fruitsInHouse;
		textTime.text = "Tempo: " + time.ToString("0"); //convertendo para string para o tempo nao ficar quebrado

		//diminuindo o tempo a cada 1 segundo
		time -= Time.deltaTime;

		//mudando a sprite da hud conforme o valor de vida da int "lifePlayer"
		switch(Player.lifePlayer)
		{
			case 0: 
				lifeHud.sprite = spriteLife[0]; // 3 vidas
				break;
			case 1: 
				lifeHud.sprite = spriteLife[1]; // 2 vidas 
				break;
			case 2: 
				lifeHud.sprite = spriteLife[2]; // 1 vida
				break;
			case 3: 
				lifeHud.sprite = spriteLife[3]; // 0 vida
				break;
		}

		//perdendo ao zerar o tempo
		if(time <= 0)
		{
			time = 0;
            SceneManager.LoadScene("06 - OverScene");
        }

        //condição para pausar o jogo 
        if (Input.GetKeyDown(KeyCode.P))
        {
            //tirando do pause
            if (pauseGame == false)
            {
                imagePause.SetActive(true); // ativando imagem de pause
                Time.timeScale = 0;
                pauseGame = true;
            }
            else //pausando
            {
                imagePause.SetActive(false); // desativando imagem de pause
                Time.timeScale = 1;
                pauseGame = false;
            }
        }

        //easteregg
        if (Player.easterEgg >= 5000)
        {
            easterEgg.SetActive(true);
            Player.easterEgg = 0;
        }

    }
}