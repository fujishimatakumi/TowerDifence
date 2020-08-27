using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float m_speed = 1f;
    Transform m_moveTarget;
    GameObject m_target;
    [SerializeField]LayerMask m_hitLayer;
    [SerializeField] float m_distance;
    // Start is called before the first frame update
    void Start()
    {
        m_moveTarget = null;
        m_target = null;
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
            Debug.Log("TargetName = " + m_target.name);
            Debug.Log("攻撃中");
        }
    }

    void OnClic()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_distance, m_hitLayer);
        //敵をクリックした場合ターゲットを設定する
        if (hit.collider == null)
        {
            m_target = null;
            m_moveTarget = null;
        }
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
        //それ以外をクリックした場合設定した項目を設定から外す
        
    }
}
