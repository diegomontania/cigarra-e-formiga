using UnityEngine;
using System.Collections;

public class ResetSound : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        //resetando variavel de som para evitar bug no menu
        MusicScenes.playSound = true;
    }
}
