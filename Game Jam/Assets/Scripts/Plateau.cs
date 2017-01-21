using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour {

	public GameObject m_tile;


	void Start ()
	{

		//m_tile.GetComponent<MeshRenderer> ().;

		// Creation de la matrice 2D

		int[,] matrice = new int[Constantes.LARGEUR_PLATEAU, Constantes.HAUTEUR_PLATEAU];

		// print (matrice.GetLength(0)); RECUP LARGEUR
		// print (matrice.GetUpperBound(0)); RECUP HAUTEUR

		for (int i = 0; i < matrice.GetUpperBound(0); i++) {
			for (int j = 0; j < matrice.GetLength(0); j++)
			{
				print(matrice[i, j]);
			}
		}

		// Creation du plateau

		float b = 0;

		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j > 0 ; j--) 
			{
				// Création des cases
				//GameObject tile = (GameObject)Instantiate(m_tile, new Vector3(j,0,i), Quaternion.Euler(new Vector3(0,0,0)));
				GameObject tile = (GameObject)Instantiate(m_tile, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
				// On donne un nom à nos cases
				tile.name = j+"-"+i; 
				// On positionne chaque case
				//tile.transform.position = new Vector3(tile.transform.position.x + j*0.2f,tile.transform.position.y, tile.transform.position.z - j);
				tile.transform.position = new Vector3(tile.transform.position.x + j*1.0f,tile.transform.position.y, tile.transform.position.z-b + (j)*0.55f);
				tile.transform.rotation = Quaternion.Euler (0, 0, 0);
			}
			b -= 1.1f;
		}
		this.transform.position = new Vector3(9,12,5);
		Quaternion rotation = Quaternion.Euler(new Vector3(90, 301, 0));
		this.transform.rotation = rotation;
	}
}
