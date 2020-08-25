using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //アイテムを購入する際に消費するコスト
    [SerializeField] int m_itemCost = 20;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameManager");
        gm = go.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        gm.SubtractResourcePoint(m_itemCost);
    }

    public void ReturnItem()
    {
        gm.AddResourcePoint(m_itemCost);
    }
}
