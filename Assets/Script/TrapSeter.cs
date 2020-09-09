using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrapSeter : MonoBehaviour
{
    //falseの場合クリックしても何もしない（セッターとしてのみ使える）
    [SerializeField] bool m_clickActive = false;
    //クリックで設置するトラップオブジェクト
    [SerializeField] GameObject m_trap;
    [SerializeField] float m_distance = 10f;
    [SerializeField] LayerMask m_hitLayer;
    GameObject m_setTrapObj;
    SetStatus m_status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_clickActive || m_setTrapObj == null) return;

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
    }
    /// <summary>
    /// 現在設定されているトラップオブジェクトを空にする
    /// </summary>
    public void ResetTrapObject()
    {
        m_setTrapObj = null;
    }
    /// <summary>
    /// 左クリックでオブジェクトを置くメソッド
    /// </summary>
    public void TrapInstallation()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 setPos = screenPos + new Vector3(0, 0, 10);
            Instantiate(m_setTrapObj, setPos, Quaternion.identity);
        }    
    }
    /// <summary>
    /// 左クリックでオブジェクトを撤去するメソッド
    /// </summary>
    public void TrapRemove()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, m_distance, m_hitLayer);

            if (hit.collider.gameObject.tag == "Trap")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}

public enum SetStatus : int
{ 
    /// <summary>設置/// </summary>
    Installation,
    /// <summary>撤去/// </summary>
    Remove

}