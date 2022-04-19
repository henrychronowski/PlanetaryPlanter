using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WelcomeTutorial : MonoBehaviour
{
    [SerializeField] Color incompleteColor;
    [SerializeField] Color finishedColor;
    [SerializeField] TextMeshProUGUI moveText;
    [SerializeField] bool moveComplete;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] bool jumpComplete;
    [SerializeField] TextMeshProUGUI openInventoryText;
    [SerializeField] bool openInventoryComplete;
    [SerializeField] TextMeshProUGUI openCraftingMenuText;
    [SerializeField] bool openCraftingMenuComplete;
    [SerializeField] TextMeshProUGUI openAlmanacText;
    [SerializeField] bool openAlmanacComplete;
    [SerializeField] TextMeshProUGUI collectItemText;
    [SerializeField] bool collectItemComplete;
    [SerializeField] TextMeshProUGUI shovelAttackText;
    [SerializeField] bool shovelAttackComplete;
    [SerializeField] float destroyDelayTime;
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            shovelAttackComplete = true;
            shovelAttackText.color = finishedColor;
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            moveComplete = true;
            moveText.color = finishedColor;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            openInventoryComplete = true;
            openInventoryText.color = finishedColor;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            openCraftingMenuComplete = true;
            openCraftingMenuText.color = finishedColor;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !NewInventory.instance.inventoryActive)
        {
            jumpComplete = true;
            jumpText.color = finishedColor;
        }
        if(NewInventory.instance.GetNumberOfItems() != 0)
        {
            collectItemComplete = true;
            collectItemText.color = finishedColor;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            openAlmanacComplete = true;
            openAlmanacText.color = finishedColor;
        }
    }

    void CheckCompletion()
    {
        if(shovelAttackComplete && moveComplete && openInventoryComplete 
            && openInventoryComplete && jumpComplete && collectItemComplete && openAlmanacComplete)
        {
            gameObject.AddComponent<DestroyDelay>().delay = destroyDelayTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckCompletion();
        if (SaveManager.instance.dataLoaded) //Prevents this tutorial from appearing on pressing load game
            Destroy(gameObject);
    }
}
