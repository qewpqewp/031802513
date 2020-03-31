using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private event UnityAction fixedupdateEvent;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
        {
            updateEvent();
        }
    }
    private void FixedUpdate()
    {
        if (fixedupdateEvent != null)
        {
            fixedupdateEvent();
        }
    }
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
    public void AddFixedUpdateListener(UnityAction fun)
    {
        fixedupdateEvent += fun;
    }
    public void RemoveFixedUpdateListener(UnityAction fun)
    {
        fixedupdateEvent -= fun;
    }
}
