using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float monsterSpawnTime;
    public float monsterDamageTime;
    public int monsterDamageRate = 1;
    public int money = 0;
    public int dayCount = 1;
    public TextMeshProUGUI daytext;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public UIInventory inventory;
    public UIInventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public EquipInventory equipInventory;
    public EquipInventory EquipInventory
    {
        get { return equipInventory; }
        set { equipInventory = value; }
    }

    void Awake()
    {
        daytext = GameObject.Find("Day").GetComponent<TextMeshProUGUI>();
        daytext.text = $"{dayCount}Day";
        Time.timeScale = 1;
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


}
