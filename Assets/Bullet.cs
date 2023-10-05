using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    void OnEnable() // 활성화될때, start에서 하면 안됨 생성은 오브젝트 풀링은 한번이니까
    {
        StartCoroutine(ReturnCo());
    }

    IEnumerator ReturnCo() // 일정시간이 지나고 리턴하는 코루틴
    {
        yield return new WaitForSeconds(2);
        BulletObjectPool.instance.ReturnPool(gameObject);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);       
    }

    private void OnTriggerEnter(Collider other)
    {
        BulletObjectPool.instance.ReturnPool(gameObject);
    }
}
