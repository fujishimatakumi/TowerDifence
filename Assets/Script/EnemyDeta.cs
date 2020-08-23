using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// エネミーの基底クラスとなる予定のデータと機能を実装するクラス
/// </summary>
public class EnemyDeta : MonoBehaviour
{
    public int m_hitPoint { get; set; }
    public int m_atackPoint { get; set; }
    public int m_speed { get; set; }
}
