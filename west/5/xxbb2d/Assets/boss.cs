using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : enemy

{


    public  float hprecover;
  
    public float blinkrange;
    float atktime = 0;
    float blinkcd=5;
    float blinktime=5;
    float utlcd=30;
    float utltime=30;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
       
        stun = new List<float>();
        

    }

    // Update is called once per frame
    void doatk()
    {
        atktime = 0;
        MusicManager.GetInstance().PlaySound("clips/mana_break");
        target.GetComponent<PlayerController>().hp -= atk;
        target.GetComponent<PlayerController>().mana -= 60;
        if (target.GetComponent<PlayerController>().mana < 0)
        {
            target.GetComponent<PlayerController>().mana = 0;
        }
    }
    void doutl()
    {
        MusicManager.GetInstance().PlaySound("clips/mana_void");
        utltime = 0;
        target.GetComponent<PlayerController>().hp -= (target.GetComponent<PlayerController>().maxmana - target.GetComponent<PlayerController>().mana) * 2;
        
    }
    void movetodestination()
    {
        Vector3 temp = destination - transform.position;
        temp = temp.normalized * speed * Time.deltaTime;
        

        GetComponent<Rigidbody2D>().MovePosition(temp + this.transform.position);
    
       
    }
    void blinktodestination()
    {

        blinktime = 0;

        if (Vector3.Distance(this.transform.position, destination) < blinkrange)
        {
            Debug.Log("blink");
            MusicManager.GetInstance().PlaySound("clips/blink_out");
            transform.position = destination;
        }
        else
        {
            Vector3 temp = destination - transform.position;
            temp = temp.normalized * blinkrange;
            GetComponent<Rigidbody2D>().MovePosition(temp + this.transform.position);
        }
        
    }
    void hpcheck()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    int state()
    {
        if (target == null)
        {
            return 0;
        }
        if (this.hp < 500)
        {
            
            Vector3 temp = transform.position- target.transform.position  ;
            temp = temp.normalized * 1.3f;
            destination = transform.position + temp;
            return 0;
        }
        float utldmg = (target.GetComponent<PlayerController>().maxmana - target.GetComponent<PlayerController>().mana) * 2;
        if((utldmg>1000 || utldmg > target.GetComponent<PlayerController>().hp) && utltime>=utlcd)
        {
            Debug.Log(utltime);
            destination = target.transform.position;
            return 2;
        }
        else
        {
            destination = target.transform.position;
            return 1;
        }
        
    }


    void ai()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("player");
        }
        switch (state())
        {
            case 0:
                if (blinktime > blinkcd)
                {
                    blinktodestination();
                }
                else
                {
                    movetodestination();
                }
                break;
            case 1:
                //Debug.Log(Vector3.Distance(this.transform.position, target.transform.position));
                if(Vector3.Distance(this.transform.position, target.transform.position) > atkrange)
                {

                    if (blinktime > blinkcd)
                    {
                        blinktodestination();
                    }
                    else
                    {
                        movetodestination();
                    }
                }

                else
                {
                    
                    if (atktime > atkdelay)
                    {
                        movetodestination();
                        doatk();
                        

                    }
                }
                break;
            case 2:
                if (Vector3.Distance(this.transform.position, target.transform.position) > atkrange)
                {

                    if (blinktime>blinkcd)
                    {
                        blinktodestination();
                    }
                    else
                    {
                        movetodestination();
                    }
                }
                else
                {
                    if (utltime > utlcd)
                    {
                        doutl();
                        

                    }
                }
                break;
        }
        
        
    }

    void FixedUpdate()

    {

        if (hp < maxhp)
        {
            hp += hprecover * Time.deltaTime;
        }
        life.transform.localScale = new Vector3(3f * hp / maxhp, 0.25f, 1);
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
        for (int i = 0; i < stun.Count; i++)
        {
            stun[i] -= Time.deltaTime;
            if (stun[i] <= 0)
            {
                stun.Remove(stun[i]);
            }

        }

        if (stun.Count == 0 && insky <= 0)
        {

            ai();
        }
        utltime += Time.deltaTime;
        blinktime += Time.deltaTime;
        atktime += Time.deltaTime;
        hpcheck();

    }

}
