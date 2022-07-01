using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public float minutes = 0;
    public float seconds = 0;
    TextMeshProUGUI timeCountText;

    void Start()
    {
        timeCountText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds >= 60f)
        {
            minutes++;
            seconds = seconds - 60;
        }

        if (seconds < 10f)
        {
            timeCountText.SetText("TimeCount : {0}:0{1}", (int)minutes, (int)seconds);
        }
        else
        {
            timeCountText.SetText("TimeCount : {0}:{1}", (int)minutes, (int)seconds);
        }
    }
}
