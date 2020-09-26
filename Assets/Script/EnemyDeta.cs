using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エネミーの機能を実装
/// </summary>
public class EnemyDeta : MonoBehaviour
{
    [field:SerializeField]
    public int m_hitPoint { get;private set; }
    [field:SerializeField]
    public int m_atackPoint { get; set; }

    Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Tower":
                GameObject go = GameObject.FindGameObjectWithTag("Tower");
                TowerDeta td = go.GetComponent<TowerDeta>();
                td.DamageToTower(m_atackPoint);
                GameObject go1 = GameObject.FindGameObjectWithTag("Manager");
                GameManager gm = go1.GetComponent<GameManager>();
                gm.DecreceEnemy();
                gm.GetTowerHP();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    private void CheckHP()
    {
        if (m_hitPoint <= 0)
        {
            EnemyDstroy();
        }
    }

    public void Damage(int damege)
    {
        m_hitPoint -= damege;
        CheckHP();
        OnDamageAnim();
    }

    public void OnDamageAnim()
    {
        m_animator.SetTrigger("Damage");
    }

    public void EnemyDstroy()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Manager");
        GameManager gm = go.GetComponent<GameManager>();
        gm.DecreceEnemy();
        Destroy(this.gameObject);
    }
}
