using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameManager : MonoBehaviour
{

    public ObjectSpawner spawner;
    public Transform cam;
    public float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            Vector3 pos = cam.position + cam.forward.normalized * distanceFromCamera;
            spawner.TrySpawnObject(pos, cam.forward);
        }
    }
}
