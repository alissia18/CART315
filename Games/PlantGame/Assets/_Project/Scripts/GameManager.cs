using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public KeyCode restartKey = KeyCode.R;
    public PlayerController playerController;
    public int magicLeft = 0;
    public bool itemCollected {get; private set;}

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        itemCollected = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(restartKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CollectItem()
    {
        itemCollected = true;
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
