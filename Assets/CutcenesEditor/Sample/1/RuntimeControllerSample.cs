/*
	Author:			小何
	CreateDate:		2019-06-12 19:21:42
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace CutcenesEditor
{
	public class RuntimeControllerSample : MonoBehaviour 
	{
        public AnimationClip clip;
        public RuntimeAnimatorController controller;
        public float weight;
        PlayableGraph playableGraph;
        AnimationMixerPlayable mixerPlayable;
        private void Start()
        {
            playableGraph = PlayableGraph.Create();
            var playableOutPut = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
            playableOutPut.SetSourcePlayable(mixerPlayable);
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
            var ctrlPlayable = AnimatorControllerPlayable.Create(playableGraph, controller);
            playableGraph.Connect(clipPlayable, 0, mixerPlayable, 0);
            playableGraph.Connect(ctrlPlayable, 0, mixerPlayable, 1);
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