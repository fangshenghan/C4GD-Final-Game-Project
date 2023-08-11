using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI interactHintText;
    private string[] dialogueTmp;
    public string[] dialogue1, dialogue;
    private int index;
    private bool learned = false;
    public bool isWizard = false;
    

    public GameObject continueBtn, winningText;
    public float wordSpeed;
    public bool playerInRange;
    
    void Start(){
        HideTextObject();
        if(StaticVars.cutsceneDone && !isWizard){
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if(playerInRange && !learned){
            ShowTextObject();
        }
        /*
        if(!playerInRange){
            HideTextObject();
        }
        */
        if(Input.GetKeyDown(KeyCode.Q) && playerInRange){
            learned = true;
            HideTextObject();
            if(dialoguePanel.activeInHierarchy){
                resetText();
            }
            else{
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if(isWizard && GunEvent.ableToChangeMode){
            dialogueTmp = dialogue1;
        }else{
            dialogueTmp = dialogue;
        }
        
        if(dialogueText.text == dialogueTmp[index]){
            continueBtn.SetActive(true);
        }
    }

    public void setLearned(bool l){
        learned = l;
    }

    public void NextLine(){
        continueBtn.SetActive(false);
        
        if(index < dialogue.Length - 1){
            index ++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else{
            resetText();
            if(isWizard){
                if(GunEvent.ableToChangeMode){
                    BoatScript.instance.setTargetAlpha(1F);
                    winningText.SetActive(true);
                    BoatScript.disableMovement = true;
                }
            }else{
                BoatScript.instance.setTargetAlpha(1F);
                StartCoroutine(playCutscene());
            }
        }
    }

    IEnumerator playCutscene(){
        yield return new WaitForSeconds(1.5F);
        StaticVars.cutsceneDone = true;
        SceneManager.LoadScene("DavidDev GivePackage Cutscene");
    }

    IEnumerator Typing(){
        foreach(char letter in dialogueTmp[index].ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void resetText(){
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("player")){
            playerInRange = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("player")){
            HideTextObject();
            playerInRange = false;
            resetText();
        }   
    }

    

    // Call this method to hide the TextMeshProUGUI text
    public void HideTextObject()
    {
        Color textColor = interactHintText.color;
        textColor.a = 0f; // Set the alpha value to 0 to make it transparent
        interactHintText.color = textColor;
    }

    // Call this method to show the TextMeshProUGUI text
    public void ShowTextObject()
    {
        Color textColor = interactHintText.color;
        textColor.a = 1f; // Set the alpha value to 1 to make it fully visible
        interactHintText.color = textColor;
    }
}

