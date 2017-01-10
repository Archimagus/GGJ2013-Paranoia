using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Trigger))]
public class Collectable : Triggerable 
{
	public string CollectableID;


	// Use this for initialization
	void Start () 
	{
		if(Inventory.Instance.HasItem(CollectableID))
		{
			GameObject.Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void OnTriggerActivated(TriggerEvenArgs e)
	{
		Inventory.Instance.AddItem(CollectableID);
		GameObject.Destroy(this.gameObject);
	}
}
