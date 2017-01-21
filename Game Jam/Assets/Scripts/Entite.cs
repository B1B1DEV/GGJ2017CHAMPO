using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entite : MonoBehaviour {

	/// <summary>
	/// Gets or sets the position actuelle.
	/// SET IT ONCE PER TURN.
	/// </summary>
	/// <value>The position actuelle SET IT ONCE PER TURN..</value>
	public Vector2 PositionActuelle { get; set; }
	public bool VisibilityFrom ( Vector2 fromPos, out Dictionary<Vector2, float> intensities )
	{
		intensities = new Dictionary<Vector2, float> ();
		return false;	
	}
}
