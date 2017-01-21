using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour {

	public enum Type { None, Sol, Mur, Piege }

	[SerializeField]
	private Type _type;
	public Type type 
	{ 
		get { return _type; }
		set 
		{
			if (value != _type) {
				if (meshObject)
					Destroy (meshObject);

				_type = value;

				switch (value) 
				{
					case Type.None:
						meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/None"), transform);
						break;
					case Type.Sol:
						meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Sol"), transform);
						break;
					case Type.Mur:
						meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Mur"), transform);
						break;
					case Type.Piege:
						meshObject = Instantiate (ResourcesLoader.Load<GameObject> ("Tiles/Piege"), transform);
						break;
				}
					
				meshObject.transform.localPosition = Vector3.zero;
				meshObject.transform.localRotation = Quaternion.identity;
				meshObject.transform.localScale = Vector3.one;
			}
		}
	}

	public Queue<float> Intensitees;
	public float absorbance;
	public float alpha;

	[SerializeField]
	private GameObject meshObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
