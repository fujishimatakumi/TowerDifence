using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    AudioSource m_source;
    [SerializeField] AudioClip m_shotSE;
    bool m_farstAtack;
    // Start is called before the first frame update
    void Start()
    {
        m_farstAtack = true;
        m_intervalCount = m_interval;
        m_target = null;
        m_targetDeta = null;
        m_source = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        LockAtField();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_target && m_targetDeta)
        {
            QuickAtack();
            LockAtTarget();
        }
        else
        {
            m_farstAtack = true;
        }

    }
    
    public void QuickAtack()
    {
        if (m_intervalCount <= 0 || m_farstAtack)
        {
            m_targetDeta.Damage(m_damge);
            GameObject bullet =  Instantiate(m_bullet,this.gameObject.transform.position,Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.MoveStart(m_target.transform.position);
            m_source.PlayOneShot(m_shotSE);
            m_intervalCount = m_interval;
            m_farstAtack = false;
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

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if (m_target)
        {
            return;
        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
            {
                m_target = collision.gameObject;
                m_targetDeta = m_target.GetComponent<EnemyDeta>();
            }
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



    private void LockAtTarget()
    {
        Vector3 dir = Vector3.Normalize(m_target.transform.position - this.gameObject.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private void LockAtField()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Generater");
        GameObject minDistanceObject = gos[0];
        float minDistance = Vector3.Distance(this.gameObject.transform.position, gos[0].transform.position);
        foreach (var item in gos)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, item.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                minDistanceObject = item;
            }
        }

        Vector3 dir = minDistanceObject.transform.position - this.gameObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
}
