using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveObjects : MonoBehaviour
{
    public static SaveObjects instance;
    public List<GameObject> plantsToSave;
    public List<string> planters;
    public int timeLeftAt; //total elapsed hours at time of leaving farming scene

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    public void LoadNewScene(int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Save();


        SceneManager.LoadScene(index);
        if (index == 0)
        {
            Load();
            //Maybe also apply something
        }
    }

    public void Save()
    {
        timeLeftAt = SunRotationScript.instance.GetTotalElapsedTime();
        Debug.Log("Left at " + timeLeftAt);
        plantsToSave.AddRange(GameObject.FindGameObjectsWithTag("Plant"));
        planters.Clear();
        //for (int i = 0; i < plantsToSave.Count; i++) this doesnt work
        //{
        //    planters.Add(plantsToSave[i].transform.parent.name);
        //}

        foreach(GameObject g in plantsToSave)
        {
            g.transform.parent = this.gameObject.transform;
            g.SetActive(false);
        }
    }

    public void Load()
    {
        for (int i = 0; i < plantsToSave.Count; i++)
        {
            plantsToSave[i].SetActive(true);
            plantsToSave[i].transform.parent = null;
            plantsToSave[i].GetComponent<Plant>().AddElapsedHours(SunRotationScript.instance.GetTotalElapsedTime() - timeLeftAt);
            Debug.Log("Returned at " + SunRotationScript.instance.GetTotalElapsedTime());

            //Instantiate(plantsToSave[i], GameObject.Find(planters[i]).transform, false);
        }
        //plantsToSave = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
