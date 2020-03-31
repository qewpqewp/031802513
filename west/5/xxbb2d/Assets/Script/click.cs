using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class click : MonoBehaviour
{
    public GameObject height;
    public GameObject width;
    public GameObject count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doclick()
    {
        int a;
        int b;
        int c;
            int.TryParse(height.GetComponent<InputField>().text,out a);
        int.TryParse(width.GetComponent<InputField>().text, out b);
        int.TryParse(count.GetComponent<InputField>().text, out c);
        PlayerPrefs.SetInt("height", a);
        PlayerPrefs.SetInt("width", b);
        PlayerPrefs.SetInt("count", c);
        SceneManager.LoadSceneAsync(1);
    }
}
