using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TrapDragController : MonoBehaviour,IDragHandler,IDropHandler,IBeginDragHandler
{
    [SerializeField] float m_distance = 10f;
    [SerializeField] LayerMask m_hitLayer;
    [SerializeField] GameObject m_setObject;
    GameManager m_gameManager;
    TrapDeta m_objectData;
    Tilemap m_tilemap;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        m_objectData = m_setObject.GetComponent<TrapDeta>();
        m_tilemap = GameObject.FindGameObjectWithTag("setField").GetComponent<Tilemap>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransform canvasTranse = GameObject.Find("Canvas").GetComponent<RectTransform>();
        GameObject go = GameObject.Instantiate(this.gameObject) as GameObject;
        go.transform.SetParent(canvasTranse);
        go.transform.localPosition = this.gameObject.transform.localPosition;
        go.transform.localScale = this.gameObject.transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    { 
        this.gameObject.transform.position = eventData.position;
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        TrapInstallation();
        Destroy(this.gameObject);
    }

    public void TrapInstallation()
    {
        
            /*
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 setPos = screenPos + new Vector3(0, 0, 10);
            */
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, m_distance, m_hitLayer);
        
        foreach (var item in hits)
        {
            if (item.collider.gameObject.tag == "Trap") return;
            if (item.collider.gameObject.tag == "setField")
            {
                if (m_gameManager.m_resourcePoint >= m_objectData.m_cost)
                {
                    var tilepos = m_tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    Debug.Log(tilepos);
                    Vector3 setPos = m_tilemap.GetCellCenterWorld(tilepos);
                    Instantiate(m_setObject, setPos, Quaternion.identity);
                    m_gameManager.SubtractResourcePoint(m_objectData.m_cost);
                }
            }
        }
            
        
    }

}
