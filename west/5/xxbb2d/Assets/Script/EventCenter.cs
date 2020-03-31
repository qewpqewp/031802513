using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public interface IEventInfo {
}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> dic = new Dictionary<string, IEventInfo>();
    
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        if (dic.ContainsKey(name))
        {
            (dic[name] as EventInfo<T>).actions += action;
            //dic[name] += action;
        }
        else
        {
            dic.Add(name, new EventInfo<T> (action));
        }
    }
    public void AddEventListener(string name, UnityAction action)
    {
        if (dic.ContainsKey(name))
        {
            (dic[name] as EventInfo).actions += action;
            //dic[name] += action;
        }
        else
        {
            dic.Add(name, new EventInfo(action));
        }
    }
    public void RemoveEventListener<T>(string name,UnityAction<T> action)
    {
        if (dic.ContainsKey(name))
        {
            (dic[name] as EventInfo<T>).actions -= action;
        }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (dic.ContainsKey(name))
        {
            (dic[name] as EventInfo).actions -= action;
        }
    }
    public void EventTrigger<T>(string name,T info)
    {
        if (dic.ContainsKey(name))
        {
            if((dic[name] as EventInfo<T>).actions != null)
            {
                (dic[name] as EventInfo<T>).actions.Invoke(info);
            }
            
        }
    }
    public void EventTrigger(string name)
    {
        if (dic.ContainsKey(name))
        {
            if ((dic[name] as EventInfo).actions != null)
            {
                (dic[name] as EventInfo).actions.Invoke();
            }

        }
    }
    public void Clear()
    {
        dic.Clear();
    }
}
