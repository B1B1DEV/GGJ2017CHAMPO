using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entite : MonoBehaviour {

	public Coord PositionActuelle { get; set; }

	private Queue<Coord> _positions;
	private Queue<Coord> positions {
		get
		{
			if (_positions == null)
			{
				_positions = new Queue<Coord> (Constantes.MEMOIRE_ENTITEES);
				_positions.Enqueue (PositionActuelle);
			}
			return _positions;
		}
	}

	public virtual void Move() {}

	public void UpdateEntite ()
	{
		Move ();
		if ( !(positions.Count < Constantes.MEMOIRE_ENTITEES ) )
			positions.Dequeue ();
		
		positions.Enqueue (PositionActuelle);
	}

	
	public bool VisibilityFrom ( Coord fromPos, out Dictionary<Coord, float> intensities )
	{
		foreach (Coord pos in positions) {
			
		}

		intensities = new Dictionary<Coord, float> ();
		return false;	
	}

	

	public bool VisibilityFrom ( Coord fromPos, out Coord maxIntensitePos, out float intensity )
	{
		maxIntensitePos = Vector2.zero;
		return false;
	}
	

}
