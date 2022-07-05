using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Nesy_TimeLine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public PlayableDirector playableDirector;
    public void StopTimeLine()
    {
        playableDirector.Pause();
    }

    public void StartTimeLine()
    {
        playableDirector.Resume();
        Debug.Log("resume");
        
    }

    TimelineProjectSettings timeline;
    
}
