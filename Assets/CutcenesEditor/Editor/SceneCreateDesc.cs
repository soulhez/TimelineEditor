/*
 所有新建文件都会自动替换头文件描述

步骤：
1.修改UNITY自带的脚本模板文件 Unity.app/Contents/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt
2.替换内容:
/*
Author:			#AuthorName#
CreateDate:		#CreateDate#
Desc:			MonoBehaviour脚本类.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace #DEFINENAMESPACE#
{
    public class #SCRIPTNAME# : MonoBehaviour 
    {

	    // Use this for initialization
	    void Start () 
	    {
		    #NOTRIM#
	    }

	    // Update is called once per frame
	    void Update () 
	    {
		    #NOTRIM#
	    }
    }
}
*/
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

namespace CowBoy.Game
{
    /// <summary>
    /// Editor自动创建脚本描述类
    /// </summary>
    public class ScriptCreateDesc : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            Debug.Log(path);
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string strContent = File.ReadAllText(path);
                strContent = strContent.Replace(CutcenesEditorData.Namespace.Key, CutcenesEditorData.Namespace.Value).Replace(CutcenesEditorData.Author.Key, CutcenesEditorData.Author.Value).Replace(CutcenesEditorData.CreateDate.Key, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                File.WriteAllText(path, strContent);
                AssetDatabase.Refresh();
            }
        }
    }
}