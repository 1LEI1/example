using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class Follow : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Move_Forward();
        Move_Left();
        Move_Back();
        Move_Right();
        ///
        TrailMove();
    }

    #region 蛇头移动
    public float Speed;

    private void Move_Forward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }

    private void Move_Back()
    {
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }
    }

    private void Move_Left()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }

    private void Move_Right()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }

    #endregion

  //蛇身跟随
  [Tooltip("跟随的物体")]
    public Transform[] Trails;//拖尾
    [Tooltip("间距")]
    public float Distance;//蛇身之间的距离

    private List<Vector3> PlayerPosList;

    private Vector3 LastPos;
    private Vector3 playerPos;
    private int MaxListNumebr = 100;//最大列表数量
    private int ListNumebr = 0;//当前列表数量

    /// <summary>
    /// 移动
    /// </summary>
    private void TrailMove()
    {
        if (PlayerPosList == null) PlayerPosList = new List<Vector3>();

        playerPos = this.transform.position;

        if (playerPos != LastPos)
        {
            if (PlayerPosList.Count > MaxListNumebr)
            {
                if (ListNumebr > MaxListNumebr)
                {
                    ListNumebr = 0;
                }
                PlayerPosList[ListNumebr] = playerPos;
                LastPos = playerPos;
                ListNumebr += 1;
            }
            else
            {
                //在列表中添加蛇头移动位置信息
                PlayerPosList.Add(playerPos);
                LastPos = playerPos;
            }
        }
        for (int i = 0; i < Trails.Length; i++)
        {
            TrailItemMove(i);
        }
    }

    private List<int> N_count;
    private Vector3 dis;

    /// <summary>
    /// 尾巴跟随
    /// </summary>
    /// <param name="number"></param>
    private void TrailItemMove(int number)
    {
        if (N_count == null)
        {
            N_count = new List<int>();
            for (int i = 0; i < Trails.Length; i++)
            {
                N_count.Add(i);
                N_count[i] = 0;
            }
        }
        if (number < 1)
        {
            dis = Trails[number].position - transform.position;
            if (dis.z > Distance || dis.x > Distance || (Mathf.Abs(dis.z) + Mathf.Abs(dis.x)) > Distance)
            {
                if (N_count[number] > MaxListNumebr)
                {
                    N_count[number] = 0;
                }
                Trails[number].position = PlayerPosList[N_count[number]];
                N_count[number] += 1;
            }
        }
        else
        {
            dis = Trails[number].position - Trails[number - 1].position;
            if (dis.z > Distance || dis.x > Distance || (Mathf.Abs(dis.z) + Mathf.Abs(dis.x)) > Distance)
            {
                if (N_count[number] > MaxListNumebr)
                {
                    N_count[number] = 0;
                }

                Trails[number].position = PlayerPosList[N_count[number]];
                N_count[number] += 1;
            }
        }
    }
}