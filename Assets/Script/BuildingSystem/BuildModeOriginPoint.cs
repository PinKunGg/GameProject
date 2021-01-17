using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeOriginPoint : MonoBehaviour
{
    private float h,v;
    private float speed;
    private void Start()
    {
        speed = GameObject.Find("Player").GetComponent<PlayerMovement>().GetPlayerSpeed;    
    }
    void Update()
    {
        if(GM.GMinstanse.GetisPC == false)
        {
            if(BuildManager.BMinstanse.GetSetinBuildMode == true)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                this.transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0f);
            }
            else
            {
                Vector3 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,Screen.height / 2,7f));
                this.transform.position = cursorPos;
            }
        }
    }
}
