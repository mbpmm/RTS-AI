
using UnityEngine;
using System.Collections;
using UnityEditor;

#if UNITY_EDITOR

[CustomEditor(typeof(MinerSight))]
public class FieldOfViewEditor2 : Editor
{
	void OnSceneGUI()
	{
		MinerSight fow = (MinerSight)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.detectionCol.radius);
		Vector3 viewAngleA = fow.DirFromAngle(-fow.fovAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle(fow.fovAngle / 2, false);

		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.detectionCol.radius);
		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.detectionCol.radius);

		Handles.color = Color.red;

        if (fow.mineInSight)
        {
			Handles.DrawLine(fow.transform.position, fow.transform.position);
		}
    }
}

#endif