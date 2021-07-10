using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class PlayerController : MonoBehaviour
{
    public static float  speed;
    private bool isForword;
    private bool isLeftword;
    private bool isRightword;

    List<TrainBody> allBodys; //存储所有的车身

    public Transform snackHead;
    public Transform bodyOne;
    public Transform bodyTwo;

    private void Start()
    {
        speed = 10;

        allBodys = new List<TrainBody>();

        SnackHead tmpHead = new SnackHead(null, snackHead);
        allBodys.Add(tmpHead);

       TrainBody tmpOneBody = new TrainBody(tmpHead, bodyOne);
        allBodys.Add(tmpOneBody);

        TrainBody tmpTwoBody = new TrainBody(tmpOneBody, bodyTwo);
        allBodys.Add(tmpTwoBody);

        StartCoroutine(MoveFront());
    }
    private void Update()
    {
        BasicMoveMent();
    }
    void BasicMoveMent()
    {
        if (Input.GetKey(KeyCode.W)&&isLeftword == false&&isRightword ==false)
        {
            this.transform.forward = Vector3.forward;
            this.transform.Translate(-speed*Time.deltaTime,0,0,Space.World);
            isForword = true;
        }
        if (Input.GetKey(KeyCode.A)&&isForword == false&&isRightword == false&&this.transform.forward != Vector3.right)
        {
            this.transform.forward = Vector3.left;
            this.transform.Translate(0,0,-speed*Time.deltaTime,Space.World);
            isLeftword = true;
        }
        if (Input.GetKey(KeyCode.D) && isForword == false && isLeftword == false&&this.transform.forward != Vector3.left)
        {
            this.transform.forward = Vector3.right;
            this.transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
            isRightword = true;
        }
        else
        {
            isForword = false;
            isLeftword = false;
            isRightword = false;
        }
    }//基础移动控制

    IEnumerator MoveFront()
    {
        while(true) //用一个死循环来让车一直移动
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
}
