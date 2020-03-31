using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyatk1 : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 3;
    public float atk;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        


        GetComponent<Rigidbody2D>().MovePosition(dir.normalized *speed * Time.deltaTime + this.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            collision.GetComponent<PlayerController>().hp -= atk;
            Destroy(this.gameObject);
        }
        if (collision.tag == "obj")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
    }
 
}
