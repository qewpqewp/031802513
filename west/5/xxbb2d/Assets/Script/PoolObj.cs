using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    virtual public void Awake()
    {
        
    }
    public void Destroy(float time)
    {
        Invoke("Push", time);
    }
    void Push()
    {
        PoolManager.GetInstance().PushObj(this.transform.name, this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
