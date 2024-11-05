using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Equipable,//장착가능
    Consumable,//회복가능
    Resource//자원
}

public enum ConsumableType
{
    Health,//체력
    Stamina//스테미나
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;//어떤 회복 템인지
    public float value;//얼만큼 회복할건지
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;//이름
    public string description;//설명
    public ItemType type;//타입
    public Sprite icon;//아이콘
    public GameObject dropPrefab;//오브젝트프리팹

    public int price;

    [Header("Equiping")]
    public bool cnaEquip;//장착가능한
    public GameObject equipPrefab;

    [Header("Stacking")]
    public bool canStack;//여러개 획득 가능한지
    public int maxStackAmount;//몇개까지 획득가능한지

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;//회복템 분류
}
