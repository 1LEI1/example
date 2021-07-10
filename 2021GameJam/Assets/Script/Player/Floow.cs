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
 public List<Transform> bodyList = new List<Transform>();//�������幹��һ��List
public GameObject bodyPrefub;
public Sprite[] bodySprites = new Sprite[2];///����ͼƬ��������
    public Transform canvas;

    private void Update()
    {
        Move();
    }
    void Move()//����һ���ƶ�����
{
    headPos = gameObject.transform.localPosition;//������ͷ�ƶ�ǰ��λ��
    gameObject.transform.localPosition = new Vector3(headPos.x + transform.position.x ,headPos.y + transform.position.y, headPos.z);
    if (bodyList.Count > 0) //������ƶ��������尤���Ӻ���ǰ�Ƶ�ǰһ�ڵ��λ��
        {
        for (int i = bodyList.Count - 2; i >= 0; i--)
        {
            bodyList[i + 1].localPosition = bodyList[i].localPosition;
        }
        bodyList[0].localPosition = headPos;
        //bodyList.Last().localPosition = headPos��///��ƨ�Ƶ���һ�������λ��
        //bodyList.Insert(0, bodyList.Last());///���һ��������List��ĵ�һ��Ԫ��
        //bodyList.RemoveAt(bodyList.Count - 1);/�Ƴ����һ�������ƶ���List�ճ�����λ��
    }
}
void Grow()
{
    int index = (bodyList.Count % 2 == 0) ? 0 : 1;
    GameObject body = Instantiate(bodyPrefub, new Vector3(2000, 2000, 0), Quaternion.identity);//�����ɵ�Ԥ��������һ��λ�ã�
    body.GetComponent<Image>().sprite = bodySprites[index];
    body.transform.SetParent(canvas, false);
    bodyList.Add(body.transform);

}
}
