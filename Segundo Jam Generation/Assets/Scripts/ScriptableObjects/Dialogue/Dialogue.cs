using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    /// <summary>
    /// variable que almacena todos los dialogos. 
    /// </summary>
    [TextArea(3,10)]
    public string[] sequences;
}
