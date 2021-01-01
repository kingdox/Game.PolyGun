//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TestCosas : MonoTest
//{


//    //TODOhay que pasar un metodo de Monotest sin que sobrescriva
//    // y que se ejecute solo
//    public override void Del()
//    {
//    }

//    public void Invoke() {
//    }

//}
//#region D
//#endregion
//public abstract class MonoTest : MonoBehaviour, ITest
//{
//    public delegate void Del2();
//    protected Del2 Dels;

//    public int x;
//    public void Invoke() { }


//    //static void Main(){
//    //    Debug.Log("ss");
//    //}
//    public void P()
//    {

//    }

//    public abstract void Del();

//    private void Awake()
//    {
//        Dels += Del;
//        Debug.Log("test 1");
//        Dels.Invoke();
//        Del();
//        Debug.Log("test 2");

//        Dels();
//        Debug.Log("test 3");

//        //OnClicked += Test;
//        ////OnClicked += On

//        //OnClicked();
//        //MonoManager.OnClicked();
//    }
//}
//public interface ITest
//{
//    void P();

//}
//public struct TestStruct
//{
//    public void Pepe() { }

//}

//public class A
//{
//    public virtual void DoWork() { }
//}
//public class B : A
//{
//    //public override void DoWork() { }
//    public override void DoWork()
//    {
//        // Call DoWork on base class
//        base.DoWork();
//    }
//}
//public class C : B
//{
//    public sealed override void DoWork() { }
//}
//public class D : C
//{
//    public new void DoWork() { }
//}
//public class E : D
//{
//    //public void DoWork() { }
   
//}



////public UnityEvent whoa;

////protected delegate void MultiDelegate();
////protected MultiDelegate myMultiDelegate;

////public override void MultiDelegate2();

////public abstract void S();

////private abstract void TT()
////{

////}
////virtual void TT()
////{

////}

////[DllImport("avifil32.dll")]
////private static extern void AVIFileInit();

//#if false
//    private int num;
//    public virtual int Number
//    {
//        get { return num; }
//        set { num = value; }
//    }
//    public delegate void MultiDelegate();
//    public MultiDelegate myMultiDelegate;

//    public event Action<int> DoorTriggerEnter;
//    public event Action<int> DoorTriggerExit;

//    public void OnDoorTriggerEnter(int instanceId)
//    {
//        DoorTriggerEnter?.Invoke(instanceId);
//    }
//    //public UnityEvent whoa;

//    void Start()
//    {
//        //Analytics.cus

//        myMultiDelegate += PowerUp;
//        myMultiDelegate += TurnRed;

//        if (myMultiDelegate != null)
//        {
//            myMultiDelegate();
//        }
//    }

//    //public extern delegate void PowerUp();

//    void TurnRed()
//    {
//    }

//#endif
//#if false
//    ////TODO crear un evento para INIT
//    public delegate void Action();
//    ///// <summary>
//    ///// sss
//    ///// </summary>
//    //public static event ClickAction OnClicked;
//    //private Action myAction = () => { };
//    //public static void Main(){ }

//#region TEST
//    private Action myAction = () => { };

//    void Start()
//    {
//        System.Type ourType = this.GetType();

//        MethodInfo mi = ourType.GetMethod("MyMethod", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

//        if (mi != null)
//        {
//            myAction = (Action)Delegate.CreateDelegate(typeof(Action), this, mi);
//        }
//    }
//#endregion
//#endif