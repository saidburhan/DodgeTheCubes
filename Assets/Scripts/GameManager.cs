using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // engellerin burada olmasından emin değilim.. 
    // şimdilik animatörlerle prototipleyeyim onları daha sonra kodlarla bi çaresine bakarız performansa göree...

    // bir takım ayar değişkenleri
    private float playerSpeed = 3f;
   // private float obstacleSpeed = 1f;
    private float obsInterval; // İki obs arası harekette geçecek olan süre.. random olacak..

    private int obsNo; // harekete geçireceğim engelin indisi olsun
   // private int obsReact; // engelin hareket tarzı..
    private int score=0; 

    [SerializeField]
    private GameObject[] obsObj;

    [SerializeField]
    private GameObject playerObj, topTarget, bottomTarget;

    [SerializeField]
    private Transform [] obsStartPos;

    [SerializeField]
    private Vector2[] obsStartPosition;

    private bool isMovingUp = false;
    private bool isGameOver = false;





	private void Awake()
	{
        if (instance == null) instance = this;
	}


	void Start()
    {
        StartCoroutine(StartingSettings());

        //for (int i = 0; i < obsObj.Length; i++) // oyunun ilk açılışında start pozisyonlarını kaydediyorum kii daha sonra restart gamelerde işe yarar
        //{
        //    obsStartPosition[i] = obsObj[i].transform.position;
        //}

    }


    void Update()
    {
		if (!isGameOver)
		{
            if (Input.GetMouseButtonDown(0))
            {
                isMovingUp = !isMovingUp;
            }
            // hedefe ulaşınca diğer hedefe yönlendirmek için boolu değiştirelim.
            if (playerObj.transform.position == bottomTarget.transform.position || playerObj.transform.position == topTarget.transform.position)
            {
                isMovingUp = !isMovingUp;
            }

            MovePlayer();
        }
		
    }

    public void MovePlayer()
	{
		if (isMovingUp)
		{
            
            Vector3 relativePos = topTarget.transform.position - playerObj.transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, q, 1f);
			playerObj.transform.position = Vector2.MoveTowards(playerObj.transform.position, topTarget.transform.position, playerSpeed * Time.deltaTime);
            
        }
		else
		{
            Vector3 relativePos = bottomTarget.transform.position - playerObj.transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, q, 1f);
            playerObj.transform.position = Vector2.MoveTowards(playerObj.transform.position, bottomTarget.transform.position, playerSpeed * Time.deltaTime);
        }
	}

    public void StartGame()
	{
        StartCoroutine(StartingSettings());
	}

    public IEnumerator StartingSettings()
	{
        topTarget.GetComponent<SpriteRenderer>().enabled = true;
        bottomTarget.GetComponent<SpriteRenderer>().enabled = false;
        playerObj.transform.position = new Vector2(0f, -1.7f);
        score = 0;
        StartCoroutine(SetObstacle());
        for (int i = 0; i < obsObj.Length; i++)
        {
            obsObj[i].transform.position = obsStartPosition[i];
            obsObj[i].GetComponent<Animator>().enabled = true;
        }
        yield return new WaitForSeconds(0.8f);
        playerObj.GetComponent<CircleCollider2D>().enabled = true;
        isGameOver = false;
        isMovingUp = true;
    }

    public void SetMovingState()
	{
        isMovingUp = !isMovingUp;
	}

    public void GameOver()
	{
        isGameOver = true;
        playerObj.GetComponent<CircleCollider2D>().enabled = false;
        for(int i = 0; i < obsObj.Length; i++)
		{
            obsObj[i].GetComponent<Animator>().SetTrigger("reset");
            obsObj[i].GetComponent<Animator>().enabled = false;
		}
        StopAllCoroutines();
	}

    public void SetTargetPosition()
	{
		if (isMovingUp && topTarget.GetComponent<SpriteRenderer>().enabled)
		{
            topTarget.GetComponent<SpriteRenderer>().enabled = false;
            bottomTarget.GetComponent<SpriteRenderer>().enabled = true;
            bottomTarget.transform.position = new Vector2(Random.Range(-1.5f,1.5f),bottomTarget.transform.position.y);
            score++;
            UiControl.instance.SetScoreText(score);
		}
		else if(!isMovingUp && bottomTarget.GetComponent<SpriteRenderer>().enabled)
		{
            bottomTarget.GetComponent<SpriteRenderer>().enabled = false;
            topTarget.GetComponent<SpriteRenderer>().enabled = true;
            topTarget.transform.position = new Vector2(Random.Range(-1.5f, 1.5f), topTarget.transform.position.y);
            score++;
            UiControl.instance.SetScoreText(score);
        }
	}

    public void SingleObs(int no)
	{
        if(obsObj[no].transform.position.x == 2)
		{
            obsObj[no].GetComponent<Animator>().SetTrigger("toleft");
		}
		else if(obsObj[no].transform.position.x == -2)
		{
            obsObj[no].GetComponent<Animator>().SetTrigger("toright");
        }
	}


    // bunları animatörle değil kodlarla yapsak daha kolay olacak yönetmesiiii.... 
    private IEnumerator SetObstacle()
	{

        Debug.Log("<color=green> coroutine çalışıyor </color>");
        if (score >= 3 && score < 10)
        {
            obsNo = Random.Range(0, 4);
            SingleObs(obsNo);
            obsInterval = Random.Range(1.3f,3f);
            yield return new WaitForSeconds(obsInterval);
            StartCoroutine(SetObstacle());
        }
        else if (score >= 10)
        {
            int secim = Random.Range(0, 3);  // burada ihtimalleri biraz daha matematiksel yapalım..
            if (secim == 0)  // tek engel hareketi olsun.. 
            {
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
                obsInterval = Random.Range(1.3f, 3f);
                yield return new WaitForSeconds(obsInterval);
                Debug.Log("<color=green>secim = " + secim + "</color>" );
            }
            else if (secim == 1) // tek engelin karşıya gidip hemen geri dönmesi.. 
            {
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
                yield return new WaitForSeconds(1.3f); // bu 1f yetmezse artıracaz.. animatörün bok yemesi işte bunlar.. kodlaaaa bunlarrııııı
                SingleObs(obsNo);
                obsInterval = Random.Range(1.3f, 3f);
                yield return new WaitForSeconds(obsInterval);
                Debug.Log("<color=green>secim = " + secim + "</color>");
            }
            else if (secim == 2) // iki engelin aralarında çok az bir süre ile nerdeyse aynı anda hareket etmesi. burada farklı engeller olmasını garantileyelim..
            {
                obsNo = Random.Range(0, 4);
                SingleObs(obsNo);
                yield return new WaitForSeconds(0.5f);
                obsNo = (obsNo + 1) % 4; // farklı olmasını bu şekilde sistematik olarak garantiledik.. 0-1 / 1-2 / 2-3/ 3-0  gerekirse buna da 1 ekleyip aralıklı olmasını sağlarız.. 
                SingleObs(obsNo);
                obsInterval = Random.Range(1.3f, 3f);
                yield return new WaitForSeconds(obsInterval);
                Debug.Log("<color=green>secim = " + secim + "</color>");
            }
            StartCoroutine(SetObstacle());
        }
		else
		{
            yield return new WaitForSeconds(1.25f);
            StartCoroutine(SetObstacle());
        }

        

    }



}

// seviye ilerleyince hareketler hızlanıp aralara bırakılan boşluklar kısalır.. zorlaşma böyle sağlanırr.. 
// sürelere değişken atarız kontrol o şekilde sağlanır..
