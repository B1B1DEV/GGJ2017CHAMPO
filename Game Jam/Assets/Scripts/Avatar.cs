using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Avatar: Entite
{
	private Tile nextTile;
	private bool nextPulse;
	private bool openDoor;

	//Sound
	public AudioSource[] sounds;
	public AudioSource noise1;
	public AudioSource noise2;

	void Start()
	{
		base.Start ();
		nextTile = null;
		nextPulse = false;
		openDoor = false;
		Camera.main.transform.position = transform.position + new Vector3 (2, 4, 1);
		Camera.main.transform.LookAt (transform.position);

		Tile tile = GameManager.Instance.tiles [PositionActuelle.x, PositionActuelle.y];
		tile.CurrentState = Tile.State.Player;
		tile.nextState = Tile.State.Player;

		sounds = GetComponents<AudioSource> ();
		noise1 = sounds [0];
		noise2 = sounds [1];
	}

	public override void Move()
	{
		if (openDoor)
		{
			nextTile.type = Tile.Type.PorteOuverte;
			nextTile.nextState = Tile.State.None;
		}

		int x = PositionActuelle.x;
		int y = PositionActuelle.y;
		bool bloque = true;

		if (bloque && GameManager.Instance.tiles [x, y + 1].CurrentState == Tile.State.None)
			bloque = false;
		else if (bloque && GameManager.Instance.tiles [x+1, y].CurrentState == Tile.State.None)
			bloque = false;
		else if (bloque && GameManager.Instance.tiles [x+1, y - 1].CurrentState == Tile.State.None)
			bloque = false;
		else if (bloque && GameManager.Instance.tiles [x, y - 1].CurrentState == Tile.State.None)
			bloque = false;
		else if (bloque && GameManager.Instance.tiles [x-1, y - 1].CurrentState == Tile.State.None)
			bloque = false;
		else if (bloque && GameManager.Instance.tiles [x-1, y].CurrentState == Tile.State.None)
			bloque = false;
		else
		{
			// on est bloqué
			GameManager.Instance.Lose ();
		}
		
		if (nextPulse)
		{
			//GameManager.Instance.pulse.
			GameManager.Instance.pulse.sourcePoint = transform.position + 0.5f*Vector3.up;
			GameManager.Instance.pulse.Fireflash ();
			//Sound
			//source.PlayOneShot(source.clip, 1.0f);
			noise1.Play();
		} else if (nextTile)
		{
			if (nextTile.CurrentState == Tile.State.None && nextTile.nextState == Tile.State.None)
			{
				GameManager.Instance.tiles [x, y].nextState = Tile.State.None;
				//PositionActuelle = new Coord(nextTile.transform.position);
                this.transform.DOMove(nextTile.transform.position, 1f);
                nextTile.nextState = Tile.State.Player;

				// test piège et porte ouverte
				if (nextTile.type == Tile.Type.Piege)
				{
					GameManager.Instance.Lose ();
				} else if (nextTile.type == Tile.Type.PorteOuverte)
				{
					GameManager.Instance.Win ();
				}

				noise2.Play ();
			}

            // Recenter camera
            //Camera.main.transform.position = Vector3.Lerp(transform.position + new Vector3(2, 4, 1), Camera.main.transform.position, 0.99f);
            //Camera.main.transform.LookAt (transform.position);
		}

		// on remet à zéro
		nextPulse = false;
		nextTile = null;
		openDoor = false;
	}

	// on récupère les commandes
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			nextTile = GetMouseOveredTile ();

			if (nextTile == null)
				return;
			
			if (nextTile.type == Tile.Type.Porte)
			{
				openDoor = true;
				return;
			}

			Vector3 deplacement = nextTile.transform.position - PositionActuelle.ToVector3 ();
			if (deplacement.magnitude > 2 * Constantes.INNER_RADIUS + 1e-3)
			{
				nextTile = null;
			}
			nextPulse = false;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			nextPulse = true;
		}

		// Recenter camera
		Camera.main.transform.position = Vector3.Lerp(transform.position + new Vector3(2, 4, 1), Camera.main.transform.position, 0.99f);
	}


	Tile GetMouseOveredTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile"),QueryTriggerInteraction.Collide)) {
			return hit.transform.gameObject.GetComponent<Tile>();
		}

		return null;
	}
}