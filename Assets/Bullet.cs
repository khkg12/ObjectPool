using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    void OnEnable() // Ȱ��ȭ�ɶ�, start���� �ϸ� �ȵ� ������ ������Ʈ Ǯ���� �ѹ��̴ϱ�
    {
        StartCoroutine(ReturnCo());
    }

    IEnumerator ReturnCo() // �����ð��� ������ �����ϴ� �ڷ�ƾ
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
