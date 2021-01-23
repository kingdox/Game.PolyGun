#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// CameraTrack
///</summary>
public class CameraTrack : MonoBehaviour
{
    #region Variables
    //Objeto que seguirá
    public Transform target;
    //pos temp de la camara
    private Vector3 tempPosition;
    //
    public float zOffset = 2f;
    public float transitionSpeed = 1;

    //public LayerMask occlusionLayer;
    //private RaycastHit occlusionHit = new RaycastHit();
    #endregion
    #region Events
    private void LateUpdate() 
    {
        CameraMoveAndRotate();
    }
    #endregion
    #region Methods
    /// <summary>
    /// Rota y meuve la camara basado en el target
    /// </summary>
    private void CameraMoveAndRotate() {

        tempPosition.Set(
            target.position.x,
            transform.position.y,
            target.position.z - zOffset
        );

        //verificamos si hay una oclusion y modificamos la posicion de la camara
        //CheckOcclusion(ref tempPosition);

        transform.position = Vector3.Lerp(
            transform.position,
            tempPosition,
            Time.deltaTime * transitionSpeed
            );
            
        //Miraremos a la direccion en una direccion para suavizar el giro
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position),
            Time.deltaTime
        );

    }
    /// <summary>
    ///  comprueba obstaculos entre target y camara
    /// </summary>
    private void CheckOcclusion(ref Vector3 pos) {
        Debug.DrawLine(target.position, pos, Color.blue);

        //if (Physics.Linecast(
        //        target.position,
        //        pos,
        //        out occlusionHit,
        //        occlusionLayer
        //    )
        //){
        //    //colocamos el impacto en caso de...
        //    //repetamos la pos en eje Y
        //    pos.Set(
        //        occlusionHit.point.x,
        //        pos.y,
        //        occlusionHit.point.z
        //    );
        //    Debug.DrawRay(occlusionHit.point, Vector3.up, Color.green);

        //}
    }
    #endregion
}
