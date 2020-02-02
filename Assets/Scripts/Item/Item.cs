using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Agua, Adubo, Cal }
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int itemID;
    public string name;
    public ItemType thisItem;
    public Sprite image;
}

