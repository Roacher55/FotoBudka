using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class PhotoBoothController : MonoBehaviour
{
    List<GameObject> inputs = new List<GameObject>();
    List<GameObject> spawnedObjects = new List<GameObject>();
    object[] objects;

    static int currentIndex = 0;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject canvas;
    private void Awake()
    {
        LoadAssets();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.fieldOfView >5)
        {
            Camera.main.fieldOfView--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.fieldOfView < 100)
        {
            Camera.main.fieldOfView++;
        }

    }
    private void LoadAssets()
    {
        foreach (var item in Resources.LoadAll<GameObject>("Input"))
        {
            inputs.Add(item);
        }
        SpawnObjects();
        SelectObject(currentIndex); 
    }

    public void Back()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = inputs.Count - 1;

        SelectObject(currentIndex);
    }

    public void Photo()
    {
        string fileName = "screenShot_" + System.Guid.NewGuid() + ".png";
        canvas.SetActive(false);
        ScreenCapture.CaptureScreenshot("Assets/Resources/Output/" + fileName);
        StartCoroutine(CaptureScreenShot());
    }

    IEnumerator CaptureScreenShot()
    {
        yield return new WaitForEndOfFrame();
        canvas.SetActive(true);
    }

    public void Forward()
    {
        currentIndex++;
        if (currentIndex == inputs.Count)
            currentIndex = 0;

        SelectObject(currentIndex);
    }

    void SpawnObjects()
    {
        foreach (var item in inputs)
        {
           var i = Instantiate(item, spawnPoint);
            i.SetActive(false);
            spawnedObjects.Add(i);
        }
    }

    void SelectObject(int index)
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            spawnedObjects[i].SetActive(i==index);
        }
    }

    
}