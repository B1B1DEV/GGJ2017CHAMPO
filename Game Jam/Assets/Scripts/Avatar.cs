using UnityEngine;

public class Avatar: Entite
{


	public override void Move()
	{
		
	}


	void Update() {
		/*
		GameObject hoveredTile = GetMouseOveredTile ();
		if (Input.GetMouseButtonDown(0) && hoveredTile != null) {
			transform.position = hoveredTile.transform.position + Vector3.up;
		}
		*/
		if (Input.GetKeyDown (KeyCode.Space)) {
			//GameManager.Instance.pulse.
			GameManager.Instance.pulse.sourcePoint = transform.position;
			GameManager.Instance.pulse.Fireflash ();
		}

		Camera.main.transform.LookAt (transform.position);
		Camera.main.transform.position = transform.position-10f * Camera.main.transform.forward;
	}


	GameObject GetMouseOveredTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Tile"))) {
			return hit.transform.gameObject;
		}

		return null;
	}
}