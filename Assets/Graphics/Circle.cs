/*
	Author:			小何
	CreateDate:		2019-06-14 15:11:26
	Desc:			MonoBehaviour脚本类.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesignGraphics
{
	public class Circle : MonoBehaviour 
	{
        private float r = 5;
        private float angle = 0;

		// Use this for initialization
		void Start () 
		{
            DrawCircle();
		}

        // Update is called once per frame
        void Update()
        {

        }

        private void DrawCircle()
        {
            Vector3 center = Vector3.zero;
            GameObject parent = new GameObject("Circle");
            for(int i = 0; i < 36; i ++)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                float rad = (angle / 180) * Mathf.PI;
                float x = center.x + r * Mathf.Cos(rad);
                float z = center.z + r * Mathf.Sin(rad);
                go.transform.position = new Vector3(x, 0, z);
                go.name = i.ToString();
                go.transform.SetParent(parent.transform);
                go.transform.LookAt(center);
                go.transform.localScale = new Vector3(1, 1, 3);
                angle += 10;
            }
        }
	}
}