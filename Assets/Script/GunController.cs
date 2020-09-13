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
    //攻撃対象のデータを保持しておく変数
    EnemyDeta m_targetDeta;
    // Start is called before the first frame update
    void Start()
    {
        m_intervalCount = m_interval;
        m_target = null;
        m_targetDeta = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target && m_targetDeta)
        {
            QuickAtack();
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
        if (m_intervalCount <= 0)
        {
            Instantiate(m_bullet, this.gameObject.transform.position, Quaternion.identity);
            m_intervalCount = m_interval;
        }
        else
        {
            m_intervalCount -= Time.deltaTime;
        }
    }

    public void QuickAtack()
    {
        if (m_intervalCount <= 0)
        {
            m_targetDeta.Damage(m_damge);
            Debug.DrawLine(this.gameObject.transform.position, m_target.transform.position, Color.red,1f);
            m_intervalCount = m_interval;
        }
        else
        {
            m_intervalCount -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_target) 
        {
            if (collision.gameObject.tag == "Enemy")
            {
                m_target = collision.gameObject;
                m_targetDeta = m_target.GetComponent<EnemyDeta>();
            } 
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_target == collision.gameObject)
        {
            m_target = null;
            m_targetDeta = null;
        }
    }
}
