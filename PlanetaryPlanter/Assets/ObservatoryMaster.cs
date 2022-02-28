using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Holds the public function to enter the observatory and handles constellation selection UI
public class ObservatoryMaster : MonoBehaviour
{
    public GameObject playerCam;
    public bool inObservatoryView;

    public AudioSource telescope;
    public AudioSource main;
    public List<Transform> observatoryPoints;
    public List<bool> unlockedConstellations;
    public float lerpTime;
    public int observatoryCamTransformIndex;
    public bool lerping;
    private Vector3 lerpTarget;
    private Vector3 lerpOrigin;
    private float timeSpentLerping;

    public Button leftButton;
    public Button rightButton;

    public enum Direction
    {
        Left,
        Right
    }
    public void EnterObservatory()
    {
        AlmanacProgression.instance.Unlock("ObservatoryEnter");

        if (!inObservatoryView)
        {
            playerCam.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
            inObservatoryView = true;
            TutorialManagerScript.instance.Unlock("The Telescope");
            telescope.Play();
            main.mute = true;
        }
        else
        {
            playerCam.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
            inObservatoryView = false;
            telescope.Stop();
            main.mute = false;
        }
    }

    public void UnlockConstellation(int index)
    {
        unlockedConstellations[index] = true;
    }

    public void ChangeTargetObservatory(int dir)
    {
        if(dir == 1)
        {
            StartLerp(observatoryPoints[observatoryCamTransformIndex].position, observatoryPoints[observatoryCamTransformIndex + 1].position);
            observatoryCamTransformIndex++;
        }   
        else
        {
            
            StartLerp(observatoryPoints[observatoryCamTransformIndex].position, observatoryPoints[observatoryCamTransformIndex - 1].position);
            observatoryCamTransformIndex--;
        }
    }

    void ChangeConstellationButtonStatus()
    {
        if(observatoryCamTransformIndex - 1 < 0 || lerping)
        {

            leftButton.interactable = false;

        }
        else
        {
            leftButton.interactable = true;

        }

        if(observatoryCamTransformIndex + 1 >= observatoryPoints.Count || lerping)
        {
            rightButton.interactable = false;

        }
        else
        {
            if(unlockedConstellations[observatoryCamTransformIndex + 1])
                rightButton.interactable = true;
            else
                rightButton.interactable = false;

        }
    }

    void StartLerp(Vector3 origin, Vector3 target)
    {
        lerpOrigin = origin;
        lerpTarget = target;
        lerping = true;
    }

    void LerpUpdate()
    {
        if(lerping)
        {
            float t = Mathf.SmoothStep(0, 1, timeSpentLerping/lerpTime);
            transform.position = Vector3.Lerp(lerpOrigin, lerpTarget, t);
            timeSpentLerping += Time.deltaTime;
            if(timeSpentLerping>= lerpTime)
            {
                timeSpentLerping = 0;
                transform.position = lerpTarget;
                lerping = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LerpUpdate();
        ChangeConstellationButtonStatus();
    }
}
