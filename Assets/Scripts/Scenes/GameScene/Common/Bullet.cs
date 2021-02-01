﻿#region Imports
using UnityEngine;
#endregion
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Destructure))]
[RequireComponent(typeof(SaveVelocity))]
public class Bullet : MonoX
{
    #region Variables

    private Movement movement;
    private Rotation rotation;
    private Destructure destructure;
    private SaveVelocity saveVelocity;
    private Vector3 initPos;
    [Header("Bullet Settings")]
    [Space]
    public BulletShot bulletShot;
    public Vector3 direction;

    [Space]
    public bool canFollow = false;
    [Header("Effects")]
    public TrailRenderer part_trail;
    public ParticleSystem part_contact;
    #endregion
    #region Events
    private void Awake(){
        Get(out destructure);
        Get(out movement);
        Get(out rotation);
        GetAdd(ref saveVelocity);

        initPos = transform.position;
    }

    private void Start()
    {
        //Si puede seguir busca a un enemigo y se ajusta
        if (canFollow)
        {
            Transform tran_enemy = TargetManager.GetEnemy(transform);

            //si no consigue enemigo...
            if (tran_enemy == null) 
            {
                canFollow = false;
            }
            else
            {
                direction = tran_enemy.position;
                rotation.LookTo(direction);
            }
        }
    }
    
    private void Update(){ 

        if (PassedRange()){
            DeleteBullet();
        }
        else
        {

            if (canFollow)
            {
                //updates the rotation based on the actual direction
                rotation.LookTo(direction);
                //movement following the actual directiong
                if (movement.Move(direction, bulletShot.speed, canFollow))
                {
                    DeleteBullet(); 
                }
            }
            else
            {
                //Lo movemos con el ejeZ
                movement.Move(transform.forward.normalized, bulletShot.speed, canFollow);
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, direction);
    //}


    /// <summary>
    /// interact with another trigger...
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        /*
         * la bala ignora si tienen el mismo tag
         * la bala ignora si la etiqueta no es una de las esperadas
         * la bala siempre se destruye si choca con una etiqueta esperada
         * 
         * 
         */

        //bullets only interact with those tags
        string[] possibleTags ={"obstacle", "enemy", "player", "ally"};
        //the tag who gonna be detected
        string entryTag = other.transform.tag;
        //wheter is the bullet need to be destroyed
        bool destroyBullet = IsTag(entryTag, possibleTags);//Sleeping Games

        // If is true then destroy the bullet
        if (destroyBullet)
        {
            //PrintX($"[{bulletShot.owner}]-{name} is triggering with tag [{entryTag}] - {other.name}");
            if (!entryTag.Equals("obstacle"))
            {
                switch (bulletShot.owner)
                {
                    case CharacterType.ALLY:
                    case CharacterType.PLAYER:
                        if (!IsTag(entryTag, "enemy")) return;

                        ShotMinion(other.transform);
                        break;
                    case CharacterType.ENEMY:
                        if (!IsTag(entryTag, "player", "ally")) return;

                        if (entryTag.Equals("player"))
                        {
                            ShotPlayer(other.transform);
                        }
                        else
                        {
                            ShotMinion(other.transform);
                        }
                        break;
                    default:
                        //otro..... 
                        break;
                }
            }

            DeleteBullet();

        }

    }
    private void OnDisable()
    {
        part_trail.transform.parent = TargetManager.GetLeftoverContainer();
        Destroy(part_trail, part_trail.time);

        part_contact.transform.parent = TargetManager.GetLeftoverContainer();
        part_contact.Play();
        Destroy(part_contact.gameObject, part_contact.main.duration);

    }
    #endregion
    #region Methods


    /// <summary>
    /// Inflict damage to a minion
    /// </summary>
    private void ShotMinion(Transform tr_minion)
    {
        Minion minion = tr_minion.GetComponent<Minion>();

        if (minion)
        {
            minion.character.timeLife -= bulletShot.damage;
            minion.body.AddForce(transform.forward * 5, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("No tiene asignado el Minion component");
        }
    }

    private void ShotPlayer(Transform tr_player)
    {
        PlayerController player = tr_player.GetComponent<PlayerController>();
        player.character.timeLife -= bulletShot.damage;
        player.body.AddForce(transform.forward * 5, ForceMode.Impulse);

    }




    public void DeleteBullet()
    {
        destructure.DestructureThis();
        Destroy(gameObject);
    }

    /// <summary>
    /// Set the direction 
    /// </summary>
    public void SetDirection(Transform trans) {
        rotation.SetDirection(trans.rotation);
        direction = trans.position;
    }

    /// <summary>
    /// Preguntamos si ha pasado el rango desde el punto inicial con el actual
    /// </summary>
    private bool PassedRange() => Vector3.Distance(initPos, transform.position) > bulletShot.range;



    /// <summary>
    ///  Checks for on of the tags
    /// </summary>
    private bool IsTag(string tag, params string[] tags) => XavLib.XavHelpTo.Know.IsEqualOf(tag, tags);
    #endregion
}