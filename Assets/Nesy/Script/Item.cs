using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{

    [SerializeField]
    private string itemName;

    public string GetItemName()
    {
        return itemName;
    }

    [SerializeField]
    private Sprite icon;

    public Sprite GetIcon()
    {
        return icon;
    }
}
