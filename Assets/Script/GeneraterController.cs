using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraterController : MonoBehaviour
{
    [SerializeField] GameObject[] m_generaters;
    [SerializeField] RoundGenerateData[] m_roundDatas;
    int m_dataIndex = 0;
    float m_nextWaitTime;
    [SerializeField] float m_generateWaiteTime = 3f;
    int m_endFlug;
    int m_endFlugCount;
     // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateWaite());   
    }

    public IEnumerator Contorolle()
    {
        while (m_dataIndex < m_roundDatas.Length)
        {   
                RoundGenerateData roundData = m_roundDatas[m_dataIndex];
                m_nextWaitTime = roundData.NextWaiteTime;
                GenerateData[] datas = roundData.m_generatDatas;
                m_endFlug = datas.Length;
                for (int i = 0; i < datas.Length; i++)
                {
                    GenerateData data = datas[i];
                    EnemyGenarater generater = m_generaters[data.GeneraterIndex].GetComponent<EnemyGenarater>();
                    generater.StartGenarate(data);
                }
            while (m_endFlug != m_endFlugCount)
            {
                yield return null;
            }
            yield return new WaitForSeconds(m_nextWaitTime);
            m_endFlugCount = 0;
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

    public void EndGenerate()
    {
        m_endFlugCount++;
    }

    public int GetEnemyNum()
    {
        int enemyNum = 0;
        for (int i = 0; i < m_roundDatas.Length; i++)
        {   
            for (int j = 0; j < m_roundDatas[i].m_generatDatas.Length; j++)
            {
                enemyNum += m_roundDatas[i].m_generatDatas[j].GenerateNum;
            }
        }

        return enemyNum;
    }

    public void StopGenerate()
    {
        StopCoroutine(Contorolle());
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i]);
        }
    }
}
