using UnityEngine;

/// <summary>
/// Clase que permite crear instancias estaticas para manejadores, por ejemplo GameManage, AudioManger, etc
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Propiedad que permite comprobar si una instancia es nula o no
    /// </summary>
    /// <value><c>Verdadero</c> si esta inicializada; de otra manera, <c>false</c>.</value>
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        Debug.Log("[Singleton] Se creo una instancia de la clase Singleton " + this);

        if (instance != null)
            Debug.Log("[Singleton] Se esta tratando de crear una segunda instancia de la clase Singleton");
        else
            instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}