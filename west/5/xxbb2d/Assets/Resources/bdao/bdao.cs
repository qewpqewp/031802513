using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bdao : PoolObj
{
    public float dirx;
    public float diry;
    public float speed=3;
    public float time;
    public float atk;
    // Start is called before the first frame update
    override public void Awake()
    {
        Destroy( time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveto = new Vector3(dirx, diry, 0) * Time.deltaTime * speed + transform.position;
        GetComponent<Rigidbody2D>().MovePosition(moveto);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && collision.GetComponent<enemy>().insky <= 0)
        {
            collision.GetComponent<enemy>().hp -= atk;
            collision.GetComponent<enemy>().stun.Add(2f);


        }
        if (collision.tag == "enemyfire")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && collision.GetComponent<enemy>().insky <= 0)
        {
            Vector3 moveto = new Vector3(dirx, diry, 0) * Time.deltaTime * speed/2 + collision.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().MovePosition(moveto); 

        }

    }

}
