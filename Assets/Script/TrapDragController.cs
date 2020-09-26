using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapDragController : MonoBehaviour,IDragHandler,IDropHandler,IBeginDragHandler
{
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
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
