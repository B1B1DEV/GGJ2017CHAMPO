using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Neon : MonoBehaviour {


	public Color hlc = new Color (0.9f, 0.8f, 0.3f);

	public Material mat;

	private static Material _newmat = null;
	public Material newmat { 
		get {
			if (_newmat == null) {
				_newmat = new Material (mat);

				_newmat.DOColor (hlc * Mathf.LinearToGammaSpace (0.3f), "_EmissionColor", 0.1f).SetLoops (-1, LoopType.Yoyo);
				_newmat.EnableKeyword ("_EMISSION");
			}
			return _newmat;
		}
	}

	public void Start()
	{

			
		GetComponent<MeshRenderer> ().materials [1] = newmat;

	}
}
