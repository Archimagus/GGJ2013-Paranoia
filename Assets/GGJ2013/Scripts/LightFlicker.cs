using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
	public float minIntensity;
	public float maxIntensity;
	public float flickerRate;

	float baseRange;
	float targetIntensity;
	float flickerVelocity;
	float changeTime = 0;
	// Use this for initialization
	void Start () 
	{
		baseRange = GetComponent<Light>().range;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - changeTime > flickerRate)
		{
			targetIntensity = Random.Range(minIntensity, maxIntensity);
			changeTime = Time.time;
		}
		GetComponent<Light>().intensity = Mathf.SmoothDamp(GetComponent<Light>().intensity, targetIntensity, ref flickerVelocity, flickerRate);
		GetComponent<Light>().range = baseRange * GetComponent<Light>().intensity;
	}
}
