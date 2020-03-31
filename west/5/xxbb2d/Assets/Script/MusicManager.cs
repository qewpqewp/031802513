using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MusicManager : BaseManager<MusicManager>
{
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    public MusicManager()
    {
        MonoManager.GetInstance().AddUpdateListener(Update);
    }
    public void RemoveMusic()
    {
        MonoManager.GetInstance().RemoveUpdateListener(Update);
    }
    private void Update()
    {
        for(int i = soundList.Count - 1; i >= 0; --i)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);

            }
        }
    }
   public void PlaySound(string name,UnityAction<AudioSource> callback=null)
    {
        if(soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }

        ResManager.GetInstance().LoadAsync<AudioClip>(name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            soundList.Add(source);
            if (callback != null)
                callback(source);
        });
    }
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }

}
