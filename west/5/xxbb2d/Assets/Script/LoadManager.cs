using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadManager : BaseManager<LoadManager>
{
    public void LoadSceneAsyn(string name,UnityAction fun)
    {
        MonoManager.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }
    private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            yield return ao.progress;
        }
        fun();

    }
}
