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

	}

	public Vector2 ToVector()
	{
		return new Vector2 ((Constantes.INNER_RADIUS + Constantes.OUTER_RADIUS / 2) * x, (x % 2 + 2 * y) * Constantes.OUTER_RADIUS);
	}
}