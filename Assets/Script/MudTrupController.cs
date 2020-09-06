using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTrupController : MonoBehaviour
{
    //減速させる割合
    [SerializeField] float m_DecelerationMag = 2f;
    EnemyMove m_em;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        m_em = go.gameObject.GetComponent<EnemyMove>();
        if (m_em)
        {
            m_em.Speed = m_em.Speed / m_DecelerationMag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_em)
        {
            m_em.Speed = m_em.Speed * m_DecelerationMag;
        }
        m_em = null;
    }
}
