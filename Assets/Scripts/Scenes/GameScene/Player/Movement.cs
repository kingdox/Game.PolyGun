#region Imports
using UnityEngine;
#endregion
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoX
{
    #region Variables
    private Rigidbody body;
    private Quaternion lastRotation;
    //private Rotation rotation;

    #endregion
    #region Events
    private void Awake(){
        Get(out body);
        //Get(out rotation);
    }
    #endregion
    #region Methods
    /// <summary>
    /// enable movement, if following it keeps fetching with the position..
    /// </summary>
    public void Move(Vector3 _axis, float speed, bool following = false){


        if (!GameManager.IsOnGame()){
            body.Sleep();
        }
        else{
            body.WakeUp();

            if (following)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _axis,
                    Time.deltaTime
                    * speed
                );
            }
            else
            {
                body.velocity = _axis * speed;
            }
        }
    }
    #endregion
}