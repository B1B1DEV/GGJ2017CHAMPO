using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour {

	public enum Type : int { None, Sol, Mur, Piege }

	#region attributes
	//public Queue<float> Intensitees;
	public float absorbance;
	public float alpha;
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

	// lumiere est en français car "light" est obsolete....... ><
	public Light lumiere 
	{
		get { return _light ? _light : _light = GetComponentInChildren<Light> (); }
	}

	public float shownIntensity
	{
		get { return _shownIntensity; }
		set 
		{
			_shownIntensity = Mathf.Clamp01 (value);
			if (_shownIntensity == 0f) 
				lumiere.enabled = false;
			else
			{
				lumiere.enabled = true;
				lumiere.intensity = _shownIntensity;
			}
		}
	}

    #endregion

    #region Unity methods
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion
}
