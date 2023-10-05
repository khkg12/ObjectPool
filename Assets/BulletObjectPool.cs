using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    // ������Ʈ Ǯ�� : ����� ������ �ı��� ���������� ��ȿ�����̱� ������ �����Ͽ� �̸� �����ΰ� Ȱ��ȭ/��Ȱ��ȭ �����μ� �����ϴ� ����.

    // 1.�ϴ� �̸� ������ ����� ��Ƶ� �ݷ���(Ǯ)�� �����ؾ��ϰ�,
    // 2.�����ؼ� �ݷ��ǿ� ��´�. (��Ȱ��ȭ�ؼ� ��´�.)
    // 3.������ �� Ȱ��ȭ�Ѵ�. 
    // 4.�پ��� �ٽ� �ݷ��ǿ� ����ش�. (��Ȱ��ȭ�ؼ� ��´�.)
    // �־��� ���ٸ� �ϱ⶧���� ť�� �����ϴ°� ���� �����

    public static BulletObjectPool instance;  
    public Queue<GameObject> objectPool;
    [SerializeField] GameObject prefab;
    [SerializeField] int initSize;

    private void Awake() // �̱���
    {
        if(instance == null)
            instance = this;    
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        objectPool = new Queue<GameObject>();
        Init();
    }
   
    public void Init()
    {
        CreatePool(initSize);
    }

    public void PopObj(Vector3 pos, Quaternion rot) // Ǯ�� �ִ� ������Ʈ�� �̾Ƴ��� �Լ�
    {
        if(objectPool.Count == 0) // ������Ʈ Ǯ�� ����ִٸ�
        {
            Debug.LogWarning("Ǯ�� ��� �߰�����");
            CreatePool(initSize / 3);
        }
        GameObject dequeObj = objectPool.Dequeue();
        dequeObj.transform.position = pos; // Ȱ��ȭ�Ǳ� �� ��ġ�� ȸ�� ����
        dequeObj.transform.rotation = rot;  
        dequeObj.SetActive(true);                
    }  

    public void ReturnPool(GameObject returnObj) // �پ����� Ǯ�� �ǵ����ִ� �Լ�
    {        
        returnObj.SetActive(false);
        objectPool.Enqueue(returnObj);        
        // ��ġ�ʱ�ȭ�� �����ʿ����, ������ ���ֱ⶧����
    }   

    public void CreatePool(int size) // Ǯ�� �����ϴ� �Լ�
    {
        for(int i = 0; i < size; i++)
        {
            GameObject temp = null;
            temp = Instantiate(prefab);
            temp.transform.parent = transform; // ���ӿ�����Ʈ �ڽ�����
            temp.SetActive(false); // ��Ȱ��ȭ
            objectPool.Enqueue(temp);
        }
    }    
}
