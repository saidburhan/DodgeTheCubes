using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsTargetCollider : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag == "obs")
		{
			Debug.Log("çalıştıııııı");
			collision.transform.Rotate(new Vector3(0, 0, 180));
			if (collision.gameObject.name == "obs1") AnimationContoller.instance.ZombieIdle(1);
			else if (collision.gameObject.name == "obs2") AnimationContoller.instance.ZombieIdle(2);
			else if (collision.gameObject.name == "obs3") AnimationContoller.instance.ZombieIdle(3);
			else if (collision.gameObject.name == "obs4") AnimationContoller.instance.ZombieIdle(4);

		}
	}
}
