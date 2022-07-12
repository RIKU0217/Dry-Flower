using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameBGMLoopManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    private int LoopEndSamples; // A
    private int LoopLengthSamples; // B

    private void Start()
    {
        LoopEndSamples = 6301885;
        LoopLengthSamples = 5538136;
    }

    private void Update()
    {
        if (AudioSource.timeSamples >= LoopEndSamples)
        {
            AudioSource.timeSamples -= LoopLengthSamples;
        }
    }
}
