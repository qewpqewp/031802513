using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : PoolObj
{
    public float dirx;
    public float diry;
    public float speed=3;
    public float time;
    public AudioClip mini;
    // Start is called before the first frame update
    override public void Awake()
    {
        Destroy( time);
        mini = Resources.Load<AudioClip>("wind/mini");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveto = new Vector3(dirx, diry, 0) * Time.deltaTime * speed + transform.position;
        GetComponent<Rigidbody2D>().MovePosition(moveto);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy"  && collision.GetComponent<enemy>().insky <=0)
        {
            collision.GetComponent<enemy>().insky = 2;
            collision.transform.localScale *= 2;
            AudioSource.PlayClipAtPoint(mini, Camera.main.transform.position, 0.3f);

        }

    }
}

