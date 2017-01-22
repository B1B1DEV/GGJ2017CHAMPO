using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class Entite : MonoBehaviour {


	private Histoire<Coord> positions;
	public Coord PositionActuelle 
	{ 
		get { return positions.ValeurActuelle; } 
		set { transform.position = value.ToVector3 (); }
	}


	protected virtual void Start()
	{
		positions = new Histoire<Coord> (() => new Coord (transform.position));
	}

	abstract public void Move ();

	public void UpdateEntite ()
	{
		Move ();
	}

	
	public bool VisibilityFrom ( Coord fromPos, out Dictionary<Coord, float> intensities )
	{
		
		intensities = new Dictionary<Coord, float> ();
		return false;	
	}

	

	public bool VisibilityFrom ( Coord fromPos, out Coord maxIntensitePos, out float intensity )
	{
		List<float> intensities = new List<float> ();
		List<Coord> coords = new List<Coord> ();

		//float maxIntensity = Mathf.NegativeInfinity;

		Coord c;
		for (int nbIterationsDansLePasse = 0; positions.ValeurPassee(nbIterationsDansLePasse, out c) ; nbIterationsDansLePasse++) {
			if (c.DistanceTo (fromPos) == nbIterationsDansLePasse) {
				Vector3 depart = fromPos.ToVector3 () + Vector3.up;
				Vector3 arrivee = c.ToVector3 () + Vector3.up;
				Ray ray = new Ray (depart, arrivee - depart); // + Vector3.up est la pour raycast 1m au dessus du sol. 
				RaycastHit hit;

				if (!Physics.Raycast (ray, out hit, Vector3.Distance (depart, arrivee), LayerMask.GetMask ("Environnement"))) 
				{
					intensities.Add (Random.value);
					coords.Add (c);
				}
			}
		}
	

		if (intensities.Count != 0) {
			float maxIntensity = intensities.Max ();
			intensity = maxIntensity;
			int indexMax = intensities.FindIndex (e => e == maxIntensity);
			maxIntensitePos = coords [indexMax];
			
			return true;
		} else {
			intensity = 0;
			maxIntensitePos = new Coord (0, 0);

			return false;
		}
	}
	

}
