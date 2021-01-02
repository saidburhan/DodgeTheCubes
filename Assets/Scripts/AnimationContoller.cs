using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationContoller : MonoBehaviour
{
    public static AnimationContoller instance;
	// girişte f / m kontrolü yapılacak.. bu scriptte yapmak daha mantıklı sanırım.. beğenmezsek değiştiriz..

	private bool isMale;  // 1 erkek 0 kız olsun.. 

    [SerializeField]
    private Animator playerAnim,topTargetAnim,bottomTargetAnim,topHeartAnim,bottomHeartAnim,
		zombie1Anim, zombie2Anim,zombie3Anim,zombie4Anim;  // target karşıda bekleyen şahsiyet olacak artık kız veya erkek şimdi onları ayarlayalım..


	private void Awake()
	{
		if (instance == null) instance = this;
	}

	private void Start()
	{
		PlayerPrefs.SetInt("gender", 1);
		if (PlayerPrefs.GetInt("gender") == 1) isMale = true;
		else isMale = false;
	}

	public void SetGender(int gender) // cinsiyet seçimi ekranındaki ui butonlarla parametre göndertelim.. 1 olursa erkek 0 olursa kız..
	{
		if(gender == 1)
		{
			isMale = true;
			PlayerPrefs.SetInt("gender", 1);
		}else if (gender == 0)
		{
			isMale = false;
			PlayerPrefs.SetInt("gender", 0);
		}
	}

	public bool GetGender() // true dönerse erkek false dönerse kız olsun. 
	{
		if (PlayerPrefs.GetInt("gender") == 1) isMale = true;
		else isMale = false;

		return isMale;
	}


	public void TargetClap()
	{
		if (isMale)// player erkekse alkışlayan taraf kızdır.. yani inşallah :d
		{
			topTargetAnim.SetTrigger("fclap");
			bottomTargetAnim.SetTrigger("fclap");
		}
		else
		{
			topTargetAnim.SetTrigger("mclap");
			bottomTargetAnim.SetTrigger("mclap");
		}
	}

	public void TargetIdle()
	{
		if (isMale)
		{
			topTargetAnim.SetTrigger("fidle");
			bottomTargetAnim.SetTrigger("fidle");
		}
		else
		{
			topTargetAnim.SetTrigger("midle");
			bottomTargetAnim.SetTrigger("midle");
		}
	}

	public void PlayerIdleAnim()
	{
		if (isMale)
		{
			playerAnim.SetTrigger("midle");
		}
		else
		{
			playerAnim.SetTrigger("fidle");
		}
	}

	public void PlayerRunAnim()
	{
		if (isMale)
		{
			playerAnim.SetTrigger("mrun");
		}
		else
		{
			playerAnim.SetTrigger("frun");
		}
	}


	public void NormalHeartAnim()
	{
		topHeartAnim.SetTrigger("normal");
		bottomHeartAnim.SetTrigger("normal");
	}

	public void BreakingHeartAnim()
	{
		topHeartAnim.SetTrigger("break");
		bottomHeartAnim.SetTrigger("break");
	}

	public void ZombieWalk(int zombiNo)
	{
		if (zombiNo == 1) zombie1Anim.SetTrigger("walk");
		else if (zombiNo == 2) zombie2Anim.SetTrigger("walk");
		else if (zombiNo == 3) zombie3Anim.SetTrigger("walk");
		else if (zombiNo == 4) zombie4Anim.SetTrigger("walk");

	}

	public void ZombieIdle(int zombiNo)
	{
		if (zombiNo == 1) zombie1Anim.SetTrigger("idle");
		else if (zombiNo == 2) zombie2Anim.SetTrigger("idle");
		else if (zombiNo == 3) zombie3Anim.SetTrigger("idle");
		else if (zombiNo == 4) zombie4Anim.SetTrigger("idle");

	}

}
