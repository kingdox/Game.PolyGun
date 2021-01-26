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
    /// <para>returns true if reaches the side</para>
    /// </summary>
    public bool Move(Vector3 _axis, float speed, bool following = false){

        bool reached = false;

        if (!GameManager.IsOnGame()){
            body.Sleep();
        }
        else{
            body.WakeUp();

            if (following)
            {
                //new added yo prevent search whne falls the enemy
                _axis.y = transform.position.y;

                transform.position = Vector3.MoveTowards(
                        transform.position,
                        _axis,
                        Time.deltaTime
                        * speed
                    );
                if (transform.position.Equals(_axis))
                {
                    reached = true;
                }
            }
            else
            {
                body.velocity = _axis * speed;
            }

        }
       

        return reached;
    }
    #endregion
}