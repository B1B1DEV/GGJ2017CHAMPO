using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour {


	public enum Type : int { None, Sol, Mur, Piege, Mur1, Mur2, Monstre, Porte, PorteOuverte }

    //None when not occupied. Busy when Wall/Trap
    public enum State : int { None, Busy, Player, Monster }

	#region Delegates & events
	delegate void VoidDelegate();
	event VoidDelegate OnNextUpdateTick = null;
	#endregion


	#region attributes
	//public Queue<float> Intensitees;
	public float alpha;
    private GameManager gm;
    public Collider coll;


	public Histoire<State> stateHistory;
	public Histoire<float> lightingHistory;

    private List<Entite> entitesInTrigger;

	public State nextState;

	public MeshRenderer unlitTile;

	public bool lit; //true if currently lit
	private int ageShown; //what state should be displayed when tile is lit


    [SerializeField] private Type _type;
	[SerializeField] private Light _light;
	[SerializeField] private float _shownIntensity;
	[SerializeField] private GameObject _meshObject;
	[SerializeField] private Type _prevType = Type.Sol;
	#endregion

	#region Properties
	public State CurrentState { get { return stateHistory.ValeurActuelle; } }//history [GameManager.time % Constantes.MEMOIRE_ENTITEES]; } }

	public Type type 
	{ 
		get { return _type; }
		set 
		{
			if (value != _type) {
				if (_meshObject)
					DestroyImmediate (_meshObject);

				switch (value) 
				{
				case Type.None:
					_meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/None"), transform);
					break;
				case Type.Sol:
					_meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Sol"), transform);
					break;
				case Type.Mur:
					_meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Mur"), transform);
                    _meshObject.GetComponent<SphereCollider>().radius = 1.5f;
					break;
				case Type.Piege:
					_meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Piege"), transform);
					break;

                case Type.Mur1:
                    _meshObject = Instantiate(ResourcesLoader.Load<GameObject>("Tiles/Mur_cubes"), transform);
                    break;
                case Type.Mur2:
                    _meshObject = Instantiate(ResourcesLoader.Load<GameObject>("Tiles/Mur_hexagones"), transform);
                    break;
				case Type.Monstre:
					_prevType = _type;
					_meshObject = Instantiate(ResourcesLoader.Load<GameObject>("Tiles/Monstre"), transform);
					OnNextUpdateTick += () => type = _prevType;
                    break;
                case Type.Porte:
                    _meshObject = Instantiate(ResourcesLoader.Load<GameObject>("Tiles/Porte"), transform);
                    break;
				case Type.PorteOuverte:
					_meshObject = Instantiate(ResourcesLoader.Load<GameObject>("Tiles/PorteOuverte"), transform);
					break;
                }

				_type = value;

				_meshObject.transform.localPosition = Vector3.zero;
				_meshObject.transform.localRotation = Quaternion.identity;
				_meshObject.transform.localScale = Vector3.one;
			}
		}
	}
    #endregion

    #region Unity methods

    private void Awake()
    {
        gm = GameManager.Instance;
    }

    // Use this for initialization
    void Start ()
    {
		nextState = State.None;

		stateHistory = new Histoire<State> (GetNextState);

        entitesInTrigger = new List<Entite>();

        this.lit = false;
        //this.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

	State GetNextState()
	{
		State ret = nextState;
		switch (type) {
		case Type.None:
		case Type.Mur:
		case Type.Mur1:
		case Type.Mur2:
		case Type.Porte:
			nextState = State.Busy;
			break;
		case Type.Piege:
			nextState = State.None;
			break;
		case Type.PorteOuverte:
		case Type.Sol:
		case Type.Monstre:
			// In this case, state is changed by player/monster. 
			break;
		}
		return ret;
	}


	// Update is called once per frame
	void Update () {
        if (lit)
        {
			_meshObject.GetComponentInChildren<MeshRenderer>().enabled = true;
			unlitTile.enabled = false;
        } else
        {
			_meshObject.GetComponentInChildren<MeshRenderer>().enabled = false;
			unlitTile.enabled = true;
        }
        
    }

    public void UpdateTick(int timeStamp)
    {
		if (OnNextUpdateTick != null) {
			OnNextUpdateTick.Invoke ();
			OnNextUpdateTick = null;
		}
		// This should not be handled by histoire directly
		/*
		this.history [timeStamp % Constantes.MEMOIRE_ENTITEES] = nextState;
		nextState = State.None;
		*/
    }

    void OnTriggerEnter(Collider other)
    {
        Firefly ffCol = other.gameObject.GetComponent<Firefly>();

        if (ffCol != null)
		{
			Vector3 thisPos = transform.position - transform.position.y * Vector3.up;
			Vector3 otherPos = other.transform.position - other.transform.position.y * Vector3.up;
			// la condition est nulle.
			if (this.type == Type.Mur && Vector3.Distance(thisPos, otherPos) < Constantes.INNER_RADIUS)
            {
                Destroy(ffCol.gameObject);
                gm.fireflies.Remove(ffCol);
            }
            else
            {
                int age = other.GetComponent<Firefly>().age;
                this.lit = true;
                this.ageShown = GameManager.time - age;
            }

            // Register
            entitesInTrigger.Add(ffCol);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Firefly ffCol = other.gameObject.GetComponent<Firefly>();

        if (ffCol != null)
        {
            this.lit = false;

            // unregister
            entitesInTrigger.Remove(ffCol);
        }
    }


    #endregion
}
