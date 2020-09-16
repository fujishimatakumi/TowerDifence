using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapSeter : MonoBehaviour
{
    //falseの場合クリックしても何もしない（セッターとしてのみ使える）
    [SerializeField] bool m_clickActive = false;
    //クリックで設置するトラップオブジェクト
    [SerializeField] GameObject m_trap;
    [SerializeField] float m_distance = 10f;
    [SerializeField] LayerMask m_hitLayer;
    //設置するゲームオブジェクト
    GameObject m_setTrapObj;
    //設置するゲームオブジェクトのデータ
    TrapDeta m_setTrapDeta;
    public SetStatus m_status { get; set; }
    GameManager m_manager;
    Tilemap m_tilemap;
    // Start is called before the first frame update
    void Start()
    {
        m_status = SetStatus.None;
        m_manager = GetComponent<GameManager>();
        m_tilemap = GameObject.FindGameObjectWithTag("Filed").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_clickActive || m_status == SetStatus.None) return;

        switch (m_status)
        {
            case SetStatus.Installation:
                TrapInstallation();
                break;
            case SetStatus.Remove:
                TrapRemove();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 設置するトラップオブジェクトを設定する
    /// </summary>
    /// <param name="trap">設置されるオブジェクト</param>
    public void SetTrapObject(GameObject trap)
    {
        m_setTrapObj = trap;
        m_setTrapDeta = m_setTrapObj.GetComponent<TrapDeta>();
    }
    /// <summary>
    /// 現在設定されているトラップオブジェクトを空にする
    /// </summary>
    public void ResetTrapObject()
    {
        m_setTrapObj = null;
        m_setTrapDeta = null;
    }
    /// <summary>
    /// 左クリックでオブジェクトを置くメソッド
    /// </summary>
    public void TrapInstallation()
    {
        if (m_setTrapObj == null) return;

        
        if (Input.GetButtonDown("Fire1"))
        {
            /*
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 setPos = screenPos + new Vector3(0, 0, 10);
            */
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_distance, m_hitLayer);
            if (hit.collider.gameObject.tag == "Filed")
            {
                if (m_manager.m_resourcePoint >= m_setTrapDeta.m_cost)
                {
                    var tilepos = m_tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    Debug.Log(tilepos);
                    Vector3 setPos = m_tilemap.GetCellCenterWorld(tilepos);
                    Instantiate(m_setTrapObj, setPos, Quaternion.identity);
                    m_manager.SubtractResourcePoint(m_setTrapDeta.m_cost);
                }
            }
        }    
    }
    /// <summary>
    /// 左クリックでオブジェクトを撤去するメソッド
    /// </summary>
    public void TrapRemove()
    {
        if (m_status != SetStatus.Remove)
        {
            m_status = SetStatus.Remove;
        }
        if (Input.GetButtonDown("Fire1"))
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_distance, m_hitLayer);

            if (hit.collider.gameObject.tag == "Trap")
            {
                TrapDeta td = hit.collider.gameObject.GetComponent<TrapDeta>();
                m_manager.AddResourcePoint(td.m_returnCost);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void InstallationMode()
    {
        m_status = SetStatus.Installation;
    }

    public void CheckPoint()
    {
        if (m_manager.m_resourcePoint <= 0)
        {
            m_status = SetStatus.NotHasPoint;
        }
        else
        {
            
        }
    }
}

public enum SetStatus : int
{ 
    /// <summary>設置/// </summary>
    Installation,
    /// <summary>撤去/// </summary>
    Remove,
    /// <summary>何もしない/// </summary>
    None,
    /// <summary>ポイントを持っていない</summary>
    NotHasPoint
}