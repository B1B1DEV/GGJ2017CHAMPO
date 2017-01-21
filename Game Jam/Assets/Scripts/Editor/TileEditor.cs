using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor (typeof(Tile))]
public class TileEditor : Editor {
	public override void OnInspectorGUI()
	{
		Tile tile = (Tile)target;

		EditorGUI.indentLevel = 0;

		tile.type = (Tile.Type)EditorGUILayout.EnumPopup ("Type", tile.type);
	}


}
