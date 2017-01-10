using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray,out hit,100))
			{
				var mainObject = GameObject.FindWithTag("Player").GetComponent<MainCharacterController>();
				
				var followObject = gameObject.GetComponent<Follower>();
				if (followObject != null && mainObject != null)
					mainObject.StartInteract(followObject);

			}
		}
		
		
	}
}
