using UnityEngine;

/// Coordonnées dans la grille
public class Coord
{
	/// <summary>
	/// Position en largeur
	/// </summary>
	public int x;
	/// <summary>
	/// Position en hauteur
	/// </summary>
	public int y;

	public Coord(int x_, int y_)
	{
		x = x_;
		y = y_;
	}

	public Coord(Vector2 vector)
	{
		x = (int) (Mathf.Round (vector.x / (Constantes.INNER_RADIUS + Constantes.OUTER_RADIUS / 2)));
		y = (int) (Mathf.Round((vector.y / Constantes.OUTER_RADIUS - x % 2)/2));
	}

	public Vector2 ToVector()
	{
		return new Vector2 ((Constantes.INNER_RADIUS + Constantes.OUTER_RADIUS / 2) * x, (x % 2 + 2 * y) * Constantes.OUTER_RADIUS);
	}
}