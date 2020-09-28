using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] string m_wayPointsObject = "WayPoint1";
    public WayPoint waypoints { get; set; }
    public float Speed = 1f;
    public float tmpSpeed = 0;
    int wayPointIndex;

    public void OnMove()
    {
        StartCoroutine(Move(waypoints));
    }

    public IEnumerator Move(WayPoint wayPoint)
    {
        while (wayPointIndex < wayPoint.m_wayPoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints.m_wayPoints[wayPointIndex].position, Speed * Time.deltaTime);

            //実装方法１
            //キャラクターが前を向くが計算式を理解していない
            Vector3 dir = waypoints.m_wayPoints[wayPointIndex].position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            /*
            //実装方法２
            //正規化して関数で向かせる方法
            Vector3 dir = (waypoints.m_wayPoints[wayPointIndex].position - gameObject.transform.position).normalized;
            gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
            */
            if (Vector2.Distance(transform.position, waypoints.m_wayPoints[wayPointIndex].position) < 0.1f)
            {
                if (wayPointIndex < waypoints.m_wayPoints.Length - 1)
                {
                    wayPointIndex++;
                }
                else
                {
                }
            }

            yield return null;
        }
    }
}
