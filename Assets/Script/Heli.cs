using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Heli : MonoBehaviour
{

    #region declaration
    
    float m_throtle = 0f;
    float m_yaw = 0f;
    float m_pitch = 0f;
    float m_roll = 0f;

    [SerializeField, Range(0f, 2000f)] float m_speedThrotle;
    [SerializeField, Range(0f, 200f)] float m_speedYaw;
    [SerializeField, Range(0f, 200f)] float m_speedPitch;
    [SerializeField, Range(0f, 200f)] float m_speedRoll;

    Rigidbody m_rb;


    #endregion

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        m_rb.AddRelativeForce(0f, m_speedThrotle * m_throtle, 0f);
        m_rb.AddRelativeTorque(m_speedRoll * m_roll, m_speedYaw * m_yaw, m_speedPitch * m_pitch);
    }


    #region input
    public void OnThrotle(InputAction.CallbackContext context)
    {
        m_throtle = context.ReadValue<float>();
    }

    public void OnYaw(InputAction.CallbackContext context)
    {
        m_yaw = context.ReadValue<float>();
    }

    public void OnPitch(InputAction.CallbackContext context)
    {
        m_pitch = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        m_roll = context.ReadValue<float>();
    }
    #endregion
}
