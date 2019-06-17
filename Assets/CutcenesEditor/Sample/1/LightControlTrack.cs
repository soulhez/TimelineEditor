/*
	Author:			小何
	CreateDate:		2019-06-12 16:38:54
	Desc:			MonoBehaviour脚本类.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CutcenesEditor
{
    [TrackClipType(typeof(LightControlAsset))]
    [TrackBindingType(typeof(Light))]
    public class LightControlTrack : TrackAsset
    {
        //public Light light = null;
        public Color color = Color.white;
        public float intensity = 1f;

        //public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        //{
        //    Light light = playerData as Light;
        //    if (light != null)
        //    {
        //        light.color = color;
        //        light.intensity = intensity;
        //    }
        //}
    }
}