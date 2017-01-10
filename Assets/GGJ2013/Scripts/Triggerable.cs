using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Triggerable : MonoBehaviour 
{
	public List<string> TriggerIDs;
	public abstract void OnTriggerActivated(TriggerEvenArgs e);
	private void TriggerActivated(TriggerEvenArgs e)
	{
		if (TriggerIDs.Contains(e.T.TriggerID))
		{
			OnTriggerActivated(e);
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
}
