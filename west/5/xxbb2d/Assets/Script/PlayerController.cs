using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float ghost;
    public float hp;
    public float maxhp;
    public float mana;
    public float maxmana;
    public float hprecover;
    public float manarecover;
    public float atk;
    public GameObject bullet;
    public float atkdelay;
    float atktime = 0;
    AudioClip click;
    AudioSource voice;

    int jisu=0;
    float buff=0;
    public GameObject mana1;
    public GameObject mana2;
    public GameObject ico1;
    public GameObject ico2;
    public GameObject r;
    public GameObject ico11;
    public GameObject ico21;
    public Skill T;
    Dictionary<string, float> manacost = new Dictionary<string, float>();
    public float speed;
    public Rigidbody2D rd;
    Animator ani;
    Dictionary<string, GameObject> febs =new Dictionary<string, GameObject>();
    Dictionary<string,Sprite> icos =new Dictionary<string, Sprite>();
    Dictionary<string, float> cds = new Dictionary<string, float>();
    Dictionary<string, float> nowcds = new Dictionary<string, float>();
    Dictionary<string, Sprite> icos1 = new Dictionary<string, Sprite>();
    Dictionary<string, int> voicenumber= new Dictionary<string, int>();

    List<string> skillnames = new List<string>();
    public GameObject ball1,ball2,ball3;
    public string D;
    public string F;
    [System.Serializable]
    public class Skill
    {
        public Queue<char> balls=new Queue<char>();
        public int q = 0;
        public int w = 0;
        public int e = 0;
        public void Getball()
        {
            q = 0;
            w = 0;
            e = 0;
            for (int i = 0; i < balls.Count; i++)
            {

                if (balls.ElementAt(i) == 'Q')
                {

                    q++;
                }
                if (balls.ElementAt(i) == 'W')
                {

                    w++;
                }
                if (balls.ElementAt(i) == 'E')
                {

                    e++;
                }
            }
        }
        public string Getskill()
        {
            
            if (balls.Count == 3)
            {

                Getball();
                if (q == 1 && e == 1 && w == 1)
                {
                    return "bdao";
                }
                if(q==3 )
                {
                    return "jisu";
                }
                if (w == 3)
                {
                    return "emp";
                }
                if (e == 3)
                {
                    return "suns";
                }
                if (q == 2 && w == 1)
                {
                    return "ghost";
                }
                if(q==2 && e == 1)
                {
                    return "wall";
                }
                if(w==2 && q == 1)
                {
                    return "wind";
                }
                if(w==2 && e == 1)
                {
                    return "buff";
                }
                if(q==1 && e == 2)
                {
                    return "two";
                }
                if(w==1 && e == 2)
                {
                    return "ball";
                }




            }return "No";
        }

       public void Q()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('Q');
            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('Q');
            }
        }
        public void W()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('W');
            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('W');
            }
        }

        public void E()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('E');

            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('E');
            }
        }


    }

    SpriteRenderer sr;
    int dir;
    public float lastx=1;
    public float lasty=0;
    void Start()
    {
        InputManager.GetInstance().StartOrEndCheck(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键按下", CheckInputDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("某键持续", CheckInput);
        EventCenter.GetInstance().AddEventListener<Dictionary<string, float>>("方向键", move);
        MonoManager.GetInstance().AddUpdateListener(getball);
        MonoManager.GetInstance().AddUpdateListener(checkhp);
        MonoManager.GetInstance().AddFixedUpdateListener(cdcheck);
        MonoManager.GetInstance().AddFixedUpdateListener(ghostcheck);

        ResManager.GetInstance().LoadAsync<AudioClip>("spawn/spawn" + (Random.Range(1, 5) + 1).ToString(), (clip) =>
        {
            voice.clip = clip;
            voice.Play();

        });
        LoadResources();
   
    }
    void LoadResources()
    {
        cds["refresh"] = 1;
        rd = this.GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        voice = this.GetComponent<AudioSource>();
        
        voicenumber["fail"] = 13;
        voicenumber["r"] = 17;
        voicenumber["buff"] = 5;
        voicenumber["wall"] = 5;
        voicenumber["ball"] = 7;
        voicenumber["emp"] = 10;
        voicenumber["suns"] = 5;
        voicenumber["bdao"] = 6;
        voicenumber["wind"] = 5;
        voicenumber["ghost"] = 5;
        voicenumber["jisu"] = 5;
        voicenumber["two"] = 6;
        cds["ghost"]= 1f / 22 * Time.deltaTime;
        cds["jisu"]= 1f / 10 * Time.deltaTime;
        cds["two"]= 1f / 15 * Time.deltaTime;
        cds["buff"] = 1f / 8 *Time.deltaTime;
        cds["wall"] = 1f / 12 * Time.deltaTime;
        cds["ball"] = 1f / 22 * Time.deltaTime;
        cds["emp"] = 1f / 15 * Time.deltaTime;
        cds["suns"] = 1f / 12 * Time.deltaTime;
        cds["bdao"] = 1f / 20 * Time.deltaTime;
        cds["wind"] = 1f / 15 * Time.deltaTime;
        cds["r"] = 1f / 1 * Time.deltaTime;
        nowcds["ghost"] = 1;
        nowcds["jisu"] = 1;
        nowcds["two"] = 1;
        nowcds["buff"] = 1;
        nowcds["wall"] = 1;
        nowcds["ball"] = 1;
        nowcds["emp"] = 1;
        nowcds["suns"] = 1;
        nowcds["bdao"] = 1;
        nowcds["wind"] = 1;
        nowcds["r"] = 1;
        skillnames.Add("buff");
        skillnames.Add("wall");
        skillnames.Add("ball");
        skillnames.Add("emp");
        skillnames.Add("suns");
        skillnames.Add("bdao");
        skillnames.Add("wind");
        skillnames.Add("ghost");
        skillnames.Add("two");
        skillnames.Add("jisu");
        manacost.Add("buff", 60f);
        manacost.Add("wall", 175f);
        manacost.Add("ball", 200f);
        manacost.Add("emp", 125f);
        manacost.Add("suns", 175f);
        manacost.Add("bdao", 300f);
        manacost.Add("wind", 150f);
        manacost.Add("ghost", 200f);
        manacost.Add("two", 75f);
        manacost.Add("jisu", 100f);
        manacost.Add("refresh", 375f);
        foreach (string name in skillnames)
        {
            string temp=name;
            ResManager.GetInstance().LoadAsync<Sprite>("ico/invoker/" + temp, (o) =>
            {
                icos.Add(temp, o);
              

            });  
           
            ResManager.GetInstance().LoadAsync<Sprite>("ico/invoker/" + temp + "1", (o) =>
            {
                icos1.Add(temp, o);


            });

         

        }
        ResManager.GetInstance().LoadAsync<Sprite>("ico/Q", (o) =>
        {
            icos["Q"] = o;


        });
        ResManager.GetInstance().LoadAsync<Sprite>("ico/W", (o) =>
        {
            icos["W"] = o;


        });
        ResManager.GetInstance().LoadAsync<Sprite>("ico/E", (o) =>
        {
            icos["E"] = o;


        });
        

    }
     void cast(string name)
    {
        if (manacost[name] > mana)
        {
            return;
        }
        if (name == "refresh")
        {
            cds["refresh"] = 0;
            mana -= manacost[name];
            outghost();
            say(name);
            nowcds["ghost"] = 1;
            nowcds["jisu"] = 1;
            nowcds["two"] = 1;
            nowcds["buff"] = 1;
            nowcds["wall"] = 1;
            nowcds["ball"] = 1;
            nowcds["emp"] = 1;
            nowcds["suns"] = 1;
            nowcds["bdao"] = 1;
            nowcds["wind"] = 1;
            nowcds["r"] = 1;
            return;
        }
        nowcds[name] = 0;
        mana -= manacost[name];
        outghost();
        say(name);
        if (name == "two")
        {
            PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                temp.transform.position = this.transform.position + new Vector3(1, 0);
            });


               

        }
        if (name == "ghost")
        {
            MusicManager.GetInstance().PlaySound("ghost/ghost");
            //AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("ghost/ghost"), Camera.main.transform.position, 0.5f);
            inghost();

        }
        if (name=="buff")
        {
            
             PoolManager.GetInstance().GetObj(name + "/feb",(temp)=> {
                 temp.transform.parent = this.transform;
                 temp.transform.position = this.transform.position + new Vector3(0, 1);
             });

     
            buff += 10;
            


        }
        if (name=="wall")
        {

            PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                temp.transform.Rotate(0, 0, -45 * dir);
                temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
            });
            


        }
        if (name=="suns")

        {
            GameObject[] temps=GameObject.FindGameObjectsWithTag("enemy");
            if (buff > 0)
            {
                int i = 0;
                foreach (GameObject temp in temps)
                {

                    PoolManager.GetInstance().GetObj(name + "/feb", (o) => {
                        o.transform.position = temp.transform.position + new Vector3(0.3f, 0.3f, 0);
                        if (i > 4)
                        {
                            o.GetComponent<AudioSource>().Stop();
                        }
                    });
                    PoolManager.GetInstance().GetObj(name + "/feb", (o) => {
                        o.transform.position = temp.transform.position + new Vector3(-0.3f, -0.3f, 0);
                        if (i > 4)
                        {
                            o.GetComponent<AudioSource>().Stop();
                        }
                        else
                        {
                            i++;
                        }
                    });
                }
            }
            else
            {
                GameObject temp = GameObject.FindGameObjectWithTag("enemy");
                if(temp!=null)
                PoolManager.GetInstance().GetObj(name + "/feb", (o) => {
                    o.transform.position = temp.transform.position;
                });
            }
          
            


        }
        if (name=="bdao")
        {
            if (buff > 0)
            {   for(int i = 0; i < 8; i++)
                {
                    int t = i;
                        PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                            temp.transform.position = this.transform.position + new Vector3(getx(t), gety(t));

                            temp.GetComponent<bdao>().dirx = getx(t);
                            temp.GetComponent<bdao>().diry = gety(t);
                            if (t != 0)
                            {
                                temp.GetComponent<AudioSource>().Stop();
                            }
                            temp.transform.rotation = Quaternion.Euler(0, 0, -45 * (t - 2));
                        });
             
                    
                }
                

            }
            else
            {
                PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                    temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
                    temp.GetComponent<bdao>().dirx = lastx;
                    temp.GetComponent<bdao>().diry = lasty;
                    temp.transform.rotation =  Quaternion.Euler(0, 0, -45 * (dir-2));
                    //temp.transform.Rotate(0, 0, -45 * dir);
                });
               
            }

        }
        if (name=="ball")
        {
            PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
                temp.GetComponent<fireball>().dirx = lastx;
                temp.GetComponent<fireball>().diry = lasty;
            });
     
            

        }
        if (name=="wind")
        {
            PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
            temp.GetComponent<wind>().dirx = lastx;
            temp.GetComponent<wind>().diry = lasty;
            });
        }
        if (name=="emp")
        {
            PoolManager.GetInstance().GetObj(name + "/feb", (temp) => {
                temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
            });
        }
        if (name == "jisu")
        {
            if (jisu != 3)
            {
                jisu = 3;
            }
        }
    }
    void doatk()
    {
        
        outghost();
        PoolManager.GetInstance().GetObj("playeratk", (temp) => {
            //GameObject temp = (GameObject)Instantiate(bullet);
            temp.transform.position = this.transform.position;
        temp.GetComponent<playeratk1>().dir = new Vector3(lastx,lasty);
        if (jisu != 0)
        {
            jisu--;
            temp.GetComponent<playeratk1>().jisuon();

        }

        temp.GetComponent<playeratk1>().atk = atk+ T.e*10;
        if (buff != 0)
        {
            temp.GetComponent<playeratk1>().atk += 50;
        }
        });
    }
    void CheckInput(KeyCode key)
    { 
        switch (key)
        {
            case KeyCode.Space:


                if (atktime > atkdelay)
                {
                    doatk();
                    atktime = 0;

                }
                break;
        }
    }
     void CheckInputDown(KeyCode key)
    {
        
        switch (key)
        {
            case KeyCode.Escape:
                RemoveAll();
                SceneManager.LoadSceneAsync(0); 
                break;
            case KeyCode.Q:
                outghost();
                if (T.balls.Count == 0)
                {
                    ball1.SetActive(true);

                }
                if (T.balls.Count == 1)
                {
                    ball2.SetActive(true);

                }
                if (T.balls.Count == 2)
                {
                    ball3.SetActive(true);

                }
                MusicManager.GetInstance().PlaySound("clips/click");
                //AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
                T.Q();
                break;
            case KeyCode.W:
                outghost();
                if (T.balls.Count == 0)
                {
                    ball1.SetActive(true);

                }
                if (T.balls.Count == 1)
                {
                    ball2.SetActive(true);

                }
                if (T.balls.Count == 2)
                {
                    ball3.SetActive(true);

                }
                MusicManager.GetInstance().PlaySound("clips/click");
               // AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
                T.W();
                break;
            case KeyCode.E:
                outghost();
                if (T.balls.Count == 0)
                {
                    ball1.SetActive(true);

                }
                if (T.balls.Count == 1)
                {
                    ball2.SetActive(true);

                }
                if (T.balls.Count == 2)
                {
                    ball3.SetActive(true);

                }
                MusicManager.GetInstance().PlaySound("clips/click");
                //AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
                T.E();
                break;
            case KeyCode.R:
                if (r.GetComponent<Image>().fillAmount >= 1)
                {
                    outghost();
                    MusicManager.GetInstance().PlaySound("clips/invoke");
                    //AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("clips/invoke"), Camera.main.transform.position, 0.3f);
                    r.GetComponent<Image>().fillAmount = 0;

                    string skill = T.Getskill();
                    if (skill == "No" || skill == D || skill == F)
                    {
                        say("fail");
                    }
                    else
                    {
                        say("r");
                        mana2.GetComponent<Text>().text = mana1.GetComponent<Text>().text;
                        ico21.GetComponent<Image>().sprite = ico11.GetComponent<Image>().sprite;
                        ico11.GetComponent<Image>().sprite = icos1[skill];
                        mana1.GetComponent<Text>().text = manacost[skill].ToString();
                        ico2.GetComponent<Image>().sprite = ico1.GetComponent<Image>().sprite;
                        ico1.GetComponent<Image>().sprite = icos[skill];
                        F = D;
                        D = skill;
                    }
                }
                break;
            case KeyCode.D:
                if(ico1.GetComponent<Image>().fillAmount >= 1)
                if (D != "error")
                {
                    
                    cast(D);
                }
                break;
            case KeyCode.F:
                if(ico2.GetComponent<Image>().fillAmount >= 1)
                if (F != "error")
                {
                    
                    cast(F);
                }
                break;
            case KeyCode.X:
                Debug.Log("xxx!!!");
                if (cds["refresh"] != 0)
                {
                    cast("refresh");
                }
                break;

        }
        
    }
    void say(string name)
    {
        if (name == "refresh")
        {
            MusicManager.GetInstance().PlaySound("clips/refresh");
            return;
        }
        

        if (voice.clip)
        {

            if (!voice.isPlaying || (voice.time >= 0.3 * voice.clip.length && name!="fail" &&name != "r"))
        {
                ResManager.GetInstance().LoadAsync<AudioClip>("clips/" + name + Random.Range(1, voicenumber[name] + 1), (clip) =>
                {
                    voice.clip = clip;
                    voice.Play();

                });
            
            
        }
        }
        else
        {
            ResManager.GetInstance().LoadAsync<AudioClip>("clips/" + name + Random.Range(1, voicenumber[name] + 1), (clip) =>
            {
                voice.clip = clip;
                voice.Play();

            });
        }

    }
    void cdcheck()
    {
        T.Getball();
        if (mana < maxmana)
        {
            mana += manarecover * Time.deltaTime;

        }
        if (hp < maxhp)
        {
            hp += (hprecover +13*T.q) * Time.deltaTime;
        }
        atktime += Time.deltaTime;
        if (buff > 0)
        {
            atktime += Time.deltaTime;
            buff -= Time.deltaTime;
        }
        r.GetComponent<Image>().fillAmount += cds["r"];
        if (D != "")
        {
            ico1.GetComponent<Image>().fillAmount = nowcds[D];
            if (mana < manacost[D])
            {
                ico1.GetComponent<Image>().color = Color.blue;
            }
            else
            {
                ico1.GetComponent<Image>().color = Color.white;
            }
        }
            
        if (F != "")
        {
            ico2.GetComponent<Image>().fillAmount = nowcds[F];
            if (mana < manacost[F])
            {
                ico2.GetComponent<Image>().color = Color.blue;
            }
            else
            {
                ico2.GetComponent<Image>().color = Color.white;
            }
        }
            

        foreach (string name in skillnames)
        {
            if (nowcds[name] < 1)
            {
                nowcds[name] += cds[name];
            }
        }
    }
     void move(Dictionary<string, float> dic)
    {
        
        float moveX = dic["Horizontal"] * Time.deltaTime * (speed+T.w*0.5f);
        float moveY = dic["Vertical"] * Time.deltaTime * (speed+ T.w * 0.5f);
        UpdateDir(moveX, moveY);
        if (moveX != 0 || moveY != 0)
        {
            ani.SetInteger("state", 1);
            if (moveX < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

        }
        else
        {
            ani.SetInteger("state", 0);
        }
        Vector3 moveto;
        if (dir == 1 || dir == 3 || dir == 5 || dir == 7)
        {
            moveto = (new Vector3(moveX, moveY, 0) / 1.414f) + transform.position;
        }
        else
        {
            moveto = new Vector3(moveX, moveY, 0) + transform.position;
        }
        rd.MovePosition(moveto);
    }
     void getball()
    {

        if (T.balls.Count == 3) {
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(2).ToString()];
            ball2.GetComponent<Image>().sprite = icos[T.balls.ElementAt(1).ToString()];
            ball3.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
            return; }
    if(T.balls.Count == 2)
        {
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(1).ToString()];
            ball2.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
            return;
        }
        if (T.balls.Count == 1)
        {
           
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
        }


    }
     void checkhp()
    {
        if (hp <= 0)
        {
            this.transform.DetachChildren();
            
            //SceneManager.LoadSceneAsync(0);
            RemoveAll();
            Destroy(this.gameObject, 0f);
        }
    }
    void outghost()
    {
        this.tag = "player";
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void inghost()
    {
        this.GetComponent<SpriteRenderer>().color = Color.gray;
        ghost += 10;
        this.tag = "Untagged";
        GameObject[] temps= GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject temp in temps)
        {
            temp.GetComponent<enemy>().target = null;
        }
    }

    void ghostcheck()
    {
        if (ghost > 0)
        {
            ghost -= Time.deltaTime;
            if (ghost <= 0)
            {
                outghost();
            }
        }
    }
    void UpdateDir(float x, float y)
    {
  
        if (x <0 && y >0)
        {
            lastx = -1;
            lasty = 1;
        
            dir = 7;
        }
        if (x == 0 && y>0)
        {
            lastx = 0;
            lasty = 1;
            dir = 0;
        }
        if (x >0 && y >0)
        {
            lastx = 1;
            lasty = 1;
            dir = 1;
        }
        if (x <0 && y == 0)
        {
            lastx = -1;
            lasty = 0;
            dir = 6;
        }
        if (x>0 && y == 0)
        {
            lastx = 1;
            lasty = 0;
            dir = 2;
        }
        if (x <0 && y <0)
        {
            lastx = -1;
            lasty = -1;
            dir = 5;
        }
        if (x == 0 && y <0)
        {
            lastx = 0;
            lasty = -1;
            dir = 4;
        }
        if (x >0 && y<0)
        {
            lastx = 1;
            lasty = -1;
            dir = 3;
        }

    }
    int getx(int dir)
    {
        if (dir == 7)
        {
            return -1;
            
        }
        if (dir == 0)
        {
            return 0;
            
        }
        if (dir == 1)
        {
            return 1;
           
        }
        if (dir==6)
        {
            return -1;
        }
        if (dir==2)
        {
            return 1;
        }
        if (dir==5)
        {
            return -1;
        }
        if (dir == 4)
        {
            
            return 0;
        }
        if (dir == 3)
        {
            return 1;
            
        }
        return 0;
    }
    int gety(int dir)
    {
        if (dir ==7)
        {
            return 1;
        }
        if (dir ==0)
        {
            return 1;
        }
        if (dir==1)
        {
            return 1;
        }
        if (dir==6)
        {
            return 0;
        }
        if (dir==2)
        {
            return 0;
        }
        if (dir == 5)
        {
            return -1;
        }
        if (dir == 4)
        {
            return -1;
        }
        if (dir == 3)
        {
            return -1;
        }
        return 0;
    }
    void RemoveAll()
    {
        MusicManager.GetInstance().RemoveMusic();
        InputManager.GetInstance().StartOrEndCheck(false);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键按下", CheckInputDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("某键持续", CheckInput);
        EventCenter.GetInstance().RemoveEventListener<Dictionary<string, float>>("方向键", move);
        MonoManager.GetInstance().RemoveUpdateListener(getball);
        MonoManager.GetInstance().RemoveUpdateListener(checkhp);
        MonoManager.GetInstance().RemoveFixedUpdateListener(cdcheck);
        MonoManager.GetInstance().RemoveFixedUpdateListener(ghostcheck);
    }
}
