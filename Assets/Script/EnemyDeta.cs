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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Tower":
                GameObject go = GameObject.FindGameObjectWithTag("Tower");
                TowerDeta td = go.GetComponent<TowerDeta>();
                td.DamageToTower(m_atackPoint);
                break;
            default:
                break;
        }
    }

    public void Damage(int damege)
    {
        m_hitPoint -= damege;
    }
}
