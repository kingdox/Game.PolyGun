#region IMports
using UnityEngine;
#endregion
[RequireComponent(typeof(Rigidbody))]
public class SaveVelocity : MonoX
{
    #region Variables
    private Rigidbody body;
    private Vector3 lastVel;
    private Vector3 lastpos;
    #endregion
    #region Events
    private void Awake()
    {
        Get(out body);
    }
    private void Update()
    {
        CheckBody();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Checks if the object can move or no
    /// </summary>
    private void CheckBody()
    {
        if (GameManager.IsOnGame())
        {
            //is Sleeping and then wakes up
            if (body.IsSleeping()) {
                body.velocity = lastVel;
                transform.position = lastpos;
            }
            else
            {
                //asignamos los nuevos cambios
                lastVel = body.velocity;
                lastpos = transform.position;
            }
            body.WakeUp();
        }
        else
        {
            body.Sleep();
        }
    }
    #endregion
}
