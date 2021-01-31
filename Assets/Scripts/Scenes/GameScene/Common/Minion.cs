#region Imports
using UnityEngine;
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
    protected bool isDead = false;
    protected bool isEnemyBoss = false;
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

        target = transform;
    }

    /// <summary>
    /// Updates the basics of the minions and then returns true wether is alive 
    /// </summary>
    public bool UpdateMinion(){
        bool keepGoing = false;
        //if is on game and the minion is near of the floor then....
        if (GameManager.IsOnGame() && movement.IsOnFloor())
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

        isDead = true;
        destructure.DestructureThis();
        Destroy(gameObject);
    }

   
    #endregion
}