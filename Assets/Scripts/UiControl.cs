using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : MonoBehaviour
{
	public static UiControl instance;

	[SerializeField]
	private Text scoreText;


	private void Awake()
	{
		if (instance == null) instance = this;
	}


	public void SetScoreText(int score)
	{
		scoreText.text = score.ToString();
	}
}
