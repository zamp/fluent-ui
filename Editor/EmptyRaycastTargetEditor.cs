using FluentUI.Components;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Editor
{
	[CanEditMultipleObjects, CustomEditor(typeof(EmptyRaycastTarget), false)]
	public class EmptyRaycastTargetEditor : GraphicEditor
	{
		public override void OnInspectorGUI()
		{
			base.serializedObject.Update();
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
			EditorGUI.EndDisabledGroup();
			// skipping AppearanceControlsGUI
			base.RaycastControlsGUI();
			base.serializedObject.ApplyModifiedProperties();
		}
	}
}