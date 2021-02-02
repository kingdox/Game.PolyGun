public class DrainDamage : MonoX
{
    #region variable
    //revisa si hacer el drenado si se destruye...
    private Bullet bullet;

    #endregion
    #region event
    private void Awake()
    {
        Get(out bullet);
        bullet.ActionShotSucces += Drain;
    }
    #endregion
    #region method

    /// <summary>
    /// Drena al objetivo en caso de seguir existiendo
    /// </summary>
    private void Drain(){
        if (bullet.tr_owner != null)
        {
            //de momento esto solo se hace para  Mond Boss, asi que no requeremos generarlizar cosa
            //todo si quisieramos cambiarlo aqui ta
            Minion mondBoss = bullet.tr_owner.GetComponent<Minion>();
            //infligimos el daño
            mondBoss.character.timeLife += bullet.bulletShot.damage;

        }
    }

    #endregion
}