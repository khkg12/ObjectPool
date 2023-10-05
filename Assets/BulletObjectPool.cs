using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    // 오브젝트 풀링 : 빈번한 생성과 파괴는 연산적으로 비효율적이기 때문에 예상하여 미리 만들어두고 활성화/비활성화 함으로서 관리하는 패턴.

    // 1.일단 미리 만들어둘 놈들을 담아둘 콜렉션(풀)이 존재해야하고,
    // 2.생성해서 콜렉션에 담는다. (비활성화해서 담는다.)
    // 3.꺼내쓸 땐 활성화한다. 
    // 4.다쓰면 다시 콜렉션에 담아준다. (비활성화해서 담는다.)
    // 넣었다 뺐다만 하기때문에 큐로 구현하는게 제일 쉬울것

    public static BulletObjectPool instance;  
    public Queue<GameObject> objectPool;
    [SerializeField] GameObject prefab;
    [SerializeField] int initSize;

    private void Awake() // 싱글턴
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

    public void PopObj(Vector3 pos, Quaternion rot) // 풀에 있는 오브젝트를 뽑아내는 함수
    {
        if(objectPool.Count == 0) // 오브젝트 풀이 비어있다면
        {
            Debug.LogWarning("풀이 비어 추가생성");
            CreatePool(initSize / 3);
        }
        GameObject dequeObj = objectPool.Dequeue();
        dequeObj.transform.position = pos; // 활성화되기 전 위치와 회전 세팅
        dequeObj.transform.rotation = rot;  
        dequeObj.SetActive(true);                
    }  

    public void ReturnPool(GameObject returnObj) // 다쓴놈을 풀에 되돌려주는 함수
    {        
        returnObj.SetActive(false);
        objectPool.Enqueue(returnObj);        
        // 위치초기화는 해줄필요없음, 꺼낼때 해주기때문에
    }   

    public void CreatePool(int size) // 풀에 생성하는 함수
    {
        for(int i = 0; i < size; i++)
        {
            GameObject temp = null;
            temp = Instantiate(prefab);
            temp.transform.parent = transform; // 게임오브젝트 자식으로
            temp.SetActive(false); // 비활성화
            objectPool.Enqueue(temp);
        }
    }    
}
