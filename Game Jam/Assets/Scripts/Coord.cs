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
		x = (int) (Mathf.Round (2 * vector.x / (3 * Constantes.OUTER_RADIUS)));
		y = (int) (Mathf.Round((vector.y / Constantes.INNER_RADIUS - x % 2)/2));
	}

	public Coord(Vector3 vector)
	{
		x = (int) (Mathf.Round (2 * vector.x / (3 * Constantes.OUTER_RADIUS)));
		y = (int) (Mathf.Round((vector.z / Constantes.INNER_RADIUS - x % 2)/2));
	}

	public Vector2 ToVector2()
	{
		return new Vector3 (1.5f * Constantes.OUTER_RADIUS * x, (x % 2 + 2 * y) * Constantes.INNER_RADIUS);
	}

	public Vector3 ToVector3()
	{
		return new Vector3 (1.5f * Constantes.OUTER_RADIUS * x, 0f, (x % 2 + 2 * y) * Constantes.INNER_RADIUS);
	}

	public int DistanceTo(Coord other)
	{
		return Mathf.RoundToInt((this.ToVector2 () - other.ToVector2 ()).SqrMagnitude () / Constantes.INNER_RADIUS);
	}
}