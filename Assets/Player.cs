using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 총알을 발사하는 플레이어 객체
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            BulletObjectPool.instance.PopObj(transform.position, transform.rotation);
        }
    }
}
