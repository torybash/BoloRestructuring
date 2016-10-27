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

		private Vector2 _scrollPosition;

		private Vector2 _boxSize;

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
			if (s_window == null) Init();

			startX = 5f; startY = 5f;

			EditorGUI.LabelField(new Rect(startX, startY, cWidth, cHeight), "Weapons", EditorStyles.boldLabel);
			startY += cHeight + 5f;

			var visibleRect=new Rect(startX, startY, s_window.position.width-startX-10, s_window.position.height-startY-5);
			var contentRect=new Rect(startX, startY, _boxSize.x, (_boxSize.y * _weapons.Count + 10) + 10);

			//DrawBox(new Rect(startX - 5, startY - 5, contentRect.width + 10, contentRect.height + 10), new Color(0.65f, 0.7f, 0.9f));

			_scrollPosition = GUI.BeginScrollView(visibleRect, _scrollPosition, contentRect);
			for (int i = 0; i < _weapons.Count; i++)
			{
				var weapon = _weapons[i];
				DrawBox(new Rect(startX, startY += 5, _boxSize.x + 10, _boxSize.y + 10), new Color(0.8f * (i%2), 0.8f, 0.8f));
				_boxSize = DrawWeaponBox(weapon, startX + 5, startY + 5);
				startY += _boxSize.y + 10f;
			}
			GUI.EndScrollView();

			if(GUI.changed) EditorUtility.SetDirty(_library);
		}


		private Vector2 DrawWeaponBox(WeaponData weapon, float startX, float startY)
		{
			var boxSize = new Vector2(startX, startY);

			cont = new GUIContent("Name", "Name of weapon");
			EditorGUI.LabelField(new Rect(startX, startY, cWidth, cHeight), cont, EditorStyles.boldLabel);
			weapon.title = EditorGUI.TextField(new Rect(startX + cSpaceX, startY, cWidth, cHeight), weapon.title);

			cont = new GUIContent("Cooldown", "How often the weapon is able to shoot");
			EditorGUI.LabelField(new Rect(startX, startY += cSpaceY, cWidth, cHeight), cont);
			weapon.cooldownDuration = EditorGUI.FloatField(new Rect(startX + cSpaceX, startY, cWidth, cHeight), weapon.cooldownDuration);
			startY += cHeight;

			boxSize.x = cWidth + cSpaceX;
			boxSize.y = startY - boxSize.y;
			
			return boxSize;
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
