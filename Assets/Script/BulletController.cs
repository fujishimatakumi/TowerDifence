using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //弾の速度
    [SerializeField] float m_speed = 10f;
    PlayerController m_player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
    }

    public IEnumerator MoveToTarget2D(Vector2 targetPos)
    {
        while (true)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, targetPos,m_speed * Time.deltaTime);
            if (Vector2.Distance(this.gameObject.transform.position,targetPos) < 0.1f)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }

    public void MoveStart(Vector2 targetPos)
    {
        StartCoroutine(MoveToTarget2D(targetPos));
    }
}
