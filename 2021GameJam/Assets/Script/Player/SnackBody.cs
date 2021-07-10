using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class SnackBody : MonoBehaviour
{
    public SnackBody front; //表示 前一段蛇身
    public Transform self; //表示 当前蛇身
    Vector3 oldPos; //当前蛇身的前一个位置

    public SnackBody(SnackBody tmpFront, Transform tmpSelf)
    {
        front = tmpFront;
        self = tmpSelf;
        oldPos = tmpSelf.position;
    }

    //这一节蛇身已经向前移动了，更新oldPos
    public void Reflash()
    {
        oldPos = this.self.position;
    }

    //跟着前面的蛇身
    public void FollowFront()
    {
        self.position = front.oldPos; //更新蛇身的位置
        front.Reflash();
    }

    public virtual void MoveForward() { }
}

public class SnackHead : SnackBody
{
    //继承基类
    public SnackHead(SnackBody tmpFront, Transform tmpSelf) : base(tmpFront, tmpSelf)
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
