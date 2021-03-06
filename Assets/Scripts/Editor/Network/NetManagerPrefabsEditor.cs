﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using Bolo.Net;

namespace Bolo.EditorExt
{
	[CustomEditor(typeof(NetManagerPrefabs))]
	public class NetManagerPrefabsEditor : Editor {

		private NetManagerPrefabs Target { get { return (NetManagerPrefabs)target; } }

		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if (GUILayout.Button("Add prefab references")) {
				AddPrefabReferences();
			}
		}

		private void AddPrefabReferences() {
			throw new System.NotImplementedException("TODO");
		}
	}
}