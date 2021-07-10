using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class SnackController : MonoBehaviour
{
    List<SnackBody> allBodys; //存储所有的蛇身

    public Transform snackHead;
    public Transform bodyOne;
    public Transform bodyTwo;

    // Use this for initialization
    void Start()
    {

        allBodys = new List<SnackBody>();

        SnackHead tmpHead = new SnackHead(null, snackHead);
        allBodys.Add(tmpHead);

        SnackBody tmpOneBody = new SnackBody(tmpHead, bodyOne);
        allBodys.Add(tmpOneBody);

        SnackBody tmpTwoBody = new SnackBody(tmpOneBody, bodyTwo);
        allBodys.Add(tmpTwoBody);

        StartCoroutine(MoveFront());
    }

    IEnumerator MoveFront()
    {
        while (true) //用一个死循环来让蛇一直移动
        {
            allBodys[0].MoveForward();

            yield return new WaitForSeconds(0.5f);

            for (int i = 1; i < allBodys.Count; i++)
            {
                allBodys[i].FollowFront();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            ((SnackHead)allBodys[0]).TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ((SnackHead)allBodys[0]).TurnRight();
        }
    }
}


