using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{   
    public StateManager brain;
    public abstract State RunCurrentState();

}
