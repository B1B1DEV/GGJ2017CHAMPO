using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static int time = 0;

	public Pulse pulse;

	public Tile[,] tiles = new Tile[Constantes.LARGEUR_PLATEAU,Constantes.HAUTEUR_PLATEAU];

    private static GameManager _instance;

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


	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
        InvokeRepeating("FourSecondsUpdateLoop", 0, 2.0f);
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

		Coord avatarPos = avatar.PositionActuelle;
		Dictionary<Coord, float> seenMonsters;
		foreach (Monstre m in monstres) {
			m.VisibilityFrom (avatarPos, out seenMonsters);
			foreach (KeyValuePair<Coord, float> pair in seenMonsters) {
				tiles [pair.Key.x, pair.Key.y].type = Tile.Type.Monstre;
			}
		}

        time++;

		OnUpdateHistoire.Invoke ();

    }
}
