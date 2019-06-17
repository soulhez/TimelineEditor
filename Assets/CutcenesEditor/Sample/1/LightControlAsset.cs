/*
	Author:			小何
	CreateDate:		2019-06-12 16:34:19
	Desc:			MonoBehaviour脚本类.
*/
using UnityEngine;
using UnityEngine.Playables;

namespace CutcenesEditor
{
	public class LightControlAsset : PlayableAsset
    {
        public ExposedReference<Light> light;
        public float intensity = 1.0f;

        private Color color = Color.green;

        public override Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<LightControlBehaviour>.Create(graph);
            var lightControlBehaviour = playable.GetBehaviour();
            lightControlBehaviour.light = light.Resolve(graph.GetResolver());
            lightControlBehaviour.color = color;
            lightControlBehaviour.intensity = intensity;
            return playable;
        }
    }
}