using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Conocimiento extraído de :
// https://en.wikipedia.org/wiki/Singleton_pattern#:~:text=In%20software%20engineering%2C%20the%20singleton,mathematical%20concept%20of%20a%20singleton.

///<sumary>
///Hereda al MonoBehaviour y se encarga de
///iniciar su instanciacion o destrucción
///</sumary>
/////TODO este tiene el problema de que solo poseerá una instancia unica....
public class MonoSingleton : MonoBehaviour, ISingleton
{
    public static MonoSingleton Instance { get; private set; }

    private void Awake(){Instant();}

	public void Instant(){
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

	}
    
}
public interface ISingleton
{
    /// <summary>
    /// Revisaremos si el objeto ya posee una instancia
    /// </summary>
    void Instant();
}