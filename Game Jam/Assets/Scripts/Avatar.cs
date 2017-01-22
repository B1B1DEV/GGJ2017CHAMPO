using UnityEngine;

public class Avatar: Entite
{
	private Tile nextTile;
	private bool nextPulse;

	void Start()
	{
		base.Start ();
		nextTile = null;
		nextPulse = false;
		Camera.main.transform.position = transform.position + new Vector3 (2, 4, 1);
		Camera.main.transform.LookAt (transform.position);
	}

	public override void Move()
	{
		if (nextPulse)
		{
			//GameManager.Instance.pulse.
			GameManager.Instance.pulse.sourcePoint = transform.position + 0.5f*Vector3.up;
			GameManager.Instance.pulse.Fireflash ();
		} else if (nextTile)
		{
			if (nextTile.CurrentState == Tile.State.None && nextTile.nextState == Tile.State.None)
			{
				GameManager.Instance.tiles [PositionActuelle.x, PositionActuelle.y].nextState = Tile.State.None;
				PositionActuelle = new Coord(nextTile.transform.position);
				nextTile.nextState = Tile.State.Player;

				// test piège
				if (nextTile.type == Tile.Type.Piege)
				{
					GameManager.Instance.Lose();
				}
			}

			Camera.main.transform.position = transform.position + new Vector3 (2, 4, 1);
			Camera.main.transform.LookAt (transform.position);
		}

		// on remet à zéro
		nextPulse = false;
		nextTile = null;
	}

	// on récupère les commandes
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			nextTile = GetMouseOveredTile ();
			if (nextTile == null)
				return;
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