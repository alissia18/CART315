using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onPlayerLifeUpdate+= OnPlayerLifeUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.Instance.onPlayerLifeUpdate -= OnPlayerLifeUpdate;
    }

    public void OnPlayerLifeUpdate()
    {
        this.GetComponent<Image>().fillAmount = GameManager.Instance.lives * 0.166f;;
        Debug.Log(GetComponent<Image>().fillAmount);
    }
}
