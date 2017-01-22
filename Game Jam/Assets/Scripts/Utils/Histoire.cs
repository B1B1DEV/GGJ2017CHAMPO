using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #lolilol
public delegate void UpdateHistoireAction ();

/// <summary>
/// Histoire.
/// Ceci est un monstre, mais c'est aussi mon enfant, je ne peut plus m'en débarasser maintenant. 
/// </summary>
public class Histoire<T> {



	public delegate T GetValueDelegate();

	private T[] histoire = new T[Constantes.MEMOIRE_ENTITEES];
	int initialTime;
	GetValueDelegate getter;

	public Histoire ( GetValueDelegate getter )
	{
		initialTime = GameManager.time;
		this.getter = getter;

		UpdateValeurActuelle ();
		GameManager.Instance.OnUpdateHistoire += UpdateValeurActuelle;
	}

	/*
	~Histoire()
	{
		GameManager.Instance.OnUpdateHistoire -= UpdateValeurActuelle;
	}
	*/
	public T ValeurActuelle { get { return histoire [(GameManager.time - initialTime) % Constantes.MEMOIRE_ENTITEES]; } } 

	public bool ValeurPassee(int nombreDeTour ,out T valeur)
	{
		if (GameManager.time - nombreDeTour  < initialTime || nombreDeTour > Constantes.MEMOIRE_ENTITEES ) {
			valeur = default(T);
			return false;
		}
		valeur = histoire [(GameManager.time - nombreDeTour - initialTime) % Constantes.MEMOIRE_ENTITEES];
		return true;
	}
		
	public void UpdateValeurActuelle ()
	{
		histoire [(GameManager.time - initialTime) % Constantes.MEMOIRE_ENTITEES] = getter ();
	}

	public void Terminer()
	{
		GameManager.Instance.OnUpdateHistoire -= UpdateValeurActuelle;
	}
}
