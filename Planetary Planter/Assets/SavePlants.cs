using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlants : MonoBehaviour
{
    public static SavePlants instance;
    public List<GameObject> plantsToSave;
    public List<string> planters;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public void LoadNewScene(int index)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            Save();


        SceneManager.LoadScene(index);
        if(index == 0)
        {
            Load();
            //Maybe also apply something
        }
    }

    public void Save()
    {
        plantsToSave.AddRange(GameObject.FindGameObjectsWithTag("Plant"));
        for(int i = 0; i < plantsToSave.Count; i++)
        {
            planters[i] = plantsToSave[i].transform.parent.name;
        }
    }

    public void Load()
    {
        for(int i = 0; i < plantsToSave.Count; i++)
        {
            Instantiate(plantsToSave[i], GameObject.Find(planters[i]).transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
