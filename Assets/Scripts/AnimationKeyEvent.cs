using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKeyEvent : MonoBehaviour
{
    public void IdleAnim(int zombieNo)
	{
		AnimationContoller.instance.ZombieIdle(zombieNo);
	}
}
