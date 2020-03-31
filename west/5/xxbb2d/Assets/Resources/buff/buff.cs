 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff : PoolObj
{
    public float time;
    // Start is called before the first frame update
    override public void Awake()
    {
        Destroy( time);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
