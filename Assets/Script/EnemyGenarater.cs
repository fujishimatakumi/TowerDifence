using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenarater : MonoBehaviour
{
    [SerializeField] GameObject m_enemy;
    [SerializeField] float m_genarateMagni = 2f;
    [field:SerializeField]
    public int enemyCount { get;private set; }
    float m_genarateCount;
    // Start is called before the first frame update
    void Start()
    {
        m_genarateCount = m_genarateMagni;
    }

    // Update is called once per frame
    void Update()
    {
        Genarate();
    }

    private void Genarate()
    {
        if (m_genarateCount <= 0)
        {
            Instantiate(m_enemy, this.gameObject.transform.position, Quaternion.identity);
            m_genarateCount = m_genarateMagni;
        }
        else
        {
            m_genarateCount -= Time.deltaTime;
        }
    }
}
