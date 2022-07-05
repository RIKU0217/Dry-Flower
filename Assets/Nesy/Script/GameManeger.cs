using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Playables;

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
        Invok();
    }

    [SerializeField]
    public GameObject  gameObject1;
    public void Swith()
    {
        if(gameObject1 == null)
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

    public void StartFungus()
    {
        flowchart.SendFungusMessage("Start");
        Debug.Log("ee");
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

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;
    public void StartScene()
    { 
        source.PlayOneShot(clip);
        Invoke("SceneLoad", 2);

    }

    [SerializeField]
    public Image image;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    float interval;

    private int i = 0;

    bool intervalswith = false;
    
    public void DownLoad()
    {
        image.sprite = sprites[i];
        Debug.Log(sprites);
        if(i < sprites.Length-1)
        {
            i++;
            intervalswith = true;
        }
        else
        {
            SceneLoad();
        }
        
    }
    public void Invok()
    {   
        if(intervalswith == true)
        {
            Invoke("DownLoad", interval);
            Debug.Log("Invok");
            intervalswith = false;
        }
    }

  

    [SerializeField]
    Text text;
    public void DestoryText()
    {
        Destroy(text);
    }

 
}
