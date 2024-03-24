using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameManager : MonoBehaviour
{

    public ObjectSpawner spawner;
    public Transform cam;
    public float distanceFromCamera;
    [SerializeField]
    private GameObject[] surfaces;
    [SerializeField]
    private List<GameObject> facingUpSurfaces;
    [SerializeField]
    private GameObject objectToSpawn; 
    // Start is called before the first frame update
    void Start()
    {
        // puzzel and clues will start after scanning the room
        //Invoke("GetUsableSurfaces", 20f);
        Invoke("SpawnObject",10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            Vector3 pos = cam.position + cam.forward.normalized * distanceFromCamera;
            spawner.TrySpawnObject(pos, cam.forward);
        }
        GetPressedOnObj();
    }

    private List<GameObject> GetUsableSurfaces(){
        GameObject TrackedSurfaces = GameObject.Find("Trackables");
        Vector3 VectorFUP = new Vector3(0,270,0);
        surfaces = new GameObject[TrackedSurfaces.transform.childCount];
        facingUpSurfaces.Clear();
        for (int i = 0; i < TrackedSurfaces.transform.childCount; i++)
        {
            surfaces[i] = TrackedSurfaces.transform.GetChild(i).gameObject;
            // Debug.Log("Child Object Name: " + surfaces[i].transform.localRotation.eulerAngles);
            // Debug.Log("is facing up: " + (surfaces[i].transform.localRotation.eulerAngles == VectorFUP));
            if (surfaces[i].transform.localRotation.eulerAngles == VectorFUP){
                facingUpSurfaces.Add(surfaces[i]);
            }
        }
        return facingUpSurfaces;
    }
    private void DeleteDestroyedFromList(){
        for (int i = facingUpSurfaces.Count - 1; i >= 0; i--)
        {
            if (facingUpSurfaces[i] == null)
            {
                // Object has been destroyed, remove it from the list
                facingUpSurfaces.RemoveAt(i);
            }
        }
    }
    private Vector3 PickRandomSurface(){
        return facingUpSurfaces[UnityEngine.Random.Range(0,facingUpSurfaces.Count)].transform.position;
    }
    private void SpawnObject(){
        GetUsableSurfaces();
        Instantiate(objectToSpawn,PickRandomSurface(),quaternion.identity);
        Debug.Log("random surface: " + PickRandomSurface());
    }

    private void GetPressedOnObj(){
        if (Input.GetMouseButtonDown(1)) // Change to Input.GetTouch if targeting mobile platforms
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Change to Input.GetTouch(0).position for mobile
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.transform.name);
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject == gameObject) // Check if the hit object is the 3D model
                {
                    // Implement your inspection behavior here
                    Debug.Log("Inspecting 3D model: " + hitObject.name);
                }
            }
        }
    }
    
}
