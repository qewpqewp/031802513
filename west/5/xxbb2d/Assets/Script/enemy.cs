using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour

{
    public GameObject life;
    public float hp;
    public float maxhp;
    public float speed;
    public float atk;
    public GameObject bullet;
    public float atkdelay;
     float atktime=0;
    public float patroldelay;
     float patroltime=0;
    public GameObject target;
    public float atkrange;
    public float stoprange;
    public float seerange;
    public Vector3 destination;
    public List<float> stun;
    public float insky;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        stun = new List<float>();
       
    }

    // Update is called once per frame
    void doatk()
    {
     
        GameObject temp = (GameObject)Instantiate(bullet);
        temp.transform.position = this.transform.position;
        temp.GetComponent<enemyatk1>().dir= target.transform.position-temp.transform.position;
        temp.GetComponent<enemyatk1>().atk = atk;
    }
    void movetodestination()
    {
        Vector3 temp = destination - transform.position;
        temp=temp.normalized*speed*Time.deltaTime;


        GetComponent<Rigidbody2D>().MovePosition(temp+this.transform.position);
    }
    void hpcheck()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }



    void patrol()
    {
        destination = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
        
    }
    void ai()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("player");
        }
        
        if ( target!=null &&Vector3.Distance(this.transform.position, target.transform.position) <= seerange)
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

    void FixedUpdate()

    {

        life.transform.localScale = new Vector3(10 * hp / maxhp, 1, 1);
        if (insky > 0)
        {   
            insky -= Time.deltaTime;
            this.transform.Rotate(0, 0, 15);
            if (insky <= 0)
            {
                this.transform.rotation = new Quaternion(0, 0, 0, 0);
                this.transform.localScale /= 2;
                hp -= 150;
            }
      
        }
        for(int i = 0; i < stun.Count; i++)
        {
            stun[i] -= Time.deltaTime;
            if (stun[i] <= 0)
            {
                stun.Remove(stun[i]);
            }
           
        }
        
        if (stun.Count == 0 && insky<=0)
        {
            
            ai();
        }
        patroltime += Time.deltaTime;
        atktime += Time.deltaTime;
        hpcheck();

    }

}
