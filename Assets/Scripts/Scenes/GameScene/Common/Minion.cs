#region Imports
using UnityEngine;
using Environment;
using XavHelpTo.Build;
#endregion
//[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(SaveVelocity))]
[RequireComponent(typeof(Destructure))]
public abstract class Minion : MonoX
{
    #region Variables
    [Header("Minion Settings")]
    public Character character;
    public Transform target;
    public bool isEnemyBoss = false;
    protected bool isDead = false;
    [Space]
    //protected float actionTimeCount;
    //protected bool canAction;

    [Space]
    [Header("Requirements")]
    //public NavMeshAgent navMeshAgent;
    public Rigidbody body;
    protected Rotation rotation;
    protected Movement movement;
    private SaveVelocity saveVelocity;
    protected Destructure destructure;
    [Space]
    [Header("Sfx Area")]
    private SfxItem[] sfx_items;
    public Transform tr_sfxParent;
    //public AudioSource src_action;
    private enum Sfx
    {
        @Creation,
        @Action,//Atacar
        @Dead,
    }

    #endregion
    #region Methods
    protected void LoadMinion()
    {
        GetAdd(ref body);

        GetAdd(ref rotation);
        GetAdd(ref movement);
        GetAdd(ref destructure);
        //in case of not set
        GetAdd(ref saveVelocity);
        GetChilds(out sfx_items, tr_sfxParent);

        target = transform;
        sfx_items[Sfx.Creation.ToInt()].PlaySound();
    }

    /// <summary>
    /// Updates the basics of the minions and then returns true wether is alive 
    /// </summary>
    public bool UpdateMinion(){
        bool keepGoing = false;
        //if is on game and the minion is near of the floor then....
        if (GameManager.IsOnGame() && movement.IsOnFloor() && !isDead)
        {
            //rigidbody.WakeUp();
            if (character.IsAlive())
            {
                //pierde vida
                character.LessLife();
                keepGoing = true;
            }
            else
            {
                Delete();
            }
        }
      

        return keepGoing;
    }
    /// <summary>
    /// Check if the minion is on the range between he and the target
    /// </summary>
    protected bool IsInRange(float range = default){
        if (range.Equals(default)) range = character.range;
        float distance = Vector3.Distance(transform.position, target.position);
        //PrintX($"Distancia entre el {tag} {name} y {target.tag} {target.name} es de {distance}, con {range} de rango");
        return distance < range;    
    }

    /// <summary>
    ///  action to damage a minion
    /// </summary>
    /// <param name="minionInContact"></param>
    protected void MinionDamage(Minion minionInContact)
    {
        minionInContact.character.timeLife -= character.damage;
        sfx_items[Sfx.Action.ToInt()].PlaySound();

        //PrintX($"Minion Ataca! {tag}, {name}");

        //RECOIL
        int magnitude = 10;
        minionInContact.body.AddForce(transform.forward * magnitude, ForceMode.VelocityChange);
        body.AddForce(-transform.forward * magnitude * 2, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Destroy the ally
    /// </summary>
    public void Delete()
    {
        if (isDead) return;


        //90% de que se cree un objeto
        if (Random.Range(0,1f) < Data.data.pctMinionDeathItem)
        {
            Instantiate(
                ItemManager.GetRandomItemShape(),
                transform.position,
                Quaternion.identity,
                TargetManager.GetItemsContainer()
            );
        }
        sfx_items[Sfx.Dead.ToInt()].PlaySound();
        sfx_items[Sfx.Dead.ToInt()].EndSoundIn(TargetManager.GetLeftoverContainer());
        isDead = true;
        destructure.DestructureThis();
        Destroy(gameObject);

    }

    /// <summary>
    /// inflict damage to a minion
    /// </summary>
    public void MinionAttackMinion(Transform enemyInContact)
    {
        Minion minion = enemyInContact.GetComponent<Minion>();
        MinionDamage(minion);
    }

    /// <summary>
    /// Updates the flag attack when is  ready, depends of a timer and their char.atkspeed
    /// </summary>
    public void AttackUpdate(ref bool canDamage, ref float damageTimeCount)
    {
        if (!canDamage && Timer(ref damageTimeCount, character.atkSpeed))
        {
            canDamage = true;
        }
    }

    /// <summary>
    /// Returns true if the minion can attack
    /// </summary>
    public bool CanAttack(Transform tr, bool canDamage, string tag) => GameManager.IsOnGame() && canDamage && tr.CompareTag(tag);

    #endregion
}