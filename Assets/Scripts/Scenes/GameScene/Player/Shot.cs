#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavLib;
#endregion

public class Shot : MonoX
{
    #region
    [Header("ShotSettings")]
    public Transform parent_bullet;
    public Bullet pref_bullet;
    [Space] // creación
    public bool canShot;
    public float timer_bullet;private float timeCount_bullet;

    #endregion
    #region Events
    private void Update(){
        // si no puede disparar y termina su vuelta de tiempo... (el tiempo no corre si pudiese disparar
        CanPassedTime(ref canShot, ref timeCount_bullet, timer_bullet);
    }
    #endregion
    #region


    /// <summary>
    /// Creamos una bala y se le coloca los parametros basado en <see cref="Shot"/>
    /// </summary>
    public void ShotBullet(BulletShot _bulletShot){
        if (!canShot) return;
        canShot = false;

        Bullet newBullet = Instantiate(pref_bullet, transform.position, transform.rotation, parent_bullet);
        newBullet.direction = Vector3.Normalize(transform.rotation * Vector3.forward);
        newBullet.bulletShot = _bulletShot;
    }
    #endregion
}
/// <summary>
/// Información acerca de los disparo de balas
/// </summary>
public struct BulletShot {

    public float speed;
    public float range;
    public float damage;

    public BulletShot(float speed, float range, float damage)
    {
        this.speed  = speed;
        this.range  = range;
        this.damage = damage;
    }
}