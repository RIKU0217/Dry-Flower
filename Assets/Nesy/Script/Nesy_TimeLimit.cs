using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nesy_TimeLimit : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject timerText;

    [SerializeField]
    public string Name;
    [SerializeField]
    float time;
    void Start()
    {
        this.timerText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        if(time > 0.0f)
        {
            this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        }
        else
        {
            this.time = 0.0f;
            this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        }
    }
}
