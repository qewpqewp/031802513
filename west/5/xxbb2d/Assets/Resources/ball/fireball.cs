using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : PoolObj
{
    public float dirx;
    public float diry;
    public float speed=3;
    public float time;
    public float atk;
    List<GameObject> enemyin = new List<GameObject>();
    public float delay = 0.3f;
    float nowtime;
    // Start is called before the first frame update
    override public void Awake()
    {
        enemyin.Clear();
        nowtime = 0;
        Destroy( time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nowtime += Time.deltaTime;
        if (nowtime >= delay)
        {   foreach(GameObject  x in enemyin)
            {
                x.GetComponent<enemy>().hp -= atk;
            }
            nowtime = 0;
        }
        Vector3 moveto = new Vector3(dirx, diry, 0) * Time.deltaTime * speed + transform.position;
        GetComponent<Rigidbody2D>().MovePosition(moveto);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && collision.GetComponent<enemy>().insky <= 0)
        {
            enemyin.Add(collision.gameObject);

        }
        if (collision.tag == "obj")
        {
            Destroy(collision.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyin.Remove(other.gameObject);

        }
    }

}
