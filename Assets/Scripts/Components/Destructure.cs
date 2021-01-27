#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Destructure : MonoX
{
    #region Variables

    [Header("Destructure Settings")]
    public Transform targetWorld;
    public Transform targetModel;
    [Space]
    public Transform[] modelParts;
    [Space]
    private bool isDestructured = false;

    [Header("Debug")]
    public bool _Debug_Destructure = false;
    #endregion
    #region Events
    private void Start(){
        //Si no hay parent busca el tipo de tag world
        if (!targetWorld)
        {
            targetWorld = TargetManager.GetLeftoverContainer();
        }

        if (!targetModel)
        {
            if (!TryGetChild(ref targetModel, transform, "model"))
            {
                PrintX("Destructure.cs => Colocar etiqueta 'model' o bien asignarlo en el inspector");
            }
        }
        //podriamos colocar algo parecido para los modelos, en caso de no asignarlo..
        GetChilds(out modelParts, targetModel);

    }
    private void Update(){
        __Debug_Destructure();
    }
    #endregion
    #region Methods


    /// <summary>
    /// Extrae de su sitio actual a el objetivo mundo
    /// </summary>
    public void DestructureThis(){
        if (isDestructured) return;
        isDestructured = true;

        foreach (Transform part in modelParts)
        {
            part.parent = targetWorld;
            Rigidbody rigid = part.gameObject.AddComponent<Rigidbody>();
            BoxCollider collider = part.gameObject.AddComponent<BoxCollider>();

            
            Transform child = part.childCount > 0 ? part.GetChild(0) : null;
            rigid.mass = 0.01f;
            // Reajusta la posición
            if (child)
            {
                collider.size = child.localScale;
                child.position = Vector3.zero;
                child.localPosition = Vector3.zero;
            }
            else
            {
                collider.size = Vector3.one;
            }
            collider.center = Vector3.zero;

            part.tag = "leftover";

            Vector3 force = Vector3.one * 10;

            for (int x = 0; x < 3; x++) {
                force[x] = Random.Range(0, force[x]);
            }

            rigid.velocity = force;
            //Añadimos una fuerza aleatoria
        }

        //Limpiamos la base de la estructura en caso de poseer
        Collider[] cols = GetComponents<Collider>();
        foreach (Collider col in cols){
            col.enabled = false;
        }
        Rigidbody body = GetComponent<Rigidbody>();
        if (body)
        {
            body.Sleep();
        }
        //Destroy(gameObject);

    }


    /// <summary>
    /// Destruye la estructura que poseía este modelo por completo
    /// </summary>
    private void __Debug_Destructure(){
        if (!DebugFlag(ref _Debug_Destructure)) return;

        //ejecuta la destructura
        DestructureThis();

    }

    #endregion
}


/*
 * TODO
 * 
 * 
 * Script encargado de separar partes del modelo, viendo las partes del hijo
 * 
 * estas partes las asigna a un target que debería de ser el @WORLD para dejarlas en el mapa
 * tambien aprovecha de asignarle un RigidBody para mantenerlas fisicamente
 * 
 * 
 */