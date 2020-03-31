using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    public string name;
    private GameObject player;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (name == "hp")
        {
            int hp = (int)player.GetComponent<PlayerController>().hp;
            int maxhp = (int)player.GetComponent<PlayerController>().maxhp;
            this.GetComponent<Slider>().maxValue = maxhp;
            this.GetComponent<Slider>().value = hp;
            text.text = hp + " / " + maxhp;
        }
        if (name == "mana")
        {
            int mana=(int) player.GetComponent<PlayerController>().mana;
            int maxmana=(int) player.GetComponent<PlayerController>().maxmana;

            this.GetComponent<Slider>().maxValue = maxmana;
            this.GetComponent<Slider>().value = mana;
            text.text = mana + " / " + maxmana;
        }
    }
}
