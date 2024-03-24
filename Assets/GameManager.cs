using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameManager : MonoBehaviour
{

    public ObjectSpawner spawner;
    public Transform cam;
    public float distanceFromCamera;
    private GameObject theLockInspector;
    public GameObject theLockInspectorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            if(theLockInspector == null)
            {
                //instantiate lock
                theLockInspector = Instantiate(theLockInspectorPrefab, cam);
                theLockInspector.transform.localPosition = new Vector3(0, 0, distanceFromCamera);
            }
            else
            {
                //destroy it
                Destroy(theLockInspector);
            }
            
        }
    }
}
