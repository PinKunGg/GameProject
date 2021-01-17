using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor MCInstanse;
    [SerializeField]GameObject LeftClickFx, RightClickFx;
    private void Awake()
    {
        if(MCInstanse == null)
        {
            MCInstanse = this;
        }    
    }
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
        if(GM.GMinstanse.GetisPC == true)
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z + 3f));
            transform.position = cursorPos; 
        }
        else
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,Screen.height / 2,7f));
            transform.position = cursorPos; 
        }

        if(Input.GetMouseButtonDown(0))
        {
            GameObject Fx = Instantiate(LeftClickFx,transform.position,Quaternion.identity);
            Destroy(Fx,0.3f);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            GameObject Fx = Instantiate(RightClickFx,transform.position,Quaternion.identity);
            Destroy(Fx,0.3f);
        }
    }
}
