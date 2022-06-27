using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swith();
    }

    [SerializeField]
    public GameObject  gameObject;
    public void Swith()
    {
        if(gameObject == null)
        {
            SceneLoad();
        }
       
    }
    [SerializeField]
    private string sceneName;

    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("シーンロード");
    }

    [SerializeField]
    GameObject Howit;
    public void Howitfrashtrue()
    {
        Howit.SetActive(true);
    }

    public void Howitfrashfale()
    {
        Howit.SetActive(false);
    }

    public void BasibasiSE()
    {
        Nesy_SoundManager.instance.PlaySE(0);
    }

    public Fungus.Flowchart flowchart = null;

    public string sendMessage;

    public int x = 0;
    public void StartFungusFromEventPlane()
    {
        if (x == 1)
        {
            flowchart.SendFungusMessage(sendMessage);
        }
        else
        {
            x = 1;
        }
    }

    public bool FungusFinish;
    public void FinishFungus()
    {
        FungusFinish = true;
    }

    [SerializeField]
    GameObject Animation;
    public void AnimationStart()
    {
        if (FungusFinish == true)
        {
            Animation.SetActive(true);
        }
    }




}
