using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneRestart : MonoBehaviour
{
    public int sceneToLoad = 0;
    // Start is called before the first frame update

    public void SceneLoader()
    {
        SceneManager.LoadScene(sceneToLoad);

    }

}
