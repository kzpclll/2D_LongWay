using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public abstract class TLPlayerBase : MonoBehaviour
{
    public PlayableDirector[] timelines;
    public int defaultTimelineIndex;

    public abstract void PlayTimelineAwake();
    public abstract void PlayTimelineRuntime(int index);

    public abstract void Initialize();
}
