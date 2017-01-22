using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstre : Entite {

	/// <summary>
	/// Intensité de la dernière source vue
	/// </summary>
	private float bestIntensity;

	/// <summary>
	/// Position de la dernière source vue
	/// </summary>
	private Coord bestSource;

	protected override void Start()
	{
		GameManager.Instance.monstres.Add (this);
	}

	public override void Move()
	{
		Coord source;
		float intensity;

		// si l'avatar est visible
		if (GameManager.Instance.avatar.VisibilityFrom (PositionActuelle, out source, out intensity))
		{
			// on oublie la dernière source vue
			bestIntensity = intensity;
			bestSource = source;
		} // sinon, on regarde s'il y a de la lumière
		else
		{
			foreach (Firefly luciole in GameManager.Instance.fireflies)
			{
				// si l'intensité est supérieure, on change d'objectif
				if (luciole.VisibilityFrom (PositionActuelle, out source, out intensity) && intensity > bestIntensity)
				{
					bestSource = source;
					bestIntensity = intensity;
				}
			}
		}
		if (bestIntensity > 0 && bestSource != PositionActuelle)
		{
			// direction dans laquelle le monstre se déplace
			Vector2 vecteurPosition = PositionActuelle.ToVector2 ();
			Vector2 direction = bestSource.ToVector2 () - vecteurPosition;
			direction.Normalize();
			Coord newPosition = new Coord(vecteurPosition + 2 * Constantes.INNER_RADIUS * direction);

			Tile tile = GameManager.Instance.tiles [newPosition.x, newPosition.y];

			if (tile.CurrentState == Tile.State.None && tile.nextState == Tile.State.None)
			{
				PositionActuelle = newPosition;
				tile.nextState = Tile.State.Monster;
			}
		}
		// déclin de l'intensité dans la mémoire du monstre
		intensity -= Constantes.INTENSITY_DECAY;
		if (bestIntensity < 0)
		{
			bestIntensity = 0f;
		}
	}
}
