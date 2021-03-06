﻿using UnityEngine;
using System.Collections;

public class Cicada : MonoBehaviour
{
    //animacao
    private Animator animator;

    //velocidade abelha
    public float velocity;

    //colisor
    private BoxCollider2D boxCollider2D;

    //posicao maxima e minima de movimentacao da abelha
    public float maxPositionX;
    public float minPositionX;

    //objeto do jogador
    public GameObject Player;

    void Start()
    {
        //Chamando Animator do objeto
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Moviment();
    }

    void Moviment()
    {
        //movimentando
        this.transform.Translate(velocity * Time.deltaTime, 0, 0f);

        if (this.transform.localPosition.x >= maxPositionX)
        {
            //Flipando personagem, como ele esta sendo flipado aqui automaticamente ele ira andar para o lado oposto
            transform.eulerAngles = new Vector2(0, 180);
        }
        else if (this.transform.localPosition.x <= minPositionX)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    //parando a cigarra e o player 
    //e mudando animacao ao encostar no player
    void Dancing()
    {
        animator.SetTrigger("dancingCicadaTrigger");
        Player.GetComponent<Player>().TriggerDancing = true; // fazendo jogador dançar
        velocity = 0;
        StartCoroutine(StopDancing());
        boxCollider2D.enabled = false; //desabilitando collider da cigarra
    }
    IEnumerator StopDancing()
    {
        yield return new WaitForSeconds(5.0f);
        velocity = 3;
        Moviment();
        StartCoroutine(EnumratorEnaableCollider()); // habilitando collider da cicada
        animator.SetTrigger("runningCicadaTrigger");
    }

    IEnumerator EnumratorEnaableCollider()
    {
        yield return new WaitForSeconds(5.0f);
        boxCollider2D.enabled = true; // habilitando collider da cicada
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            Dancing();
        }
    }
}
