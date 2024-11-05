using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Equipable,//��������
    Consumable,//ȸ������
    Resource//�ڿ�
}

public enum ConsumableType
{
    Health,//ü��
    Stamina//���׹̳�
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;//� ȸ�� ������
    public float value;//��ŭ ȸ���Ұ���
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;//�̸�
    public string description;//����
    public ItemType type;//Ÿ��
    public Sprite icon;//������
    public GameObject dropPrefab;//������Ʈ������

    public int price;

    [Header("Equiping")]
    public bool cnaEquip;//����������
    public GameObject equipPrefab;

    [Header("Stacking")]
    public bool canStack;//������ ȹ�� ��������
    public int maxStackAmount;//����� ȹ�氡������

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;//ȸ���� �з�
}
