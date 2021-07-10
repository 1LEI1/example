using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class TrainBody : MonoBehaviour
{
    public TrainBody front; //表示 前一段车身
    public Transform self; //表示 当前车身
    Vector3 oldPos; //当前车身的前一个位置

    public TrainBody(TrainBody tmpFront, Transform tmpSelf)
    {
        front = tmpFront;
        self = tmpSelf;
        oldPos = tmpSelf.position;
    }

    //这一节车身已经向前移动了，更新oldPos
    public void Reflash()
    {
        oldPos = this.self.position;
    }

    //跟着前面的车身
    public void FollowFront()
    {
        self.position = front.oldPos; //更新车身的位置
        front.Reflash();
    }

    public virtual void MoveForward() { }
}

public class SnackHead : TrainBody
{
    //继承基类
    public SnackHead(TrainBody tmpFront, Transform tmpSelf) : base(tmpFront, tmpSelf)
    {
    }
    public override void MoveForward()
    {
        self.Translate(-self.forward, Space.World);
    }

    /// <summary>
    /// 向右移动
    /// </summary>
    public void TurnRight()
    {
        Vector3 tmpAngle = self.localEulerAngles;
        tmpAngle.y += 90;
        self.localEulerAngles = tmpAngle;
    }

    /// <summary>
    /// 向左移动
    /// </summary>
    public void TurnLeft()
    {
        Vector3 tmpAngle = self.localEulerAngles;
        tmpAngle.y -= 90;
        self.localEulerAngles = tmpAngle;
    }
}
