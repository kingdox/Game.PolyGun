#region Imports
using UnityEngine;
#endregion
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoX
{
    #region Variables
    private Rigidbody body;
    private Quaternion lastRotation;

    #endregion
    #region Events
    private void Awake(){
        Get(out body);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Asignamos el movimiento basado en la dirección que se movera
    /// y la velocidad que lo hará
    /// <para>
    /// En caso de no asignarle velocidad se establecerá la
    /// que tiene <see cref="Movement"/>
    /// </para>
    /// </summary>
    public void Move(Vector3 _axis, float speed, bool oneDir = false){


        if (!GameManager.IsOnGame()){
            body.Sleep();
        }
        else{
            body.WakeUp();

            //SetRotation(_axis);
            if (oneDir)
            {
                //transform.rotation = Quaternion.LookRotation(Vector3.Normalize(transform.position) - _axis);
                transform.rotation = Quaternion.LookRotation(_axis, Vector3.forward);

            }
            else
            {
                transform.rotation = Quaternion.LookRotation(_axis);
            }

            body.velocity = new Vector3(_axis.x, 0, _axis.z) * speed;
            body.velocity = _axis * speed;// * Time.deltaTime;
        }
    }

    /// <summary>
    /// Asignamos la rotación basado en la dirección(del axis);
    /// </summary>
    private void SetRotation(Vector3 _axis){
        // si movemos

        if (!_axis.Equals(Vector3.zero)){
            //lastRotation = transform.rotation;
        }else{
            //transform.rotation = lastRotation;
        }
    }
    #endregion
}