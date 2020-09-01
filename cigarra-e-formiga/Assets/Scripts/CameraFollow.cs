using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour 
{
	//seguir jogador
	public Transform player;

	//camera delay = tempo de atraso
	public static float smooth = 0.2f;

	//posicao Y e X camera	
	public static float initialPositionYCamera = 3.88f;
	public static float initialPositionXCamera = 0;

	//velocidade camera
	private Vector2 velocityCamera;

	// Use this for initialization
	void Start () 
	{
		velocityCamera = new Vector2 (0.5f, 0.5f);

	}//fecha Start
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 newPosition2D = Vector2.zero;

		//posicao da camera x                                                   //varivel para mover a camera
		newPosition2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x + initialPositionXCamera, ref velocityCamera.x, smooth);

		//posicao da camera y                                                   //varivel para mover a camera
		newPosition2D.y = Mathf.SmoothDamp (transform.position.y, player.position.y + initialPositionYCamera, ref velocityCamera.y, smooth);

		Vector3 newPositionCamera = new Vector3(newPosition2D.x , newPosition2D.y, transform.position.z);

		transform.position = Vector3.Slerp(transform.position, newPositionCamera, Time.time);

	}//fecha Update

}//fecha classe
