using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PopUpInfo
{
    public string text;
    public Sprite icon;
}

public class PopUpController : MonoBehaviour
{
    public static PopUpController instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform inactivePos;
    public Transform activePos;

    [SerializeField]
    float duration;

    [SerializeField]
    float popupProgress;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    bool popupInProgress;

    [SerializeField]
    AlmanacProgression alamanac;

    [SerializeField]
    TextMeshProUGUI textMesh;

    [SerializeField]
    Image iconHolder;

    List<PopUpInfo> queue;

    public void NewPopUp(string text, Sprite sprite)
    {
        PopUpInfo info = new PopUpInfo();
        info.icon = sprite;
        info.text = text;
        if(!popupInProgress)
        {
            StartPopUp(info);
        }
        else
        {
            queue.Add(info);

        }
    }

    public void StartPopUp(PopUpInfo p)
    {
        popupInProgress = true;
        textMesh.text = p.text;
        iconHolder.sprite = p.icon;
    }

    public void NewPopUp(PopUp p)
    {
        if (!popupInProgress)
        {
            popupInProgress = true;
            textMesh.text = p.text;
        }

    }

    public void LerpToActive()
    {
        popupProgress += Time.deltaTime;
        transform.position = Vector3.Lerp(inactivePos.position, activePos.position, popupProgress * moveSpeed);
        
    }

    public void LerpToInactive()
    {
        transform.position = Vector3.Lerp(activePos.position, inactivePos.position, (popupProgress * moveSpeed)-duration);
        if (popupInProgress && (transform.position == inactivePos.transform.position || (popupProgress > duration*2)))
        {
            popupInProgress = false;
            popupProgress = 0;
        }
    }

    public void UpdatePopup()
    {
        if(popupInProgress)
        {
            popupProgress += Time.deltaTime;
            if(popupProgress <= duration)
            {
                LerpToActive();
            }
            else
            {
                LerpToInactive();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        queue = new List<PopUpInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePopup();

        if(!popupInProgress && queue.Count != 0)
        {
            StartPopUp(queue[0]);
            queue.RemoveAt(0);
        }
    }
}
