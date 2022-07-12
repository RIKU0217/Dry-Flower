using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C_Fade : MonoBehaviour
{
    [SerializeField, Tooltip("フェードアウトの遷移時間")] private float fadeTime = 2f;

    private Image[] childImage;
    private TextMeshProUGUI[] childText;
    private bool compFadeOut = false;
    private float timer = 0f;

    private void Start()
    {
        //子オブジェクトの指定したコンポーネントのリストを取得
        childImage = gameObject.transform.GetComponentsInChildren<Image>();
        childText = gameObject.transform.GetComponentsInChildren<TextMeshProUGUI>();

        //取得した子のコンポーネントのalphaを0にする
        foreach (Image child in childImage)
        {
            child.color = new Color(child.color.r, child.color.g, child.color.b, 0f);
            child.raycastTarget = false;
        }
        foreach (TextMeshProUGUI child in childText)
        {
            child.color = new Color(child.color.r, child.color.g, child.color.b, 0f);
        }
    }

    private void Update()
    {
        if (!compFadeOut)
        {
            if(timer > fadeTime)
            {
                foreach (Image child in childImage)
                {
                    child.color = new Color(child.color.r, child.color.g, child.color.b, 1f);
                    if (child.GetComponent<Button>())
                    {
                        child.raycastTarget = true; //Buttonコンポーネントを持つならraycastTargetをtrueにする
                    }
                }
                foreach(TextMeshProUGUI child in childText)
                {
                    child.color = new Color(child.color.r, child.color.g, child.color.b, 1f);
                }
                compFadeOut = true;
            }
            else
            {
                foreach (Image child in childImage)
                {
                    child.color = new Color(child.color.r, child.color.g, child.color.b, timer / fadeTime);
                }

                foreach (TextMeshProUGUI child in childText)
                {
                    child.color = new Color(child.color.r, child.color.g, child.color.b, timer / fadeTime);
                }
            }

            timer += Time.deltaTime;
        }
    }
}
