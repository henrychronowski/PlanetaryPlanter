using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public CanvasGroup group;
    public Slider progressBar;
    List<AsyncOperation> currentOperations;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    float fadeProgress;
    bool fadingOut;
    public void NewOperation(AsyncOperation operation)
    {
        currentOperations.Add(operation);
        progressBar.value = 0;
        StartCoroutine(LoadingProgress());

    }

    IEnumerator LoadingProgress()
    {
        while (true)
        {

             //Progress bar updates
            if(currentOperations.Count == 0)
            {
                //Do nothing
                progressBar.value = 1;
                fadeProgress += Time.deltaTime;
                group.alpha = 1 - fadeProgress / fadeOutTime;
                if (group.alpha <= 0)
                {
                    group.alpha = 0;
                    break;
                }
                yield return null;

            }
            else if (!currentOperations[0].isDone)
            {
                fadeProgress += Time.deltaTime;
                group.alpha = fadeProgress / fadeInTime;
                progressBar.value = currentOperations[0].progress;
                yield return null;

            }
            else //Complete
            {
                currentOperations.RemoveAt(0);

                if (currentOperations.Count == 0)
                {
                    fadeProgress = 0;
                }
                yield return null;

            }
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        currentOperations = new List<AsyncOperation>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
