using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plateau : MonoBehaviour {

	public GameObject tilePrefab;

	public int[,] tiles = new int[Constantes.LARGEUR_PLATEAU, Constantes.HAUTEUR_PLATEAU];

	void Start ()
	{

		//m_tile.GetComponent<MeshRenderer> ().;

		// Creation de la matrice 2D



		// print (matrice.GetLength(0)); RECUP LARGEUR
		// print (matrice.GetUpperBound(0)); RECUP HAUTEUR

		for (int i = 0; i < tiles.GetUpperBound(0); i++) {
			for (int j = 0; j < tiles.GetLength(0); j++)
			{
				print(tiles[i, j]);
			}
		}

		// Creation du plateau

		float b = 0;


		float ru = .5f;
		float ri = Mathf.Sqrt(3)/2 * ru ;


		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j > 0 ; j--) 
			{
				// Création des cases
				//GameObject tile = (GameObject)Instantiate(m_tile, new Vector3(j,0,i), Quaternion.Euler(new Vector3(0,0,0)));
				GameObject tile = (GameObject)Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, transform);
				// On donne un nom à nos cases
				tile.name = j+"-"+i; 
				// On positionne chaque case
				//tile.transform.position = new Vector3(tile.transform.position.x + j*0.2f,tile.transform.position.y, tile.transform.position.z - j);
				tile.transform.position = new Vector3( (ru + ri/2) * j , 0, (j%2)*ri+  + i * ri * 2 );
				tile.transform.rotation = Quaternion.identity;
			}
			b -= 1.1f;
		}
		// this.transform.position = new Vector3(9,2,5);
		// Quaternion rotation = Quaternion.Euler(new Vector3(90, 301, 0));
		// this.transform.rotation = rotation;
	}
}
