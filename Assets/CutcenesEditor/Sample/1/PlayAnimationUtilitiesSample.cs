/*
	Author:			小何
	CreateDate:		2019-06-12 11:36:45
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Playables;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class PlayAnimationUtilitiesSample : MonoBehaviour 
	{
        public AnimationClip clip;
        PlayableGraph playableGraph;
        private void Start()
        {
            AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out playableGraph);
        }
        private void OnDisable()
        {
            playableGraph.Destroy();
        }
	}
}