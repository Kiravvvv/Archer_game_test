//Замедлить объект (возможно и ускорить, но нужно доработать)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Slow_motion : Slow_motion_change
{

    [Tooltip("Физика")]
    [SerializeField]
    Rigidbody Body = null;

    [Tooltip("Фактор физики")]
    [SerializeField]
    float Rigidbody_factor = 20;

    /// <summary>
    /// Отключить замедление
    /// </summary>
    protected override void Off_dilation()
    {
        Body.useGravity = true;

        float velocity_factor = 2 / Time_dilation_factor;
        Body.velocity *= velocity_factor;
        Body.angularVelocity *= velocity_factor;

        Body.drag = 0;
        Body.angularDrag = 0.05f;
    }

    /// <summary>
    /// Включить замедление
    /// </summary>
    protected override void On_dilation()
    {
        float value = Rigidbody_factor - Rigidbody_factor * Time_dilation_factor;

        Body.useGravity = false;

        Body.drag = value;
        Body.angularDrag = value/2f;
    }

    public void Add_force(Vector3 direction_force)
    {
        Vector3 force = direction_force * Time_dilation_factor;
        Body.AddForce(force, ForceMode.Force);
    }

}
