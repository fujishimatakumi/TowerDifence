using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーに必要なデータと機能を持つ基底クラスになる予定のクラス
/// </summary>
public class PlayerDeta : MonoBehaviour
{
    public int m_hitPoint { get; set; } = 50;
    public int m_atackPoint { get; set; } = 5;
    public int m_speedPoint { get; set; } = 10;
}
