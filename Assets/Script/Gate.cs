using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    Collider m_collider;
    bool m_isActive = true;
    public UnityEvent m_desactivate;


    public Race m_race;

    public int m_id;
    private void Awake()
    {
        m_desactivate.AddListener(OnDesactivate);
        //gameObject.SetActive(false);
    }
    void Start()
    {
        m_collider = GetComponent<Collider>();

    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isActive)
            return;

        Heli heli = other.GetComponent<Heli>();
        if (!heli)
            return;

        if (m_race.CanGateBeActivated(m_id))
        {
            m_desactivate.Invoke();
        }
    }

    void OnDesactivate()
    {
        m_isActive = false;
        gameObject.SetActive(false);
    }
}
