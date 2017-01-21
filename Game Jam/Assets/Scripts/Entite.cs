using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entite : MonoBehaviour {

	public Coord PositionActuelle { get; set; }

	private SortedDictionary<int, Coord> _positions;
	private SortedDictionary<int, Coord> positions {
		get
		{
			if (_positions == null)
			{
				_positions = new SortedDictionary<int, Coord> ();
				_positions.Add (GameManager.time, PositionActuelle);
			}
			return _positions;
		}
	}

	public virtual void Move() {}

	public void UpdateEntite ()
	{
		Move ();
		while ( positions.Count >= Constantes.MEMOIRE_ENTITEES )
			positions.Remove(positions.First().Key);
		
		positions.Add (GameManager.time, PositionActuelle);
	}

	
	public bool VisibilityFrom ( Coord fromPos, out Dictionary<Coord, float> intensities )
	{
		
		intensities = new Dictionary<Coord, float> ();
		return false;	
	}

	

	public bool VisibilityFrom ( Coord fromPos, out Coord maxIntensitePos, out float intensity )
	{
		foreach (KeyValuePair<int, Coord> pair in positions) {
			int distance = Mathf.RoundToInt((pair.Value.ToVector2 () - fromPos.ToVector2 ()).SqrMagnitude () / Constantes.INNER_RADIUS);
		}

		maxIntensitePos = new Coord(0,0);
		intensity = 0f;
		return false;
	}
	

}
