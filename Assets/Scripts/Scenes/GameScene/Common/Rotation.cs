#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Rotation : MonoX
{
    #region
    public Transform target = null;
    public Vector3 direction = Vector3.zero;
    private bool auto = false;
    [Space]
    private Quaternion lastRotation;
    #endregion
    #region
    private void Awake()
    {
        auto = false;
    }
    private void Update()
    {
        if (auto)
        {
            if (target == null)
            {
                LookTo(direction);
            }
            else
            {
                LookTo(target.position);
            }
        }
    }
    #endregion
    #region Methods

    /// <summary>
    /// Changes the automatic rotation
    /// </summary>
    public void ChangeAutoRotate() => auto = !auto;

    /// <summary>
    /// rotates the object to the target pos.
    /// refresh the direction
    /// </summary>
    public void LookTo(Vector3 target){
        Quaternion quaternion = Quaternion.LookRotation(target - transform.position);
        SetDirection(quaternion);
    }
    /// <summary>
    /// Store the direction by a rotation
    /// </summary>
    public void SetDirection(Quaternion rotation)
    {
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
    }

    /// <summary>
    /// rotation based on the axis
    /// </summary>
    public void RotateByAxis(Vector3 axis)
    {
        axis.y = 0;
        //transform.rotation = Quaternion.LookRotation(axis);

        // if exist any axis movement
        if (!axis.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.LookRotation(axis);
            lastRotation = transform.rotation;
        }
        else
        {
            //else it shows the last...
            transform.rotation = lastRotation;
        }
    }

    
    #endregion
}
