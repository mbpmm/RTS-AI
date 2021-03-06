﻿
using UnityEngine;
using System.Collections;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(ExplorerSight))]
public class FieldOfViewEditor : Editor
{
	void OnSceneGUI()
	{
		ExplorerSight fow = (ExplorerSight)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.detectionCol.radius);
		Vector3 viewAngleA = fow.DirFromAngle(-fow.fovAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle(fow.fovAngle / 2, false);

		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.detectionCol.radius);
		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.detectionCol.radius);

		Handles.color = Color.red;

        if (fow.spotInSight)
        {
			Handles.DrawLine(fow.transform.position, fow.transform.position);
		}
    }
}

#endif