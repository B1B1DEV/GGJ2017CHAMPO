using UnityEngine;

public class Avatar: Entite
{


	public override void Move()
	{
		
	}


	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click, casting ray.");
			// CastRay();
		}
	}


	GameObject GetMouseOveredTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)) {
			Debug.DrawLine(ray.origin, hit.point);
		}

		return null;
	}
}