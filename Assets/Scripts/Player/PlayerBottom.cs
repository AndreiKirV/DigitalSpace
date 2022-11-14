using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBottom : MonoBehaviour
{
    private List<GameObject> _contacts = new List<GameObject>();
    public UnityEvent LeftTheGround = new UnityEvent();
    public UnityEvent WentToGround = new UnityEvent();
    public UnityEvent WentToLava = new UnityEvent();

    private void OnTriggerEnter(Collider other) 
    {
        _contacts.Add(other.gameObject);

        if (LeftTheGround!=null)
        WentToGround.Invoke();

        if(other.gameObject.name == "Lava" && WentToLava != null)
        WentToLava.Invoke();
    }

    private void OnTriggerExit(Collider other) 
    {
        _contacts.Remove(other.gameObject);

        if (WentToGround!=null && _contacts.Count == 0)
        LeftTheGround.Invoke();
    }
}
