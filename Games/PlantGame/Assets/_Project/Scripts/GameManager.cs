using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public int magicLeft = 0;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMagic(int change)
    {
        if (magicLeft == 0 && change > 0)
        {
            playerController.ActivateMagic();
        }
        magicLeft += change;
        if (magicLeft == 0)
        {
            playerController.DeactivateMagic();
        }
    }
}
