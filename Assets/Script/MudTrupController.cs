using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTrupController : MonoBehaviour
{
    //減速させる割合
    [SerializeField] float m_DecelerationMag = 2f;
    
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
        EnemyMove em = go.gameObject.GetComponent<EnemyMove>();
        if (em)
        {
            em.tmpSpeed = em.Speed;
            em.Speed = em.Speed / m_DecelerationMag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyMove em = collision.gameObject.GetComponent<EnemyMove>();
        if (em)
        {
            em.Speed = em.tmpSpeed;
        }
        
    }
}
