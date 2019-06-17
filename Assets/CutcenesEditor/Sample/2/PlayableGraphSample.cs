/*
	Author:			小何
	CreateDate:		2019-06-13 16:02:47
	Desc:			MonoBehaviour脚本类.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace CutcenesEditor
{
    [RequireComponent(typeof(Animator))]
    public class PlayableGraphSample : MonoBehaviour 
	{
        PlayableGraph playableGraph;
        public AnimationClip clip1;
        public AnimationClip clip2;

        float weight;

        AnimationMixerPlayable mixerPlayable;

        // Use this for initialization
        void Start () 
		{
            CreatePlayableGraph();
        }
	
		// Update is called once per frame
		void Update () 
		{
			
		}

        void CreatePlayableGraph()
        {
            playableGraph = PlayableGraph.Create();
            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "PlayableGraphSample", GetComponent<Animator>());

            mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
            playableOutput.SetSourcePlayable(mixerPlayable);
            var clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);
            var clipPlayable2 = AnimationClipPlayable.Create(playableGraph, clip2);
            //playableGraph.Connect(clipPlayable1,0,mixerPlayable,0);
            //playableGraph.Connect(clipPlayable2, 0, mixerPlayable, 1);
            //上面的两行与下面的两行代码是等价的
            mixerPlayable.ConnectInput(0, clipPlayable1, 0);
            mixerPlayable.ConnectInput(1, clipPlayable2, 0);
            weight = Mathf.Clamp01(weight);
            mixerPlayable.SetInputWeight(0, 1.0f - weight);//分别设置两个数据端口的权重
            mixerPlayable.SetInputWeight(1, weight);
            playableGraph.Play();
            GraphVisualizerClient.Show(playableGraph, this.GetType().Name);
        }
	}
}