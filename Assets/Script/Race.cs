using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Race : MonoBehaviour
{
    [SerializeField] GameObject m_gateToInstenciate;

    [SerializeField] Gate[] m_gates;

    public int m_lastGate = 0;

    void Start()
    {
        CreateRace();
    }

    void CreateRace()
    {


        m_gates = GetComponentsInChildren<Gate>();
        foreach (Gate gate in m_gates)
        {
            gate.m_race = this;
            gate.gameObject.SetActive(true);
        }

        Array.Sort(m_gates, (gate1, gate2) => gate1.m_id.CompareTo(gate2.m_id));
    }

    void StopRace()
    {
        foreach (Gate gate in m_gates)
        {
            gate.gameObject?.SetActive(false);
        }
        m_lastGate = 0;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public bool CanGateBeActivated(int _id)
    {
        if (m_lastGate == _id)
        {
                Debug.Log("yes");
            m_lastGate++;
            if (m_lastGate >= m_gates.Length)
            {
                Debug.Log("goujoub");
                StopRace();
                Destroy(this, 1f);
            }
            return true;
        }
                Debug.Log("no");
        return false;
    }
}
