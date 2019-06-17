/*
	Author:			小何
	CreateDate:		2019-06-12 19:26:02
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class PlayWithTimeControlSample : MonoBehaviour 
	{
        public AnimationClip clip;
        public float time;
        PlayableGraph playableGraph;
        AnimationClipPlayable playableClip;

        private void Start()
        {
            playableGraph = PlayableGraph.Create();
            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            playableClip = AnimationClipPlayable.Create(playableGraph, clip);
            playableOutput.SetSourcePlayable(playableClip);
            playableGraph.Play();
            playableClip.SetPlayState(PlayState.Paused);
        }
        private void Update()
        {
            playableClip.SetTime(time);
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}