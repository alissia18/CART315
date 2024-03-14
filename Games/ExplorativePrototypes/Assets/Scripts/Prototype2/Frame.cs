using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public InventoryManager inventory;
    public Picture currentPicture;
    public Transform pictureAnchor;
    public CinemachineVirtualCamera camera;
    public const int LOW_PRIORITY = 5;
    public const int HIGH_PRIORITY = 10;
    void Awake()
    {
        camera.m_Priority = LOW_PRIORITY;
    }

    void Start()
    {
        inventory = InventoryManager.Instance;
    }
    public void SetFrame()
    {
        camera.m_Priority = HIGH_PRIORITY;
    }
    
    public void UnsetFrame()
    {
        camera.m_Priority = LOW_PRIORITY;
    }
    
    [Button]
    public void PlacePicture()
    {
        Picture inventoryPic = inventory.RemoveFromInventory();
        PlacePicture(inventoryPic);
    }
    
    public void PlacePicture(Picture picture)
    {
        if (currentPicture != null) currentPicture.Pickup();
        currentPicture = picture;
        currentPicture.transform.SetParent(pictureAnchor, false);
        currentPicture.Place();
    }

   

    public void Reset() // Gets used whenever you add a component to an object for the first time, or when you click Reset which resets to default values.
    {
        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        if (camera)
        {
            camera.m_Priority = LOW_PRIORITY;
        }
    }
}
