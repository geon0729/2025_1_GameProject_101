using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //전역 변수 (모든 공이 공유해서 사용 할 수 있음)
    public static bool canPlay = true;
    public static bool anyBallMoveing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAllBalls();
        
        if(!anyBallMoveing && !canPlay)
        {
            canPlay = true;
            Debug.Log("턴 종료! 다시 칠 수 있습니다.");
        }
    }

    void CheckAllBalls()
    {
        SimpleBallController[] allBalls = FindObjectsOfType<SimpleBallController>();
        anyBallMoveing = false;

        foreach(SimpleBallController ball in allBalls)
        {
            if(ball.IsMoving())
            {
                anyBallMoveing = true;
                break;
            }
        }
    }

    public static void OnBallHit()
    {
        canPlay = false;
        anyBallMoveing = true;
        Debug.Log("턴 시작! 공이 멈출 때 까지 기다리세요.");
    }
}
