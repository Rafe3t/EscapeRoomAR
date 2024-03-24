using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameManager : MonoBehaviour
{
    public ObjectSpawner spawner;
    public Transform cam;
    public float distanceFromCamera;
    private GameObject theLockInspector;
    public GameObject theLockInspectorPrefab;
    [SerializeField]
    private GameObject[] surfaces;
    [SerializeField]
    private List<GameObject> facingUpSurfaces;
    [SerializeField]
    private GameObject[] objectsToSpawn; 
    private int currentObjectToSpawn = 0;
    private int lockKeyCode = 0; 
    // Start is called before the first frame update
    void Start()
    {
        // puzzel and clues will start after scanning the room
        //Invoke("GetUsableSurfaces", 20f);
        Invoke("SpawnObject",10f);
        Invoke("SpawnObject",11f);
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
        Instantiate(objectsToSpawn[currentObjectToSpawn],PickRandomSurface(),quaternion.identity);
        if(currentObjectToSpawn == 1){
            lockKeyCode = int.Parse(GenerateFiveDigitNumber());
        }
        currentObjectToSpawn++;
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

    string GenerateFiveDigitNumber()
    {
        int randomNumber = UnityEngine.Random.Range(10000, 99999); // Generate a random 5-digit number
        GameObject.Find("LockCode").GetComponent<TextMeshProUGUI>().text = randomNumber.ToString();
        return randomNumber.ToString(); // Convert the number to a string and return it
        
    }
    
}
