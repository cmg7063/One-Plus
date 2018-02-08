using UnityEngine;
using System.Collections;

public class FollowingEnemy : MonoBehaviour
{
    public float specifiedDistance;
    public GameObject target;
    public float speed;

    // Use this for initialization
    void Start()
    {
        specifiedDistance = 5.0f;
        target = GameObject.FindGameObjectWithTag("Player");
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= specifiedDistance)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}