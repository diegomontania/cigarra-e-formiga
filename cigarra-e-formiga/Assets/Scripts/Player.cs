using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	//enum para escolher qual funçao de movimento o jogador ira usar dependendo da plataforma selecionada
	public enum Platform
	{
		Mobile,
		Web
	}
	
	//Isto é o que você precisa mostrar, no inspetor.
	public Platform platform;
	private bool mobile;
	private bool web;
	
	//variavel para o objeto vazio no pe do player
	public Transform ground;
	
	//Variaveis para animacao do player
	public Transform player;
	private Animator animator;
	
	//Variavel pulo
	public bool isGrounded;
	
	//Verifica se ele esta pulando
	public bool jumped;

	//animacao rodando no mobile
	public static bool animationMobile;
	
	//boleana para evitar que a animaçao de idle entre em conflito com a animacao de run
	public static bool idle;
	
	//condicao para parar o player ao encostar na placa easteregg
	public bool disableKeyboard;

	//condicao para habilitar os botoes conforme a plataforma em especfico
	public static bool activeButtonsPlataformGame;

    //condicao para o jogador dancar
    private bool dancingPlayer;
	public bool TriggerDancing { get; set; }

	//vida do jogador
	public static int lifePlayer;

	//ajuste de camera ao tocar a plataforma
	public static float cameraAdjustment;
	
	//Variavel para movimento do player
	public static float velocityPlayer;
	
	//Forca que o jogador ira pular
	public float force;
	
	//Tempo que ele pode pular
	public float jumpTime;
	
	//Tempo de subida e execucao da animacao
	public float jumpDelay;

    //easteregg
    public static int easterEgg;

	//Botoes e objeto da hud mobile no inspector
	//public GameObject buttonMobileJump, buttonMobileLeft, buttonMobileRight;

	// Use this for initialization
	void Start()
	{
		//Chamando Animator do objeto Player
		animator = GetComponent<Animator>();

		//valores padrao
		animator.Play("idle");
		lifePlayer = 3;
		velocityPlayer = 5;
		easterEgg = 0;
		cameraAdjustment = 6.2f;
		lifePlayer = 3;
		idle = true;

		//Escolhendo plataforma pelo enum
		switch (platform)
		{
			case Platform.Web:
				web = true;
				break;
			case Platform.Mobile:
				mobile = true;
				break;
		}

		//ativando botoes no mobile se o enum estiver na condicao " mobile "
		//buttonMobileJump.SetActive(mobile);
		//buttonMobileLeft.SetActive(mobile);
		//buttonMobileRight.SetActive(mobile);
	}
	
	// Update is called once per frame
	void Update()
	{
        //parando de movimentar o jogador no pause
        if(GameManager.pauseGame == false)
        {
			//ativa bolean que faz dançar
            if(TriggerDancing)
            {
                //dancando
                Dancing();
				dancingPlayer = true;
				TriggerDancing = false; //desabilita para evitar loop no update
			}
            else if(!dancingPlayer)
            {
                Moving();
                MovimentAnimation();
                Jumping();
                DeadPlayer();
            }
        }
		
		//parametro do animator e comparando com a boleana
		animator.SetBool("idle", idle);

        /*condicao para os botoes no mobile serem ativados
		if(ButtonMobile.enableButtons == true)
		{
			EnableButtonsMobile();
			ButtonMobile.enableButtons = false;
		}*/
    }
	
	//Movimento
	void Moving ()
	{
		//faz com que o personagem nao va para a animaçao idle se mudar a movimentaçao da direita para a esquerda e vice versa - evitando bug
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
		{
			idle = false;
		}
		else
		{	
			idle = true;
		}
		
		//Movimentando direita
		if (Input.GetAxisRaw ("Horizontal") > 0 && disableKeyboard == false)// || ButtonMobile.horizontal > 0 )
		{
			//Movimentando usando Vector2, velocidade e deltatime (melhorando desempenho)
			transform.Translate (Vector2.right * velocityPlayer * Time.deltaTime);
			
			//Flipando personagem
			transform.eulerAngles = new Vector2 (0, 0);
		}
		
		//Movimentando esquerda 
		//[Vector2 = x,y] ≠ [Vector3 = x,y,z]
		if (Input.GetAxisRaw ("Horizontal") < 0 && disableKeyboard == false) //|| ButtonMobile.horizontal < 0 )
		{
			transform.Translate (Vector2.right * velocityPlayer * Time.deltaTime);
			
			//Flipando personagem, como ele esta sendo flipado aqui automaticamente ele ira andar para o lado oposto
			transform.eulerAngles = new Vector2 (0, 180);
		}
	}
	
	//Animacao
	void MovimentAnimation ()
	{
		if (web == true)
		{
			//Chamando animacao no movimento
			animator.SetFloat ("run", Mathf.Abs (Input.GetAxis ("Horizontal")));
		}
		if (mobile == true)
		{
			//ativando boleana para animacao rodar no mobile
			if(animationMobile == true)
			{
				animator.SetFloat ("run", Mathf.Abs (1));
			}
			else
			{
				animator.SetFloat ("run", Mathf.Abs (0));
			}
		}
	}
	
	//jump
	void Jumping ()
	{
		isGrounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer ("Plataforma"));
		
		//Pulo
		if (Input.GetKeyDown (KeyCode.Space)&& isGrounded && !jumped  || Input.GetKey (KeyCode.UpArrow) && isGrounded && !jumped)// || ButtonMobile.jump == true && isGrounded && !jumped) 
		{
			GetComponent<Rigidbody2D>().AddForce (transform.up * force);
			jumpTime = jumpDelay;
			animator.SetTrigger("jump");
			jumped = true;
			//animator.Play("Jump-final");
		}
		
		jumpTime -= Time.deltaTime;
		
		if (jumpTime <= 0 && isGrounded && jumped)
		{
			animator.SetTrigger("ground");
			jumped = false;
		}

	}

    //dancando
    void Dancing ()
    {
		velocityPlayer = 0;
		animator.SetBool("dancingPlayer", true);
		StartCoroutine(StopDancing());
	}
	IEnumerator StopDancing()
	{
		yield return new WaitForSeconds(5.0f);
		animator.SetBool("dancingPlayer", false);
		velocityPlayer = 5;
		dancingPlayer = false;
	}

	//perdendo
	void DeadPlayer()
	{
		if(lifePlayer <= 0)
		{
			//esperando um tempo para perder = dar tempo da animacao de morte rodar
			StartCoroutine(EnumeratorDeadPlayer());
		}
	}
	IEnumerator EnumeratorDeadPlayer()
	{
		yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("06 - OverScene");
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//coletando frutas
		if(other.gameObject.CompareTag("fruit"))
		{
			GameManager.fruitsInBag ++;
		}

		//colindo com os inimigos e diminuindo hp
		if(other.gameObject.CompareTag("enemy"))
		{
			lifePlayer -= 1;
		}

        //mudando posição da camera em Y para poder corrigir visão do jogador
        if (other.gameObject.CompareTag("adjustCameraY"))
        {
            CameraFollow.smooth = 0.5f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //resetando valores iniciais
        if (other.gameObject.CompareTag("adjustCameraY"))
        {
            CameraFollow.smooth = 0.2f;
        }
    }

    void OnCollisionEnter2D (Collision2D other)
	{
		//mudando de cena assim que colidir ao colidir com o deadcollider
		if(other.gameObject.CompareTag("deadCollider"))
		{
			lifePlayer = 0;
            SceneManager.LoadScene("06 - OverScene");
        }

		//mudando de cena assim que colidir ao colidir com o deadcollider
		if(other.gameObject.CompareTag("colliderHouse"))
		{
			//recebendo o valor dos itens que estao na mochila e passando para a casa 
			//e resetando o valor dos itens na mochila e resetando a velocidade do jogador
			GameManager.fruitsInHouse += GameManager.fruitsInBag;
			GameManager.fruitsInBag = 0;
			GameManager.time = 120;
			velocityPlayer = 5;
		}   
	}

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("colliderHouse"))
        {
            easterEgg += 1;
        }
    }

}//fecha classe