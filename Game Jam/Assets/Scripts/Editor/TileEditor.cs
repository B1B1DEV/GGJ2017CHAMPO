using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(Tile))]
public class TileEditor : Editor {
	public override void OnInspectorGUI()
	{
		Tile tile = (Tile)target;

		EditorGUI.indentLevel = 0;

		tile.type = (Tile.Type)EditorGUILayout.EnumPopup ("Type", tile.type);

		tile.shownIntensity = EditorGUILayout.Slider ("Shown Intensity", tile.shownIntensity, 0, 1);
	}


}
