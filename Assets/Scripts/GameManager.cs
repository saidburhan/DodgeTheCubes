using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    // bir takım ayar değişkenleri
    private float playerSpeed = 3f;
    private float obstacleSpeed = 1f;
    private int obsNo;
    private int obsReact;
    private int score=0;

    [SerializeField]
    private GameObject[] obsObj;

    [SerializeField]
    private GameObject playerObj, topTarget, bottomTarget;

    [SerializeField]
    private bool isMovingUp = true;



	private void Awake()
	{
        if (instance == null) instance = this;
	}


	void Start()
    {
        obsObj[0].GetComponent<Animator>().SetTrigger("toleft");
    }


    void Update()
    {
		if (Input.GetMouseButtonDown(0)){
            isMovingUp = !isMovingUp;
        }

        if(playerObj.transform.position == bottomTarget.transform.position || playerObj.transform.position == topTarget.transform.position)
		{
            isMovingUp = !isMovingUp;
		}

        MovePlayer();
        Debug.Log(isMovingUp);
    }

    public void MovePlayer()
	{
		if (isMovingUp)
		{
            
            playerObj.transform.position = Vector2.MoveTowards(playerObj.transform.position, topTarget.transform.position, playerSpeed * Time.deltaTime);
            //playerObj.transform.Translate(topTarget.transform.localPosition);
            //playerObj.transform.position = Vector3.Lerp(playerObj.transform.position, topTarget.transform.position, playerSpeed*Time.deltaTime);
        }
		else
		{
            playerObj.transform.position = Vector2.MoveTowards(playerObj.transform.position, bottomTarget.transform.position, playerSpeed * Time.deltaTime);
           // playerObj.transform.Translate(bottomTarget.transform.localPosition);
            //playerObj.transform.position = Vector3.Lerp(playerObj.transform.position, bottomTarget.transform.position, playerSpeed*Time.deltaTime);
        }
	}

    public void StartingSettings()
	{
        isMovingUp = true;
        score = 0;
	}

    public void SetMovingState()
	{
        isMovingUp = !isMovingUp;
	}

    public void SetTargetPosition()
	{
		if (isMovingUp && topTarget.GetComponent<SpriteRenderer>().enabled)
		{
            topTarget.GetComponent<SpriteRenderer>().enabled = false;
            bottomTarget.GetComponent<SpriteRenderer>().enabled = true;
            bottomTarget.transform.position = new Vector2(Random.Range(-1.5f,1.5f),bottomTarget.transform.position.y);
		}
		else if(!isMovingUp && bottomTarget.GetComponent<SpriteRenderer>().enabled)
		{
            bottomTarget.GetComponent<SpriteRenderer>().enabled = false;
            topTarget.GetComponent<SpriteRenderer>().enabled = true;
            topTarget.transform.position = new Vector2(Random.Range(-1.5f, 1.5f), topTarget.transform.position.y);
        }
	}

    public void ObstacleReaction()
	{
       
	}

    public void SingleObs(int no)
	{
        if(obsObj[no].transform.position.x >= 2)
		{
            obsObj[no].GetComponent<Animator>().SetTrigger("toleft");
		}
		else
		{
            obsObj[no].GetComponent<Animator>().SetTrigger("toright");
        }
	}

    public void DoubleObs()
	{

	}

    public void ContiniousObs()  // tıklayıp hemen geri dönsün bazen.. 
	{

	}

    public void SetLevel()
	{
        if (score >= 3 && score < 10)
        {
            obsNo = Random.Range(0,4);
            SingleObs(obsNo);
        }
        else if(score >= 10)
        {
            int secim = Random.Range(0,3);
            if(secim == 0)
			{
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
            }
            else if(secim == 1)
			{
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
            }
            else if(secim == 2)
			{
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);

            }
		}

    }

   



    
}
