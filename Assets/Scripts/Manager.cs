using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Play,
    Win, 
    Failed
}
public class Manager : MonoBehaviour
{
    public static Manager singleton;
    public State gameState;
    private void Awake()
    {
        singleton = this;
    }
}
