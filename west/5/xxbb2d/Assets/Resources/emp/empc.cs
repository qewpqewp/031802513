 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class empc : PoolObj
{
    public float time;
    public float boomtime;
    public float Nowtime=0;
     AudioClip boom;
   public  List<GameObject> enemyin = new List<GameObject>();
    // Start is called before the first frame update
    override public void Awake()
    { enemyin.Clear();
        Nowtime = 0;
        boom = Resources.Load<AudioClip>("emp/emp_discharge");
        Destroy( time);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Nowtime += Time.deltaTime;
        if (Nowtime > boomtime)
        {
            Nowtime = 0;
            
            foreach (GameObject x in enemyin)
            {
                if(x.GetComponent<enemy>().insky <= 0)
                {
                    PlayerController temp = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
                    temp.mana += 200;
                    if (temp.mana > temp.maxmana)
                    {
                        temp.mana = temp.maxmana;
                    }
                x.GetComponent<enemy>().hp -= 200;
                }
            }
            AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position,0.3f);



        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            enemyin.Add(collision.gameObject);

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
