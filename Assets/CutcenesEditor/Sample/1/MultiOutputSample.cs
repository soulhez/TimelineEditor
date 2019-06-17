/*
	Author:			小何
	CreateDate:		2019-06-12 19:22:36
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class MultiOutputSample : MonoBehaviour 
	{
        public AnimationClip animationClip;
        public AudioClip audioClip;
        PlayableGraph playableGraph;
        private void Start()
        {
            playableGraph = PlayableGraph.Create();
            var animationOutPut = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            var audioOutput = AudioPlayableOutput.Create(playableGraph, "Audio", GetComponent<AudioSource>());
            var animationClipPlayable = AnimationClipPlayable.Create(playableGraph, animationClip);
            var audioClipPlayable = AudioClipPlayable.Create(playableGraph, audioClip, true);
            animationOutPut.SetSourcePlayable(animationClipPlayable);
            audioOutput.SetSourcePlayable(audioClipPlayable);
            playableGraph.Play();
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}