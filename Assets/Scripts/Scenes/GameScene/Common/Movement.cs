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
    }
    #endregion
    #region Methods
    /// <summary>
    /// enable movement, if following it keeps fetching with the position..
    /// <para>returns true if reaches the side</para>
    /// </summary>
    public bool Move(Vector3 _axis, float speed, bool following = false){

        bool reached = false;

        //if (!GameManager.IsOnGame()){
        //    body.Sleep();
        //}
        //else{
        //    body.WakeUp();

        if (GameManager.IsOnGame())
        {
            //si estás cerca del suelo
            if (IsOnFloor())
            {

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
                    //Vector3
                    //_axis.y = 0;
                    //transform.position
                    body.velocity = (_axis * speed) + (Vector3.up * body.velocity.y);

                    //Old
                    //body.velocity = (_axis * speed);// + (Vector3.up * body.velocity.y);
                }
            }
            else
            {
                //go down more faster if it's far in Y
                transform.position -= transform.up * Time.deltaTime;
            }

        }


        //}


        return reached;
    }


    /// <summary>
    /// check if we are near of the floor
    /// </summary>
    /// <returns></returns>
    public bool IsOnFloor() => transform.position.y < 5;//HARDCODED?

    /// <summary>
    /// Stops the velocity
    /// </summary>
    public void StopMovement()
    {
        body.velocity = Vector3.zero;
    }

    #endregion
}