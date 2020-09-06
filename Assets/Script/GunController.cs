using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class GunController : MonoBehaviour
{
    //発射するオブジェクト
    [SerializeField] GameObject m_bullet;
    //敵に与えるダメージ
    [SerializeField] int m_damge = 10;
    //射撃間隔
    [SerializeField] float m_interval = 1f;
    float m_intervalCount;
    //攻撃対象
    GameObject m_target;
    // Start is called before the first frame update
    void Start()
    {
        SetTaget();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null)
        {
            SetTaget();
        }
        else
        {
            Atack();
        }
    }

    //シーンからエネミーを見つけてくる
    public void SetTaget()
    {
        m_target = GameObject.FindGameObjectWithTag("Enemy");
    }
    //弾となるオブジェクトを生成する
    public void Atack()
    {
        Instantiate(m_bullet, this.gameObject.transform.position, Quaternion.identity);
    }
}
