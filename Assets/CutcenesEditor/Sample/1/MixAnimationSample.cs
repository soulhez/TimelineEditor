/*
	Author:			小何
	CreateDate:		2019-06-12 11:37:50
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class MixAnimationSample : MonoBehaviour 
	{

        public AnimationClip clip0;
        public AnimationClip clip1;
        public float weight;
        PlayableGraph playableGraph;
        AnimationMixerPlayable mixerPlayable;
        void Start()
        {
            playableGraph = PlayableGraph.Create();
            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
            playableOutput.SetSourcePlayable(mixerPlayable);
            var clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
            var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);
            playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);
            playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);
            playableGraph.Play();
        }
        private void Update()
        {
            weight = Mathf.Clamp01(weight);
            mixerPlayable.SetInputWeight(0, 1.0f - weight);
            mixerPlayable.SetInputWeight(1, weight);
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}