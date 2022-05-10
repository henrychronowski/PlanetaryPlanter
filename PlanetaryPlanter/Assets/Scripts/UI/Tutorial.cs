using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class Tutorial : MonoBehaviour
{
    public enum TutorialType
    {
        Text,
        Gif,
        Comic
    }

    public bool isUnlocked;
    public string title;
    [TextArea(8, 50)]
    public string description;
    public bool isComic;
    public TutorialType type;
    public Sprite comicSprite;
    public Image comicCanvasImage;
    public GameObject comicTutorialCanvas;
    public GameObject tutorial;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI comicTitleText;
    public TextMeshProUGUI comicDescriptionText;
    public VideoClip gif;
    public GameObject gifCanvas;
    public VideoPlayer gifPlayer;
    public TextMeshProUGUI gifTitleText;
    public TextMeshProUGUI gifDescriptionText;

    public class TutorialManagerScript : UnityEvent { }
    public TutorialManagerScript tutorialEvent = new TutorialManagerScript();
    public TutorialManagerScript onTutorialEvent { get { return tutorialEvent; } set { tutorialEvent = value; } }

    public void TutorialEventTriggered()
    {
        if(!isUnlocked)
        {
            float deltaTime = Time.deltaTime;
            isUnlocked = true;
            AlmanacProgression.instance.Unlock(title);
            if(type == TutorialType.Comic)
            {
                comicTutorialCanvas.SetActive(true);
                comicTutorialCanvas.GetComponent<TutorialPopup>().FadeInStart();
                comicTutorialCanvas.GetComponent<TutorialPopup>().setDeltaTime = deltaTime;
                comicCanvasImage.sprite = comicSprite;
                comicDescriptionText.text = description;
                comicTitleText.text = title;
            }
            else if(type == TutorialType.Gif)
            {

                gifCanvas.SetActive(true);
                gifCanvas.GetComponent<TutorialPopup>().FadeInStart();
                gifCanvas.GetComponent<TutorialPopup>().setDeltaTime = deltaTime;

                gifPlayer.clip = gif;
                gifDescriptionText.text = description;
                gifTitleText.text = title;
            }
            else
            {
                tutorial.SetActive(true);
                tutorial.GetComponent<TutorialPopup>().FadeInStart();
                tutorial.GetComponent<TutorialPopup>().setDeltaTime = deltaTime; 
                descriptionText.text = description;
                titleText.text = title;
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
        
    }
}
