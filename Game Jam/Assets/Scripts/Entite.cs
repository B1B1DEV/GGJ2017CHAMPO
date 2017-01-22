using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

abstract public class Entite : MonoBehaviour {


	protected Histoire<Coord> positions;
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

		bool ret = false;

		intensities = new Dictionary<Coord, float> ();

		Coord c= PositionActuelle;
		for (int nbIterationsDansLePasse = 0; positions.ValeurPassee (nbIterationsDansLePasse, out c); nbIterationsDansLePasse++) {
			if (GameManager.Instance.tiles[c.x,c.y].lit && c.DistanceTo (fromPos) == nbIterationsDansLePasse) {
				Vector3 depart = fromPos.ToVector3 () + Vector3.up;
				Vector3 arrivee = c.ToVector3 () + Vector3.up;
				Ray ray = new Ray (depart, arrivee - depart); // + Vector3.up est la pour raycast 1m au dessus du sol. 
				RaycastHit hit;

				if (!Physics.Raycast (ray, out hit, Vector3.Distance (depart, arrivee), LayerMask.GetMask ("Mur"),QueryTriggerInteraction.Collide)) {
					intensities.Add (c, GameManager.Instance.tiles[c.x,c.y].lightingHistory.ValeurActuelle);
					ret = true;
				}
			}
		}


		return ret;	
	}

	

	public bool VisibilityFrom ( Coord fromPos, out Coord maxIntensitePos, out float intensity )
	{

		Coord c = PositionActuelle;
		for (int nbIterationsDansLePasse = 0; positions.ValeurPassee (nbIterationsDansLePasse, out c); nbIterationsDansLePasse++) {
			if (c.x >= 0 && c.x < Constantes.LARGEUR_PLATEAU && c.y >= 0 && c.y < Constantes.HAUTEUR_PLATEAU && GameManager.Instance.tiles[c.x,c.y].lit && c.DistanceTo (fromPos) == nbIterationsDansLePasse) {
				Vector3 depart = fromPos.ToVector3 () + Vector3.up;
				Vector3 arrivee = c.ToVector3 () + Vector3.up;
				Ray ray = new Ray (depart, arrivee - depart); // + Vector3.up est la pour raycast 1m au dessus du sol. 
				RaycastHit hit;

				if (!Physics.Raycast (ray, out hit, Vector3.Distance (depart, arrivee), LayerMask.GetMask ("Environnement"),QueryTriggerInteraction.Collide)) {
					maxIntensitePos = c;
					intensity = GameManager.Instance.tiles[c.x,c.y].lightingHistory.ValeurActuelle;
					return true;
				}
			}
		}

		maxIntensitePos = new Coord (0, 0);
		intensity = 0;
		return false;	
	}

	

}
