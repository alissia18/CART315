using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int magicLeft = 0;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMagic(int change)
    {
        magicLeft += change;
    }
}
