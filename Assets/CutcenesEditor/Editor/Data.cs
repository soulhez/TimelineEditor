/*
	Author:			小何
	CreateDate:		2019-06-12 11:07:02
	Desc:			数据脚本类.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CutcenesEditorData
{
    public const string SciptTemplatesPath = "E:\\Program Files\\Unity2017\\Editor\\Data\\Resources\\ScriptTemplates";
    public const string NewBehaviourScriptName = "81-C# Script-NewBehaviourScript.cs.txt";

    const string NameSpaceKey = "#DEFINENAMESPACE#";
    const string NameSpaceValue = "CutcenesEditor";

    const string AuthorKey = "#AuthorName#";
    const string AuthorValue = "小何";

    const string CreateDateKey = "#CreateDate#";

    public static KeyValuePair<string, string> Namespace = new KeyValuePair<string, string>(NameSpaceKey, NameSpaceValue);
    public static KeyValuePair<string, string> Author = new KeyValuePair<string, string>(AuthorKey, AuthorValue);
    public static KeyValuePair<string, string> CreateDate = new KeyValuePair<string, string>(CreateDateKey, null);
}