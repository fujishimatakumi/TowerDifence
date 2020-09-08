using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeta : MonoBehaviour
{
    [SerializeField] int m_towerHP = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageToTower(int damage)
    {
        m_towerHP -= damage;
        CheckNowHP();
    }
    private void CheckNowHP()
    {
        if (m_towerHP <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        gm.m_status = GameManager.GameStatus.Clear;
    }
}
