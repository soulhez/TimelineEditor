/*
	Author:			小何
	CreateDate:		2019-06-12 19:27:37
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class PlayQueueSample : MonoBehaviour 
	{
        public AnimationClip[] clipsToPlay;
        PlayableGraph playableGraph;
        private void Start()
        {
            //playableGraph = PlayableGraph.Create();
            //var playQueuePlayable = ScriptPlayable<PlayQueuePlayable>.Create(playableGraph);
            //var playQueue = playQueuePlayable.GetBehaviour();
            //playQueue.Initialize(clipsToPlay, playQueuePlayable, playableGraph);
            //var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());
            //playableOutput.SetSourcePlayable(playQueuePlayable);
            //playableOutput.SetSourceInputPort(0);
            //playableGraph.Play();
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}