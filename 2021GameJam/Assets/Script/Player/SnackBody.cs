using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class SnackBody : MonoBehaviour
{
    public SnackBody front; //��ʾ ǰһ������
    public Transform self; //��ʾ ��ǰ����
    Vector3 oldPos; //��ǰ�����ǰһ��λ��

    public SnackBody(SnackBody tmpFront, Transform tmpSelf)
    {
        front = tmpFront;
        self = tmpSelf;
        oldPos = tmpSelf.position;
    }

    //��һ�������Ѿ���ǰ�ƶ��ˣ�����oldPos
    public void Reflash()
    {
        oldPos = this.self.position;
    }

    //����ǰ�������
    public void FollowFront()
    {
        self.position = front.oldPos; //���������λ��
        front.Reflash();
    }

    public virtual void MoveForward() { }
}

public class SnackHead : SnackBody
{
    //�̳л���
    public SnackHead(SnackBody tmpFront, Transform tmpSelf) : base(tmpFront, tmpSelf)
    {
    }
    public override void MoveForward()
    {
        self.Translate(-self.forward, Space.World);
    }

    /// <summary>
    /// �����ƶ�
    /// </summary>
    public void TurnRight()
    {
        Vector3 tmpAngle = self.localEulerAngles;
        tmpAngle.y += 90;
        self.localEulerAngles = tmpAngle;
    }

    /// <summary>
    /// �����ƶ�
    /// </summary>
    public void TurnLeft()
    {
        Vector3 tmpAngle = self.localEulerAngles;
        tmpAngle.y -= 90;
        self.localEulerAngles = tmpAngle;
    }

}
