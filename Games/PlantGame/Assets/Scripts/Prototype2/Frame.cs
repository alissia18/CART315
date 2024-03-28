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
        currentPicture = pictureAnchor.GetComponentInChildren<Picture>();
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
        PlacePicture(inventory.RemoveFromInventory());
    }
    
    public void PlacePicture(Picture picture)
    {
        if (picture == null) return;
        picture.transform.SetParent(pictureAnchor, false);
        picture.Place();
        currentPicture?.OnPickup();
        currentPicture = picture;
    }

    public void PickupPicture()
    {
        if (currentPicture)
        {
            if (inventory.heldPicture)
            {
                PlacePicture();
            }
            else
            {
                currentPicture.OnPickup();
                currentPicture = null;
            }
        }
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
