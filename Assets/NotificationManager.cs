using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EventNotification
{
    Null,
    MainMenuStart,
    GameStart,
    GameOver
}


public class NotificationManager : MonoBehaviour {

    #region InternalType

    class EventReceiver
    {
        public Action<object> callback;
        public MonoBehaviour target;
    }

    #endregion


    static public NotificationManager Instance;
    static Dictionary<EventNotification, List<EventReceiver>> _internalList;

	void Awake()
    {
        Instance = this;
        _internalList = new Dictionary<EventNotification, List<EventReceiver>>();
    }

    public void AttachNotif(EventNotification notif, MonoBehaviour t, Action<object> action)
    {
        List<EventReceiver> tmp;
        var newEvent = new EventReceiver() {
            target=t,
            callback=action
        };

        if (_internalList.TryGetValue(notif, out tmp))
        {
            tmp.Add(newEvent);
        }
        else
        {
            _internalList.Add(notif, new List<EventReceiver>() { newEvent });
        }
    }

    public void FireNotification(EventNotification notif, object help=null)
    {
        List<EventReceiver> tmp;
        if (_internalList.TryGetValue(notif, out tmp))
        {
            foreach (var t in tmp.Where(i => i.target!=null))
            {
                t.callback(help);
            }
        }
    }

    public void RemoveNotification(MonoBehaviour target)
    {
        foreach(var t in _internalList.Select(i => new { Key = i.Key, Value = i.Value.Where(j => j.target == target).ToList() }))
        {
            _internalList[t.Key].RemoveAll(a => t.Value.Contains(a));
        }
    }


}
