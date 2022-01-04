using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MyButton : MonoBehaviour 
{
	public AudioClip mouseOverSound;
	public AudioClip mouseDownSound;
	public AudioClip mouseClickSound;
	public string preference;
	public string preferenceValue;
	public Material mat;
	public Texture def;
	public Texture mouseOver;
	public Texture click;
    bool clickedLastFrame = false;
	bool mouseOverPlayed = false;
	bool mouseDownPlayed = false;
	// Use this for initialization
	protected virtual void Start() 
	{
		mat.SetTexture("_MainTex", def);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
        // this is to prevent buttons that dynamicly replace the clicked button 
        // from getting clicked in the same frame as this button.
        if (clickedLastFrame)
        {
            clickedLastFrame = false;
            Click();
        }
        else
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100) &&
                hit.transform == transform)
            {
                if (Input.GetButton("Fire1") && click != null)
				{
					if (mouseClickSound != null && !mouseDownPlayed)
					{
						GetComponent<AudioSource>().PlayOneShot(mouseDownSound);
						mouseDownPlayed = true;
					}
                    mat.SetTexture("_MainTex", click);
                }
                else if(mouseOver != null)
                {
					if (mouseOverSound != null && !mouseOverPlayed)
					{
						GetComponent<AudioSource>().PlayOneShot(mouseOverSound);
						mouseOverPlayed = true;
					}
                    mat.SetTexture("_MainTex", mouseOver);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    clickedLastFrame = true;
                }
            }
            else
            {
                if (def != null)
                {
					mouseOverPlayed = false;
					mouseDownPlayed = false;
                    mat.SetTexture("_MainTex", def);
                }
            }
        }
	}
	protected virtual void Click()
	{
		if (mouseClickSound != null)
		{
			GetComponent<AudioSource>().PlayOneShot(mouseClickSound);
		}
		if(!string.IsNullOrEmpty(preference))
		{
			PlayerPrefs.SetString(preference, preferenceValue);
		}
	}
}
