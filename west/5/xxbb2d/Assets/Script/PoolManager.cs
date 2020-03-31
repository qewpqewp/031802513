using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PoolData
{
    public GameObject fatherObj;
    public List<GameObject> poolList;
    public PoolData(GameObject obj,GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() { obj };
    }
    public GameObject GetObj()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        obj.GetComponent<PoolObj>().SendMessage("Awake");
        return obj;
    }

    
    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
        obj.SetActive(false);
    }
}
public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string,PoolData> dic = new Dictionary<string, PoolData>();
    private GameObject poolObj;

    public void GetObj(string name,UnityAction<GameObject> callback)
    {
        
        if(dic.ContainsKey(name) && dic[name].poolList.Count > 0)
        {
            
            callback(dic[name].GetObj());
        }
        else
        {
            ResManager.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                callback(o);
            });

        }
     
    }

    public void PushObj(string name,GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");
        obj.SetActive(false);
        if (dic.ContainsKey(name))
        {
            dic[name].PushObj(obj);
        }
        else
        {
            dic.Add(name, new PoolData(obj,poolObj));
        }
    }
}
