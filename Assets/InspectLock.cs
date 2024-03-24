using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectLock : MonoBehaviour
{
    private float x, y;
    private float d1x, d2x ,d1y, d2y, vx, vy;
    public float dirx, diry;
    private bool firsttoggle;
    public Transform theCam;
    public Vector3 came, transe;

    public Transform[] clockfaces;
    public Transform selected;
    public int selectIndex;
    public int[] clockfaces_value;
    private bool roll;
    public float roll_time;
    private float valueWhenRolled;
    // Start is called before the first frame update
    void Start()
    {
        theCam = GameObject.FindWithTag("MainCamera").transform;
        x = Screen.width;
        y = Screen.height;

        for(int i=0;i<5;i++)
        {
            clockfaces[i] = transform.GetChild(i+1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = theCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.transform.tag == "ClockFace")
                {
                    for(int i=0;i<5;i++)
                    {
                        if(hit.transform.name == clockfaces[i].name)
                        {
                            if(selected == clockfaces[i])
                            {
                                selected = null;
                                selectIndex = 5;
                            }
                            else
                            {
                                selected = clockfaces[i];
                                selectIndex = i;
                            }
                            i=5;
                        }
                    }
                }
            }
        }

        if(Input.GetKeyDown("i"))
        {
            if(selected != null)
            {
                increase();
            }
            else
            {
                Debug.Log("nothing selected");
            }
            
        }
        else if(Input.GetKeyDown("k"))
        {
            if (selected != null)
            {
                decrease();
            }
            else
            {
                Debug.Log("nothing selected");
            }
        }
    }

    void increase()
    {
        if(clockfaces_value[selectIndex] == 9)
        {
            clockfaces_value[selectIndex] = 0;
        }
        else
        {
            clockfaces_value[selectIndex]++;
        }
        selected.GetComponent<Roll>().roll(clockfaces_value[selectIndex]);
    }

    void decrease()
    {
        if (clockfaces_value[selectIndex] == 0)
        {
            clockfaces_value[selectIndex] = 9; 
        }
        else
        {
            clockfaces_value[selectIndex]--;
        }
        selected.GetComponent<Roll>().roll(clockfaces_value[selectIndex]);
    }

}
