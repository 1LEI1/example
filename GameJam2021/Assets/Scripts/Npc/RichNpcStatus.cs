using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///
///<summary>
public class RichNpcStatus : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameOver();
    }
    void GameOver()
    {
        SceneManager.LoadScene(0);//�������볡��  ���¿�ʼ
    }
}
