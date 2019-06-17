/*
	Author:			小何
	CreateDate:		2019-06-12 11:28:25
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class PlayAnimationSample : MonoBehaviour 
	{
        PlayableGraph playableGraph;
        public AnimationClip clip;

        private void Start()
        {
            playableGraph = PlayableGraph.Create();
            playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
            playableOutput.SetSourcePlayable(clipPlayable);
            playableGraph.Play();
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}