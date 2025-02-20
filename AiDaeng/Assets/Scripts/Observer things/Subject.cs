using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> ObserverList = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        ObserverList.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        ObserverList.Remove(observer);
    }

    public void NotifyObserver(PlayerAction action)
    {
        ObserverList.ForEach((ObserverList) =>
        {
            ObserverList.OnNotify(action);
        });


    }
}
