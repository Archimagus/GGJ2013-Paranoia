using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Light attachedLight;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		var lightPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		attachedLight.transform.position = lightPos;
	}
}
