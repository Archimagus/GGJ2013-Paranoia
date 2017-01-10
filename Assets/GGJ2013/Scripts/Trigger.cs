using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class Trigger : MonoBehaviour 
{
	public string TriggerID;
	public int TriggerValue;
	public bool RequiresInteraction;
	public KeyCode InteractKey;
	public string InteractMessage;
	/// <summary>
	/// Collectable items that the player must have collected to use this trigger.
	/// </summary>
	public string[] requiredItems;


	public bool interactionReady { get; private set; }
	public string cantInteractReason { get; private set; }

	public EventHandler<TriggerEvenArgs> TriggerActivated;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (interactionReady && Input.GetKeyDown(InteractKey))
		{
			OnTriggerAvtivated();
		}
	}

	void OnGUI()
	{
		if (interactionReady)
		{
			var c = new GUIContent(InteractKey.ToString() + ": " + InteractMessage);
			var size = GUI.skin.label.CalcSize(c);
			GUI.Label(new Rect(Screen.width / 2.0f - size.x / 2.0f, 10, size.x, size.y + 10), c);
		}
		else if (!string.IsNullOrEmpty(cantInteractReason))
		{
			var c = new GUIContent(cantInteractReason);
			var size = GUI.skin.label.CalcSize(c);
			GUI.Label(new Rect(Screen.width / 2.0f - size.x / 2.0f, 10, size.x, size.y + 10), c);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (HasNeededAllNeededItems())
		{
			if (RequiresInteraction)
			{
				interactionReady = true;
			}
			else
			{
				OnTriggerAvtivated();
			}
		}
	}
	private bool HasNeededAllNeededItems()
	{
		bool retVal = true;
		string reason = "I don't have ";
		foreach (var item in requiredItems)
		{
			if (!Inventory.Instance.HasItem(item))
			{
				reason += item + " & ";
				retVal = false;
			}
		}
		if (!retVal)
		{
			cantInteractReason = reason.Remove(reason.LastIndexOf("&") - 1);
		}
		return retVal;
	}

	private void OnTriggerAvtivated()
	{
		foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("TriggerActivated", new TriggerEvenArgs(this), SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerExit(Collider other)
	{
		interactionReady = false;
		cantInteractReason = string.Empty;
	}
}
public class TriggerEvenArgs : EventArgs
{
	public Trigger T;
	public TriggerEvenArgs(Trigger t)
	{
		T = t;
	}
}