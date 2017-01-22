﻿using System.Collections;
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
	private Avatar _avatar;
	public Avatar avatar
	{ 
		get {return _avatar?_avatar:_avatar=FindObjectOfType<Avatar>(); }
	}
	public List<Firefly> fireflies;
	public List<Monstre> monstres;

	public event UpdateHistoireAction OnUpdateHistoire = null;


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
			f.UpdateEntite ();
        }

		foreach (Monstre m in monstres) {
			m.UpdateEntite ();
		}

		avatar.UpdateEntite ();

        //Tiles record their state
        foreach (Tile t in tiles) {
            t.UpdateTick(time);
        }

        time++;

		OnUpdateHistoire.Invoke ();
    }
}
