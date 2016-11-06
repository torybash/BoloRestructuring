using UnityEngine;
using System.Linq;
using System.Collections;

namespace Bolo.DataClasses
{
	public abstract class LibraryObject<T> : ScriptableObject where T : ScriptableObject
	{
		private static T s_libObject;
		public static T sLibObject
		{
			get
			{
				if (s_libObject == null) s_libObject = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
				if (s_libObject == null)
				{
					//TODO create scriptable object
				}
				return s_libObject;
			}
		}

		
	}
}