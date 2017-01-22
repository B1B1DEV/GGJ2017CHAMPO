using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public static int time = 0;

	public GameObject directLight;

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

	public event Firefly.DestroyFireflyAction DestroyFireflyEvent = null;

	public enum Status {Normal, Paused, GameOver, Victory};
	public Status status { get; private set; }

	public AudioSource[] sounds;
	public AudioSource noise1;

	void Awake()
	{
		_instance = this;
		status = Status.Normal;
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("FourSecondsUpdateLoop", 0, Constantes.TURN_DURATION);
        // tiles = this.plateau.GetComponentsInChildren<Tile>();
		sounds = GetComponents<AudioSource> ();
		noise1 = sounds [0];
    } 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			switch (status)
			{
			case Status.Normal:
				Time.timeScale = 0f;
				status = Status.Paused;
				break;
			case Status.Paused:
				Time.timeScale = 1f;
				status = Status.Normal;
				break;
			case Status.GameOver:
			case Status.Victory:
				Application.Quit ();
				break;
			}
		} else if (status == Status.Paused && Input.GetKeyDown (KeyCode.X))
		{
			Application.Quit ();
		}
	}

    void FourSecondsUpdateLoop()
    {
//        Debug.Log("Moving entities acknowledge update now");
        //Fireflies get one step older
        foreach (Firefly f in fireflies)
        {
            f.age++;
			f.UpdateEntite ();
        }

		if (DestroyFireflyEvent != null) {
			DestroyFireflyEvent.Invoke ();
			DestroyFireflyEvent = null;
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

	public void Lose()
	{
		OnUpdateHistoire += delegate() {

			directLight.SetActive(true);
			status = Status.GameOver;
			noise1.Play();
			Time.timeScale = 0f;

		};
	}

	void OnGUI()
	{
		if (status == Status.GameOver)
		{
			GUIStyle style = new GUIStyle ();
			style.fontSize = 60;
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Game Over!", style);
		} else if (status == Status.Victory)
		{
			GUIStyle style = new GUIStyle ();
			style.fontSize = 60;
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Victory!", style);
		} else if (status == Status.Paused)
		{
			GUIStyle style = new GUIStyle ();
			style.fontSize = 60;
			style.normal.textColor = Color.white;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Type 'X' to exit", style);
		}
	}

	public void Win()
	{
		OnUpdateHistoire += delegate() {

			directLight.SetActive(true);
			status = Status.Victory;
			Time.timeScale = 0f;

		};
	}
}
