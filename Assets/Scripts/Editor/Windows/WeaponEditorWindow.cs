using UnityEngine;
using UnityEditor;
using System.Collections;
using Bolo.Util;
using System;
using System.Collections.Generic;

namespace Bolo.EditorExt
{
	public class WeaponEditorWindow : EditorWindow
	{
		private static WeaponEditorWindow s_window;

		private List<WeaponData> _weapons;

		private WeaponLibrary _library;
		
		private GUIContent cont;
		private float startX, startY;

		//Default values
		private const float cWidth = 150f, cHeight = 18f;
		private const float cSpaceX = 110f, cSpaceY = 20f;


		[MenuItem("Bolo/Weapon Editor")]
		private static void Init()
		{
			s_window = GetWindow<WeaponEditorWindow>();
			s_window.Show();
			s_window.titleContent.text = "Weapons";
			//s_window.titleContent.image = //TODO

			s_window.LoadLibrary();
		}


		private void LoadLibrary()
		{
			_library = WeaponLibrary.libObject;
			_weapons = WeaponLibrary.GetWeapons();
		}

		void OnGUI()
		{
			startX = 5f; startY = 5f;

			EditorGUI.LabelField(new Rect(startX, startY, cWidth, cHeight), "Weapons", EditorStyles.boldLabel);

			//GUI.BeginScrollView()
			foreach (var weapon in _weapons)
			{

				//Field("Name", "Name of weapon", ref weapon.title));

				DrawBox(new Rect(startX, startY += cSpaceY, position.width - startX - 10, 40), new Color(0.8f, 0.8f, 0.8f));

				cont = new GUIContent("Name", "Name of weapon");
				EditorGUI.LabelField(new Rect(startX, startY, cWidth, cHeight), cont);
				weapon.title = EditorGUI.TextField(new Rect(startX + cSpaceX, startY, cWidth, cHeight), weapon.title);

				cont = new GUIContent("Cooldown", "How often the weapon is able shoot");
				EditorGUI.LabelField(new Rect(startX, startY += cSpaceY, cWidth, cHeight), cont);
				weapon.cooldownDuration = EditorGUI.FloatField(new Rect(startX + cSpaceX, startY, cWidth, cHeight), weapon.cooldownDuration);


			}


			if(GUI.changed) EditorUtility.SetDirty(_library);
		}


		private void DrawBox(Rect rect, Color color)
		{
			GUI.color = color;
			GUI.Box(rect, "");
			GUI.color = Color.white;
		}

		//private void Field<T>(string title, string tooltip, ref T obj, float spaceX = 0, float spaceY = 0)
		//{


		//	cont = new GUIContent(title, tooltip);
		//	EditorGUI.LabelField(new Rect(startX += spaceX, startY += spaceY, cWidth, cHeight), cont);
		//	if (obj.GetType() == typeof(string))
		//	{
		//		obj = EditorGUI.TextField(new Rect(startX + cSpaceX, startY, cWidth, cHeight), (string) (object)obj);
		//	}else if (obj.GetType() == typeof(int))
		//	{

		//	}else if (obj.GetType() == typeof(float))
		//	{

		//	}
		//}
	}

}
