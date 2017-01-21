using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

	public static GameManager _instance;
	public static GameManager Instance 
	{ 
		get {return _instance?_instance:_instance=FindObjectOfType<GameManager>(); }
	}

	public Plateau plateau;
	public Entite avatar;
	public List<Firefly> fireflies;

	// Use this for initialization
	void Start () {
		
	} 
	
	// Update is called once per frame
	void Update () {
		
	}
}
