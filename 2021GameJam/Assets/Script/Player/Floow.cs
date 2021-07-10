using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
///
///<summary>
public class Floow : MonoBehaviour
{
    private Vector3 headPos;
//using UnityEditor.SceneManagement;
 public List<Transform> bodyList = new List<Transform>();//定义身体构成一个List
public GameObject bodyPrefub;
public Sprite[] bodySprites = new Sprite[2];///身体图片构成数组
    public Transform canvas;

    private void Update()
    {
        Move();
    }
    void Move()//定义一个移动方法
{
    headPos = gameObject.transform.localPosition;//保存蛇头移动前的位置
    gameObject.transform.localPosition = new Vector3(headPos.x + transform.position.x ,headPos.y + transform.position.y, headPos.z);
    if (bodyList.Count > 0) //身体的移动，蛇身体挨个从后往前移到前一节点的位置
        {
        for (int i = bodyList.Count - 2; i >= 0; i--)
        {
            bodyList[i + 1].localPosition = bodyList[i].localPosition;
        }
        bodyList[0].localPosition = headPos;
        //bodyList.Last().localPosition = headPos；///蛇屁移到第一个蛇身的位置
        //bodyList.Insert(0, bodyList.Last());///最后一个蛇身变成List里的第一个元素
        //bodyList.RemoveAt(bodyList.Count - 1);/移除最后一个蛇身移动后List空出来的位置
    }
}
void Grow()
{
    int index = (bodyList.Count % 2 == 0) ? 0 : 1;
    GameObject body = Instantiate(bodyPrefub, new Vector3(2000, 2000, 0), Quaternion.identity);//给生成的预制体身体一个位置；
    body.GetComponent<Image>().sprite = bodySprites[index];
    body.transform.SetParent(canvas, false);
    bodyList.Add(body.transform);

}
}
