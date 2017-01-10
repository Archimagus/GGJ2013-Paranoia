using UnityEngine;
using System.Collections;

public class PauseScren : MonoBehaviour 
{
	bool paused;
	public bool Paused 
	{
		get { return paused; }
		set 
		{ 
			paused = value;
			Time.timeScale = paused ? 0 : 1;
		}
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		if (Paused)
		{

			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("Resume"))
			{
				Paused = false;
			}
			if (GUILayout.Button("Quit"))
			{
				Application.LoadLevel("MainMenu");
			}

			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndArea();
		}
	}
}
