using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public Transform parent = null;

    public void InitPool(GameObject _obj, int poolSize)
    {
        parent = new GameObject(_obj.name).transform;

        for (int i=0; i<poolSize; i++)
        {
            GameObject obj = Instantiate(_obj);
            obj.name += i;            
            obj.SetActive(false);
            obj.transform.SetParent(parent);

            list.Add(obj);
        }
    }

    // 대기중인 오브젝트를 꺼내온다
    public GameObject GetObject(float posX, float posY)
    {
        GameObject obj = list.Find(item => item.activeSelf == false);

        // 자식을 가지고 있으면 자식들도 전부 활성화
        if(obj.transform.childCount > 0)
        {
            // 자식 오브젝트도 전부 활성화
            for (int j = 0; j < obj.transform.childCount; j++)
            {
                obj.transform.GetChild(j).gameObject.SetActive(true);
            }
        }

        if (obj == null)
            return null;

        obj.transform.position = new Vector3(posX, posY, 0f);
        obj.SetActive(true);
        return obj;      
    }

    // 대기중인 오브젝트가 있는지 확인만 함
    public GameObject PeekObject()
    {
        GameObject obj = list.Find(item => item.activeSelf == false);
        return obj;
    }
}
