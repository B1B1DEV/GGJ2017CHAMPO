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
	
	public int hole = 20;
	public int colonneUnique = 10;



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

		/// MUR DE FOND ==================================================================================
		
		
		for (int i = 0; i < Constantes.HAUTEUR_PLATEAU ; i++) 
		{
			for (int j = 0; j < Constantes.LARGEUR_PLATEAU; j++) 
			{
				// Création des cases
				
				GameObject tileGO = (GameObject)Instantiate(tilePrefab, Vector3.zero, Quaternion.identity, transform);

				// On donne un nom à nos cases
				tileGO.name = j+"-"+i; 
				// On positionne chaque case
				//tile.transform.position = new Vector3(tile.transform.position.x + j*0.2f,tile.transform.position.y, tile.transform.position.z - j);
				tileGO.transform.position = new Coord(j , i).ToVector3();
				tileGO.transform.rotation = Quaternion.identity;

				Tile tile = tileGO.GetComponent<Tile> ();

				tile.type = Tile.Type.Mur;

				GameManager.Instance.tiles [j, i] = tile;
			}
		}
		
		
		

		percer();
		colonnes();
		pieges();
		salleCentral();
		cheminFinal();
		
	}
	
	
	//// PERCER ==============================================================
	
	public void percer()
	{
		int taille = Constantes.HAUTEUR_PLATEAU * Constantes.LARGEUR_PLATEAU;
		int hole = (taille*60)/100;
		
		for(int i=0; i< hole; i++)
		{
			int x=Random.Range(0, Constantes.LARGEUR_PLATEAU);
			int y=Random.Range(0, Constantes.HAUTEUR_PLATEAU);
			string n = x.ToString () + "-" + y.ToString ();
			GameObject tileGO = GameObject.Find(n);
			Tile tiler = tileGO.GetComponent<Tile> ();
			
			if(x != 0 && x != Constantes.LARGEUR_PLATEAU-1 && y != 0 && y!= Constantes.HAUTEUR_PLATEAU-1)
				tiler.type = Tile.Type.Sol;
		}
	}
	
	
	
		/// On suprime les colones isolé ========================================================================
		
	void colonnes()
	{
		for (int i = 1; i < Constantes.HAUTEUR_PLATEAU-1 ; i++) 
		{
			for (int j = 1; j < Constantes.LARGEUR_PLATEAU-1; j++) 
			{

				string name0 = j.ToString () + "-" + i.ToString ();
				string name2 = (j-1).ToString () + "-" + (i-1).ToString ();
				string name3 = (j+1).ToString () + "-" + (i).ToString ();
				string name4 = j.ToString () + "-" + (i+1).ToString ();
				string name5 = (j+1).ToString () + "-" + (i+1).ToString ();
				
				GameObject tileGO = GameObject.Find(name0);
				GameObject tileGO2 = GameObject.Find(name2);
				GameObject tileGO3 = GameObject.Find(name3);
				GameObject tileGO4 = GameObject.Find(name4);
				GameObject tileGO5 = GameObject.Find(name5);
				
				int rand = Random.Range(0, 100);
				int var = 0;
				
				Tile tile = tileGO.GetComponent<Tile> ();
				Tile tile2 = tileGO2.GetComponent<Tile> ();
				Tile tile3 = tileGO3.GetComponent<Tile> ();
				Tile tile4 = tileGO4.GetComponent<Tile> ();
				Tile tile5 = tileGO5.GetComponent<Tile> ();
				
				if(tile2.type  == Tile.Type.Sol)
					var+=colonneUnique;
				if(tile3.type  == Tile.Type.Sol)
					var+=colonneUnique;
				if(tile4.type  == Tile.Type.Sol)
					var+=colonneUnique;
				if(tile5.type  == Tile.Type.Sol)
					var+=colonneUnique;
				
				if(rand <= colonneUnique )
				{
					if(j != 0 && j != Constantes.LARGEUR_PLATEAU-1 && i != 0 && i!= Constantes.HAUTEUR_PLATEAU-1)
						tile.type = Tile.Type.Sol;
				}
					

			}
		}
		
	}
	
	
	public void salleCentral()
	{ 
		/// CREATION DE LA SALLE ======================================================
		int xMilieux = Constantes.HAUTEUR_PLATEAU/2;
		int yMilieux = Constantes.LARGEUR_PLATEAU/2;
		
		string name0 = "";

		
		for(int posX = xMilieux-Constantes.RADIUS_SPAWN; posX<xMilieux+Constantes.RADIUS_SPAWN;posX++)
			for(int posY = yMilieux-Constantes.RADIUS_SPAWN; posY<xMilieux+Constantes.RADIUS_SPAWN; posY++)
			{
				name0 = posY.ToString () + "-" + posX.ToString ();
				GameObject tileGO = GameObject.Find(name0);
				Tile tiler = tileGO.GetComponent<Tile> ();
				tiler.type = Tile.Type.Sol;
			}
		
		
	}
	
	/// Gestion des piges ===========================================================
	public void pieges()
	{
		for (int i = 0; i < Constantes.HAUTEUR_PLATEAU ; i++) 
		{
			for (int j = 0; j < Constantes.LARGEUR_PLATEAU; j++) 
			{

				string name0 = j.ToString () + "-" + i.ToString ();
				GameObject tileGO = GameObject.Find(name0);
				Tile tile = tileGO.GetComponent<Tile> ();
				
				if(tile.type  == Tile.Type.Sol)
				{
					int rand = Random.Range(0, 100);
					if(rand <= Constantes.TRAPS)
						tile.type = Tile.Type.Piege;
				}
				

			}
		}
	}

	
	/// Chemin final ===========================================================
	
	public void cheminFinal()
	{
		int randx;
		int randy;
		int posX = Constantes.HAUTEUR_PLATEAU/2;
		int posY = Constantes.LARGEUR_PLATEAU/2;
		
		while(posX != 0 && posX != Constantes.LARGEUR_PLATEAU-1 && posY != 0 && posY != Constantes.HAUTEUR_PLATEAU-1)
		{
			randx = Random.Range(-1,2);
			randy = Random.Range(-1,2);
			posX += randx;
			posY += randy;
			
			Tile tile = GameManager.Instance.tiles[posX,posY];
			
			tile.type = Tile.Type.Sol;
		}
		// Agrandissement de la sortie
		
		
	}
	
	
	

	public void chargementMap(int[,] map)
	{
		// Destruction des tiles
		for (int i = Constantes.HAUTEUR_PLATEAU; i > 0 ; i--) 
		{
			for (int j = Constantes.LARGEUR_PLATEAU; j >0 ; j--) 
			{
				string name0 = j.ToString () + "-" + i.ToString ();
				DestroyObject(GameObject.Find(name0));
				
			}
		}

		// Reconstruction
		
		

		tilesInt = map;
		
		for (int i = 0; i < Constantes.HAUTEUR_PLATEAU ; i++) 
		{
			for (int j = 0; j < Constantes.LARGEUR_PLATEAU; j++) 
			{
				print(tilesInt[i,j]);
			}
		}

		construction ();


	}

}
