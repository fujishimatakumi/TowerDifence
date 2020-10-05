using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyGenarater : MonoBehaviour
{
    [SerializeField] WayPoint m_wayPoint;
    [SerializeField] GameObject m_enemy;
    float m_generateMagni;
    float m_generateCount;
    int m_generateNum;
    GeneraterController m_Gcontroller;

    // Start is called before the first frame update
    void Start()
    {
        m_Gcontroller = GameObject.FindGameObjectWithTag("GContr").GetComponent<GeneraterController>();
    }

    // Update is called once per frame
    

    public void StartGenarate(GenerateData setData)
    {
        SetData(setData);
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        while (m_generateNum > 0)
        {
            if (m_generateCount <= 0)
            {
                GameObject go = Instantiate(m_enemy, this.gameObject.transform.position, Quaternion.identity);
                go.transform.SetParent(this.gameObject.transform);
                EnemyMove em = go.GetComponent<EnemyMove>();
                em.waypoints = m_wayPoint;
                em.OnMove();
                m_generateCount = m_generateMagni;
                m_generateNum--;
            }
            else
            {
                m_generateCount -= Time.deltaTime;
            }
            yield return null;
        }
    }

    public void CheckChiled()
    {
        if (this.gameObject.transform.childCount - 1 <= 0)
        {
            m_Gcontroller.EndGenerate();
        }
    }

    private void SetData(GenerateData data)
    {
        m_generateMagni = data.GenerateMargin;
        m_generateNum = data.GenerateNum;
    }


}
