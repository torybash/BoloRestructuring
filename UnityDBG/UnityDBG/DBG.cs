using System;
using System.Collections.Generic;
//using System.Linq;

using UnityEngine;



namespace UnityDBG {
    public class DBG {

        private static DBGTags dbgTags;
        public static DBGTags DbgTags {
            get { return dbgTags; }
            set { dbgTags = value; }
        }

        private static string nextMsgTag = "";


        public static void Log(object obj, string tag = "") {
           
            DBGTag dbgTag = dbgTags.GetTag(tag);
            string str  = (string) obj;
            if (dbgTag != null) str = ApplyTag(str, dbgTag);
            if (str != null) Debug.Log(str);
        }

        //public static void Log(object obj) {
        //    obj = nextMsgTag + obj;
        //    obj = CheckAndUseTag(obj);
        //    if (obj != null) Debug.Log(obj);
        //}
        //public static void Log(object obj, UnityEngine.Object context) {
        //    obj = CheckAndUseTag(obj);
        //    if (obj != null) Debug.Log(obj, context);
        //}

        //public static void LogWarning(object obj) {
        //    obj = CheckAndUseTag(obj);
        //    if (obj != null) Debug.LogWarning(obj);
        //}
        //public static void LogError(object obj) {
        //    obj = CheckAndUseTag(obj);
        //    if (obj != null) Debug.LogError(obj);
        //}

        //public static void PrepareLog(string tag) {
        //    nextMsgTag = tag;
        //}


        private static string ApplyTag(string msg, DBGTag dbgTag) {
            if (!dbgTag.show) {
                return null;
            }

            msg = "[" + dbgTag.tag + "]" + msg;

            //Bold tag text
            msg = msg.Insert(0, "<b>");
            msg = msg.Insert(msg.IndexOf("]".ToCharArray()[0]) + 1, "</b>");

            //Color text
            msg = "<color=" + dbgTag.GetHexColor() + ">" + msg + "</color>";            
            return msg;
        }
    }


    [System.Serializable]
    public class DBGTag {
        public string tag;
        public bool show;
        public Color color;
        [NonSerialized] private string colorHex = "";

        public string GetHexColor() {
            if (colorHex.Length == 0) {
                Color32 color32 = (Color32)color;
                colorHex = "#" + color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
            }
            return colorHex;
        }
    }

    [System.Serializable]
    public class DBGTags : ScriptableObject {
        public List<DBGTag> tags;

        public DBGTag GetTag(string tag) {
            foreach (DBGTag dbgTag in tags) {
                if (dbgTag.tag == tag) {
                    return dbgTag;
                }
            }
            return null;
        }
    }
}
