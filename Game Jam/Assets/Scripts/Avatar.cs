using UnityEngine;

public class Avatar: Entite
{


	public override void Move()
	{
		
	}


	void Update() {
		

		if (Input.GetMouseButtonDown(0)) {
			GameObject hoveredTile = GetMouseOveredTile ();
			if (hoveredTile)
				transform.position = hoveredTile.transform.position;
		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			//GameManager.Instance.pulse.
			GameManager.Instance.pulse.sourcePoint = transform.position;
			GameManager.Instance.pulse.Fireflash ();
		}

		Camera.main.transform.LookAt (transform.position);
		Camera.main.transform.position = transform.position-4f * Camera.main.transform.forward;
	}


	GameObject GetMouseOveredTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Tile"),QueryTriggerInteraction.Collide)) {
			return hit.transform.gameObject;
		}

		return null;
	}
}