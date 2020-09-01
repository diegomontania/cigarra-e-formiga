using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour 
{
	public GameObject creditText;
	public GameObject positionMaxY;
	public GameObject positionMinY;
	
	public float velocityRollUp;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//moovendo harmoniaText e othersText
		transform.Translate (0 , velocityRollUp * Time.deltaTime, 0);
		
		//limitando o harmoniaTexto
		if(creditText.transform.localPosition.y > positionMaxY.transform.localPosition.y +2)
		{
			//fazendo ele resetar a posicao
			creditText.transform.localPosition = new Vector3 (9, positionMinY.transform.localPosition.y - creditText.transform.localPosition.y / 2, 0);
		}
	}
}
