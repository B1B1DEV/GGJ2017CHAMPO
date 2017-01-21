using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constantes {
	public static int LARGEUR_PLATEAU = 100;
	public static int HAUTEUR_PLATEAU = 100;
	public static string FILENAME = "MAP.txt";
    public static int N_LUCIOLES = 6;
	public static float OUTER_RADIUS = 0.5f;
	public static float INNER_RADIUS = Mathf.Sqrt(3)/2 * OUTER_RADIUS;
	public static int MEMOIRE_ENTITEES = 10;
	public static int TILE_SOL = 0;
	public static int TILE_NONE = 1;
	public static int TILE_MUR = 2;
	public static int TILE_PIEGE = 3;

    public static float DELTA_T_LIGHT = 3f;

}
