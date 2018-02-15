using UnityEngine;
using System.Collections;

public class FollowingEnemy : MonoBehaviour
{
    public GameObject target;
	public float targetMaxDistance;

	public GameObject hive;
	public float hiveMaxDistance;
	public bool returnHome;

    public float speed;

	private SpriteRenderer SpriteRend;

    // Use this for initialization
	void Start() {
		target = GameObject.FindGameObjectWithTag("Player");
		targetMaxDistance = 5.0f;

		hiveMaxDistance = 7.5f;
		returnHome = false;

        speed = 1;

		SpriteRend = gameObject.GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update() {
		var targetDistance = Vector2.Distance(target.transform.position, transform.position);
        var hiveDistance = Vector2.Distance(hive.transform.position, transform.position);

		// if returnHome is false chase player when in distance
		if (!returnHome) {
			// move towards target if in distance and is within hive distance
			if (targetDistance <= targetMaxDistance && hiveDistance <= hiveMaxDistance) {
				SpriteRend.color = new Color (1f, 1f, 1f, 1f);
				Follow (target);
			} else if (targetDistance > targetMaxDistance && hiveDistance <= 0.25f) {
				// when target is out of range and close to hive
				// reset pos to hive pos and set alpha to 0
				transform.position = hive.transform.position;
				SpriteRend.color = new Color (1f, 1f, 1f, 0f);
			} else if (hiveDistance > hiveMaxDistance) {
				// set returnHome true when hiveMaxDistance is reached
				returnHome = true;
			} else {
				// return back to hive
				Follow (hive);
			}
		} else {
			// turn returnHome false once its 50% of hiveMaxDistance
			if (hiveDistance <= hiveMaxDistance / 2) {
				returnHome = false;
			}

			Follow (hive);
		}
    }

	// follow the targeted object
	private void Follow(GameObject targetObj) {
		Vector3 dir = targetObj.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}