using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM GMinstanse;
    private bool isEsc;
    [SerializeField] private bool isPC;
    [SerializeField] private GameObject Cursor;
    public bool GetisPC
    {
        get
        {
            return isPC;
        }
    }
    private void Awake()
    {
        if(GMinstanse == null)
        {
            GMinstanse = this;
        }
    }
    private void Update()
    {
        if(isPC == true)
        {
            Cursor.SetActive(true);
        }
        else
        {
            if(BuildManager.BMinstanse.GetSetinBuildMode == false)
            {
                if(Cursor.activeInHierarchy == true)
                {
                    Cursor.SetActive(false);
                }
            }
            else
            {
                if(Cursor.activeInHierarchy == false)
                {
                    Cursor.SetActive(true);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isEsc = !isEsc;
        }

        if(isEsc)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
