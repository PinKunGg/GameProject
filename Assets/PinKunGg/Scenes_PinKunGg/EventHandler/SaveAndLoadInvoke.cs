using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveAndLoadInvoke : MonoBehaviour
{
    public static SaveAndLoadInvoke SALIKinstanse;
    UnityEvent Saving = new UnityEvent();
    UnityEvent Loading = new UnityEvent();

    private void OnApplicationQuit()
    {
        AutoSaveData();
    }
    private void Awake()
    {
        if(SALIKinstanse == null)
        {
            SALIKinstanse = this;
        }    
    }
    private void Start()
    {
        Invoke("StartLoadingData",0.01f);
    }
    private void StartLoadingData()
    {
        LoadingData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            SavingData();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            LoadingData();
        }
    }
    public void AddSavingEventLisener(UnityAction lisener) 
    {
        Saving.AddListener(lisener);
    }
    public void AddLoadingEventLisener(UnityAction lisener) 
    {
        Loading.AddListener(lisener);
    }

    private void SavingData() 
    {
        Saving.Invoke();
    }
    private void LoadingData()
    {
        Loading.Invoke();
    }
    public void AutoSaveData()
    {
        print("- AutoSave -");
        Saving.Invoke();
        print("- AutoSave Complete -");
    }
}
