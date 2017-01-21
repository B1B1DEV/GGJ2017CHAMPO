using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public enum Type { None, Sol, Mur, Piege }

	public Type type;
	public Queue<float> Intensitees;
	public float absorbance;
	public float alpha;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateType()
	{
		
	}
}
