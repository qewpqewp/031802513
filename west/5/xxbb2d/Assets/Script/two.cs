using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class two : PoolObj
{
    public float speed;
    public float atk;
    public GameObject bullet;
    public float atkdelay;
    float atktime = 0;
    public float patroldelay;
    float patroltime = 0;
    public GameObject target;
    public float atkrange;
    public float stoprange;
    public float seerange;
    public Vector3 destination;
    public float time;
    // Start is called before the first frame update
    override public void Awake()
    {
        Destroy( time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ai();
    }
    void doatk()
    {

        GameObject temp = (GameObject)Instantiate(bullet);
        temp.transform.position = this.transform.position;
        temp.GetComponent<playeratk1>().dir = target.transform.position - temp.transform.position;
        temp.GetComponent<playeratk1>().atk = atk;
    }
    void movetodestination()
    {
        Vector3 temp = destination - transform.position;
        temp = temp.normalized * speed * Time.deltaTime;


        GetComponent<Rigidbody2D>().MovePosition(temp + this.transform.position);
    }
    void patrol()
    {
        destination = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));

    }
    void ai()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("enemy");
        }
        patroltime += Time.deltaTime;
        atktime += Time.deltaTime;
        
        if (target != null && Vector3.Distance(this.transform.position, target.transform.position) <= seerange)
        {
            destination = target.transform.position;
            if (Vector3.Distance(this.transform.position, target.transform.position) <= atkrange)
            {
                if (atktime > atkdelay)
                {
                    doatk();
                    atktime = 0;

                }


            }
            if (Vector3.Distance(this.transform.position, target.transform.position) >= stoprange)
                movetodestination();
            return;
        }
        else
        {
            if (patroltime > patroldelay)
            {
                patrol();
                patroltime = 0;
            }

        }

        movetodestination();
    }
}
