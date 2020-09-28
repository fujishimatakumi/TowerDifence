using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraterController : MonoBehaviour
{
    [SerializeField] GameObject[] m_generaters;
    [SerializeField] GenerateData[] m_Datars;
    int m_dataIndex = 0;
    int m_generateNum;
    float m_genarateCount;
    float m_generateMagni;
    float m_nextWaitTime;
    [SerializeField] GameObject m_enemy;
    [SerializeField] float m_generateWaiteTime = 3f;
     // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateWaite());   
    }

    public IEnumerator Contorolle()
    {
        while (m_dataIndex < m_Datars.Length)
        {
            GenerateData data = m_Datars[m_dataIndex];
            SetData(data);
            while (m_generateNum > 0)
            {
                if (m_genarateCount <= 0)
                {
                   GameObject go = Instantiate(m_enemy, m_generaters[data.GeneraterIndex].transform.position, Quaternion.identity);
                   EnemyMove em = go.GetComponent<EnemyMove>();
                   em.waypoints = data.Waypoint;
                   em.OnMove();
                   m_genarateCount = m_generateMagni;
                    m_generateNum--;
                }
                else
                {
                    m_genarateCount -= Time.deltaTime;
                }
                yield return null;
            }
           yield return new WaitForSeconds(m_nextWaitTime);
            m_dataIndex++;
        }
    }

    private IEnumerator GenerateWaite()
    {
        while (m_generateWaiteTime > 0)
        {
            m_generateWaiteTime -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(Contorolle());
    }

    private void SetData(GenerateData data)
    {
        m_generateMagni = data.GenerateMargin;
        m_generateNum = data.GenerateNum;
        m_nextWaitTime = data.NextWateTime;
    }
}
