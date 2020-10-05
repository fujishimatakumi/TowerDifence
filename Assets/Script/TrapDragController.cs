using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TrapDragController : MonoBehaviour,IDragHandler,IDropHandler,IBeginDragHandler
{
    [SerializeField] float m_distance = 10f;
    [SerializeField] LayerMask m_hitLayer;
    [SerializeField] GameObject m_setObject;
    [SerializeField] AudioClip m_setSE;
    [SerializeField] GameObject m_setObjectImage;
    GameManager m_gameManager;
    TrapDeta m_objectData;
    Tilemap m_tilemap;
    AudioSource m_source;
    

    private void Start()
    {
        m_source = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        m_gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        m_objectData = m_setObject.GetComponent<TrapDeta>();
        m_tilemap = GameObject.FindGameObjectWithTag("setField").GetComponent<Tilemap>();
        StartCoroutine(CheckClicke());
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_setObjectImage.SetActive(true);
        m_setObjectImage.transform.localPosition = this.gameObject.transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_setObjectImage.transform.position = eventData.position;
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        TrapInstallation();
        m_source.PlayOneShot(m_setSE);
        m_setObjectImage.SetActive(false);
    }

    public void TrapInstallation()
    {       
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

    private IEnumerator CheckClicke()
    {
        while (true)
        {
            EnableTrapUI();
            yield return null;
        }
    }

    private void EnableTrapUI()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, m_distance, m_hitLayer);

            foreach (var item in hits)
            {
                if (item.collider.gameObject.tag == "Trap")
                {
                    TrapDeta td = item.collider.gameObject.GetComponent<TrapDeta>();
                    td.EnableCanvas();
                }
            }
        }
    }

}
