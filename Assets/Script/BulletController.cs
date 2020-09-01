using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //弾の速度
    [SerializeField] float m_speed = 10f;
    PlayerController m_player;
    Transform m_target;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_target = m_player.m_target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position =  Vector2.MoveTowards(this.gameObject.transform.position, m_target.transform.position, m_speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
    }
}
