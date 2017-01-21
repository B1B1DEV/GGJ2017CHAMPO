using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour {

	public enum Type : int { None, Sol, Mur, Piege }

    //None when not occupied. Busy when Wall/Trap
    public enum State : int { None, Busy, Player, Monster }

	#region attributes
	//public Queue<float> Intensitees;
	public float absorbance;
	public float alpha;
    private GameManager gm;
    //public Collider coll;

    [SerializeField] private Type _type;
	[SerializeField] private Light _light;
	[SerializeField] private float _shownIntensity;
	[SerializeField] private GameObject _meshObject;
	#endregion

	#region Properties
	public Type type 
	{ 
		get { return _type; }
		set 
		{
			if (value != _type) {
				if (_meshObject)
					DestroyImmediate (_meshObject);

				_type = value;

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
					break;
				case Type.Piege:
					_meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Piege"), transform);
					break;
				}

				_meshObject.transform.localPosition = Vector3.zero;
				_meshObject.transform.localRotation = Quaternion.identity;
				_meshObject.transform.localScale = Vector3.one;
			}
		}
	}

    public State[] history;

    public bool lit; //true if currently lit
    private int ageShown; //what state should be displayed when tile is lit


    #endregion

    #region Unity methods

    private void Awake()
    {
        //this.GetComponent<Light>().intensity = intensity;
        gm = GameManager.Instance;
    }

    // Use this for initialization
    void Start () {
        this.history = new State[Constantes.MEMOIRE_ENTITEES]; //N last states;
        this.history[0] = State.None;
        this.lit = false;
        //this.GetComponentInChildren<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
/*        if (lit)
        {
            this.GetComponentInChildren<MeshRenderer>().enabled = true;
        } else
        {
            this.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
 */   }

    void UpdateTick(int timeStamp)
    {
        this.history[timeStamp % 100] = State.None; //Replace by actual
    }

    void OnTriggerEnter(Collider other)
    {
        Firefly ffCol = other.gameObject.GetComponent<Firefly>();

        if (ffCol != null)
        {
            if (this.type == Type.Mur)
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
        }
    }

    void OnTriggerExit(Collider other)
    {
        Firefly ffCol = other.gameObject.GetComponent<Firefly>();

        if (ffCol != null)
        {
            this.lit = false;
        }
    }


    #endregion
}
