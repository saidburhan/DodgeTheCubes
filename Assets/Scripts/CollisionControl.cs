using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{



	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.transform.tag == "target")
		{
			GameManager.instance.SetTargetPosition();

		}
	}
 



	
}
