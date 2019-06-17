/*
	Author:			小何
	CreateDate:		2019-06-12 19:26:53
	Desc:			MonoBehaviour脚本类.
*/

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace CutcenesEditor
{
	public class PlayQueuePlayable : MonoBehaviour 
	{
        private int m_CurrentClipIndex = -1;
        private float m_TimeToNextClip;
        private Playable mixer;
        public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
        {
            owner.SetInputCount(1);
            mixer = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);
            graph.Connect(mixer, 0, owner, 0);
            owner.SetInputWeight(0, 1);
            for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
            {
                graph.Connect(AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]), 0, mixer, clipIndex);
                mixer.SetInputWeight(clipIndex, 1.0f);
            }
        }
        //override public void PrepareFrame(Playable owner, FrameData info)
        //{
        //    if (mixer.GetInputCount() == 0)
        //        return;
        //    m_TimeToNextClip -= (float)info.deltaTime;
        //    if (m_TimeToNextClip <= 0.0f)
        //    {
        //        m_CurrentClipIndex++;
        //        if (m_CurrentClipIndex >= mixer.GetInputCount())
        //            m_CurrentClipIndex = 0;
        //        var currentClip = (AnimationClipPlayable)mixer.GetInput(m_CurrentClipIndex);
        //        currentClip.SetTime(0);
        //        m_TimeToNextClip = currentClip.GetAnimationClip().length;
        //    }
        //    for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
        //    {
        //        if (clipIndex == m_CurrentClipIndex)
        //            mixer.SetInputWeight(clipIndex, 1.0f);
        //        else
        //            mixer.SetInputWeight(clipIndex, 0.0f);
        //    }
        //}
	}
}