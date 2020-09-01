using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの移動速度
    float m_speed = 1f;
    //プレイヤーが移動する位置
    Transform m_moveTarget;
    //攻撃対象
    public GameObject m_target { get; set; }
    [SerializeField] LayerMask m_hitLayer;
    //レイを飛ばす距離
    [SerializeField] float m_distance;
    //攻撃の間隔
    [SerializeField] float m_attackTime = 2f;
    //弾となるプレハブ
    [SerializeField] GameObject m_shotPrefab;
    float m_attackTimeCount;
    // Start is called before the first frame update
    void Start()
    {
        m_moveTarget = null;
        m_target = null;
        m_attackTimeCount = m_attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnClic();
        }
        if (m_moveTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_moveTarget.transform.position, m_speed * Time.deltaTime);
        }
        if (m_target)
        {
            if (m_attackTimeCount <= 0)
            {
                EnemyShot();
                m_attackTimeCount = m_attackTime;
            }
            else
            {
                m_attackTimeCount -= Time.deltaTime;
            }
            Debug.Log("TargetName = " + m_target.name);
            Debug.Log("攻撃中");
        }
        //ターゲットが外れた場合発射間隔カウントを初期化する
        else
        {
            m_attackTimeCount = m_attackTime;
        }
    }

    void OnClic()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_distance, m_hitLayer);
        //それ以外をクリックした場合設定した項目を設定から外す
        if (hit.collider == null)
        {
            m_target = null;
            m_moveTarget = null;
        }
        //敵をクリックした場合ターゲットを設定する
        else if (hit.collider.tag == "Enemy")
        {
            m_target = hit.collider.gameObject;
        }
        //フィールドをクリックした場合クリックしたフィールドの場所を設定する
        else if (hit.collider.tag == "Filed")
        {
            m_moveTarget = hit.collider.gameObject.transform;
            if (m_target)
            {
                m_target = null;
            }
        }
    }
    void EnemyShot()
    {
        Instantiate(m_shotPrefab, this.gameObject.transform.position, Quaternion.identity);
    }
}
