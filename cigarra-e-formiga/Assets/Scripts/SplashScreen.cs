using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour 
{
    //objetos no inspector
    public GameObject imageFormiga, imageEstacio;

	// Use this for initialization
	void Start () 
	{
        //chamando coroutine da imagem da formiga 
        StartCoroutine ("ImageSplashFormiga");
    }

    IEnumerator ImageSplashFormiga()
    {
        yield return new WaitForSeconds(9.0f);

        imageFormiga.SetActive(false); // desativando imagem formiga
        imageEstacio.SetActive(true); // ativando imagem estácio

        //desativando imagem da estacio
        StartCoroutine("ImageSplashEstacio");
    }

    IEnumerator ImageSplashEstacio()
    {
        yield return new WaitForSeconds(9.0f);

        imageEstacio.SetActive(false);

        //desativando imagem da estacio
        SceneManager.LoadScene("01 - MenuScene");
    }

}
