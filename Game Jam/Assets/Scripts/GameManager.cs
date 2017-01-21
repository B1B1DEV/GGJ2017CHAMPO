using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static int time = 0;

	public Tile[,] tiles = new Tile[Constantes.LARGEUR_PLATEAU,Constantes.HAUTEUR_PLATEAU];

    public static GameManager _instance;
	public static GameManager Instance 
	{ 
		get {return _instance?_instance:_instance=FindObjectOfType<GameManager>(); }
	}

	public Plateau plateau;
	public Avatar avatar;
	public List<Firefly> fireflies;




	// Use this for initialization
	void Start () {
        //InvokeRepeating("FourSecondsUpdateLoop", 0, 2.0f);
        // tiles = this.plateau.GetComponentsInChildren<Tile>();
    } 
	
	// Update is called once per frame
	void Update () {
		
	}


    void FourSecondsUpdateLoop()
    {
        Debug.Log("Moving entities acknowledge update now");
        //Fireflies get one step older
        foreach (Firefly f in fireflies)
        {
            f.age++;
        }
        //Tiles record their state
        foreach (Tile t in tiles) {
            t.SendMessage("UpdateTick", time);
        }
        time++;
    }
}
