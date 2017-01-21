using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau : MonoBehaviour {

	public GameObject m_tile;
	public int m_hauteur = 10;
	public int m_largeur = 10;


	void Start ()
	{

		//m_tile.GetComponent<MeshRenderer> ().;

		// Creation du plateau

		for (int y = 0; y < m_hauteur; y++) 
		{
			for (int x = 0; x < m_largeur; x++) 
			{
				// Création des cases
				GameObject tile = (GameObject)Instantiate(m_tile, new Vector3(x,0,y), Quaternion.Euler(new Vector3(0,0,0)));
				// On donne un nom à nos cases
				tile.name = x+"-"+y; 
				// On positionne chaque case
				tile.transform.position = new Vector3(tile.transform.position.x + 0.2f * x,tile.transform.position.y, tile.transform.position.z - 1.0f * x);
				tile.transform.rotation = Quaternion.Euler (0, 0, 0);

			}
		}
		this.transform.position = new Vector3(9,12,5);
		Quaternion rotation = Quaternion.Euler(new Vector3(90, 301, 0));
		this.transform.rotation = rotation;
	}
}
