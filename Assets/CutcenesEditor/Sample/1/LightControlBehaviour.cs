/*
	Author:			小何
	CreateDate:		2019-06-12 16:32:29
	Desc:			MonoBehaviour脚本类.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace CutcenesEditor
{
	public class LightControlBehaviour : PlayableBehaviour
    {
        public Light light = null;
        public Color color = Color.white;
        public float intensity = 1f;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (light != null)
            {
                light.color = color;
                light.intensity = intensity;
            }
        }
    }
}