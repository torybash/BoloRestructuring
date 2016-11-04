using UnityEngine;
using System;
using UnityDBG;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class D {

	public delegate void DebugLogger(object message, string tag = "");
	public static DebugLogger LogDelegate;


	[SerializeField] private string assetPath = "Assets/Plugins/DBG/DBGTags.asset";

    public static bool _initialized = false;

	private static D s_instance;	
	private static D sInstance
	{
		get
		{
			if (s_instance == null) s_instance = new D();
			return s_instance;
		}
	} 


    public static void Init() {
        Debug.Log("Initializing D!");

		LogDelegate += DBG.Log;

        if (UnityDBG.DBG.DbgTags == null) {
            SetDBGTagReference();
        }

        _initialized = true;
    }

    private static void SetDBGTagReference() {

		DBGTags dbgTags = AssetDatabase.LoadAssetAtPath<DBGTags>(sInstance.assetPath);
		//DBGTags dbgTags = Resources.LoadAssetAtPath<DBGTags>(assetPath);;
#if UNITY_EDITOR

		if (dbgTags == null) {
            dbgTags = ScriptableObject.CreateInstance<DBGTags>();
            AssetDatabase.CreateAsset(dbgTags, sInstance.assetPath);
        }
#endif
        UnityDBG.DBG.DbgTags = dbgTags;
    }



    public static DebugLogger Log {
        get {
            if (!_initialized) Init();
            return LogDelegate;
        }
    }

    //public static DebugLogger LogWarning {
    //    get {
    //        Check();
    //        return LogWarningDelegate;
    //    }
    //}

    //public static DebugLogger LogError {
    //    get {
    //        Check();
    //        return LogErrorDelegate;
    //    }
    //}
}
