using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �Ѿ��� �߻��ϴ� �÷��̾� ��ü
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            BulletObjectPool.instance.PopObj(transform.position, transform.rotation);
        }
    }
}
