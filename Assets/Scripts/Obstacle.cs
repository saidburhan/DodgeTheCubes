using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int obstacleSpeed = 3;

    private bool moveLeft = true;
    private bool moveRight = false;

    private int randomTime;
    private int randomObstacle;



    [SerializeField]
    private GameObject[] obstacles;

   


    void Update()
    {
        MoveObstacle(randomObstacle);
    }

    void MoveObstacle(int no)
	{
        if(moveLeft)
		{
            Vector2.MoveTowards(obstacles[no].transform.position, new Vector2(-2f, obstacles[no].transform.position.y), obstacleSpeed* Time.deltaTime);
		}
		
        if(moveRight)
		{
            Vector2.MoveTowards(obstacles[no].transform.position, new Vector2(2f, obstacles[no].transform.position.y), obstacleSpeed * Time.deltaTime);
        }
        
    }

    public void ObstacleAktivate()
	{
        randomObstacle = Random.Range(0, 4);


	}


}
