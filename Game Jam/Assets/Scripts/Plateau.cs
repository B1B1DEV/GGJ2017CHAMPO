using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;



public class Plateau : MonoBehaviour {

	public GameObject tilePrefab;

	// Creation de la matrice 2D

	public int[,] tilesInt = new int[Constantes.LARGEUR_PLATEAU, Constantes.HAUTEUR_PLATEAU];

	public bool serialisation = false;
	public bool deserialisation = false;

	public int decal = 0; 



	void Start ()
	{

		//m_tile.GetComponent<MeshRenderer> ().;
		construction();
	}




	void Update (){
		if (serialisation) {
			serialisation = false;
			int[,] tab2 = tilesInt;
			serialiser(Constantes.FILENAME, tab2);
		}
		else if (deserialisation) {
			deserialisation = false;
			deserialiser(Constantes.FILENAME, 10);
		}
	}





	void miseAJour()
	{
		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j >0 ; j--) 
			{
				string name = j.ToString () + "-" + i.ToString ();
				//GameObject.Find(name).
			}
		}
	}





	// Sérialisation
	public void serialiser(string fichier, int[,] tab2)
	{
		FileStream fs = new FileStream (fichier, FileMode.Create);
		BinaryFormatter bf = new BinaryFormatter ();

		// Transformation de la map
		decal = 0;

		string layer = "" + Constantes.HAUTEUR_PLATEAU;

		//Regle probleme de taille
		if (Constantes.HAUTEUR_PLATEAU < 10)
			layer += " ";
		if (Constantes.HAUTEUR_PLATEAU > 99)
			decal++;
		
		layer += Constantes.LARGEUR_PLATEAU;

		if (Constantes.LARGEUR_PLATEAU < 10)
			layer += " ";
		if (Constantes.LARGEUR_PLATEAU > 99)
			decal++;



		// Conversion

		for(int j=0; j < Constantes.HAUTEUR_PLATEAU; j++)
		for (int i = 0; i < Constantes.LARGEUR_PLATEAU; i++) {
			layer += tab2[i, j].ToString();
		
		}

		bf.Serialize (fs, layer);
		fs.Close ();
		print ("Serialisation terminée");
	}

	// Désérialisation

	public void deserialiser(string fichier, int tab)
	{
		FileStream fs = new FileStream (fichier, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter ();
		object test = bf.Deserialize(fs);
		string st = test.ToString();
		print (st);

		// Transformation string en matrice

		Constantes.HAUTEUR_PLATEAU = int.Parse(st.Substring(0, 2));
		Constantes.LARGEUR_PLATEAU = int.Parse(st.Substring (2, 2));
		 
		int[,] matrice = new int[Constantes.LARGEUR_PLATEAU, Constantes.HAUTEUR_PLATEAU];


		// Remplissage
		int cpt = 4;

		for (int j = 0; j < Constantes.LARGEUR_PLATEAU; j++) {
			for (int i = 0; i < Constantes.HAUTEUR_PLATEAU; i++) {
				matrice[j, i] = int.Parse(st.Substring(cpt+decal, 1));
			}
		}

		fs.Close ();
		print ("Serialisation terminée");

		chargementMap (matrice);
	}



	public void construction(){

		// Creation du plateau

		float b = 0;


		float ru = .5f;
		float ri = Mathf.Sqrt(3)/2 * ru ;


		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j > 0 ; j--) 
			{
				// Création des cases
				
				GameObject tileGO = (GameObject)Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, transform);

				// On donne un nom à nos cases
				tileGO.name = j+"-"+i; 
				// On positionne chaque case
				//tile.transform.position = new Vector3(tile.transform.position.x + j*0.2f,tile.transform.position.y, tile.transform.position.z - j);
				tileGO.transform.position = new Vector3( (ru + ri/2) * j , 0, (j%2)*ri+  + i * ri * 2 );
				tileGO.transform.rotation = Quaternion.identity;

				Tile tile = tileGO.GetComponent<Tile> ();

				if(tilesInt[j, i] == Constantes.TILE_SOL)
					tile.type = Tile.Type.Sol;
				else if(tilesInt[j, i] == Constantes.TILE_NONE)
					tile.type = Tile.Type.None;
				else if(tilesInt[j, i] == Constantes.TILE_MUR)
					tile.type = Tile.Type.Mur;
				else if(tilesInt[j, i] == Constantes.TILE_PIEGE)
					tile.type = Tile.Type.Piege;
			}
		}
	}





	public void chargementMap(int[,] map)
	{
		// Destruction des tiles
		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j >0 ; j--) 
			{
				string name = j.ToString () + "-" + i.ToString ();
				DestroyObject(GameObject.Find(name));
			}
		}

		// Reconstruction

		tilesInt = map;

		construction ();


	}

}
