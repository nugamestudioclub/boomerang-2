using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour {

    private UIDocument root; 
    private Button  startButton; 
    private Button  settingsButton; 
    private Button creditsButton;
    private Button exitButton;
    // Start is called before the first frame update
    void Start(){
        root = GetComponent<UIDocument>();
        startButton = root.rootVisualElement.Q("StartButton") as Button; 
        startButton.RegisterCallback<ClickEvent>(OnStart);   
        settingsButton = root.rootVisualElement.Q("SettingsButton") as Button; 
        settingsButton.RegisterCallback<ClickEvent>(OnSettings); 
        creditsButton = root.rootVisualElement.Q("CreditsButton") as Button; 
        creditsButton.RegisterCallback<ClickEvent>(OnCredits);    
        exitButton = root.rootVisualElement.Q("ExitButton") as Button; 
        exitButton.RegisterCallback<ClickEvent>(OnExit);  
         
        
    }


    private void onDisable(){
        startButton. UnregisterCallback<ClickEvent>(OnStart); 
        settingsButton. UnregisterCallback<ClickEvent>(OnSettings); 
        creditsButton. UnregisterCallback<ClickEvent>(OnCredits); 
        exitButton. UnregisterCallback<ClickEvent>(OnExit); 
    }

   

    private void OnStart(ClickEvent clickEvent){
            SceneManager.LoadScene("Level1");
    }

    private void OnSettings(ClickEvent clickEvent){
        SceneManager.LoadScene("Settings");
    }
   
     private void OnCredits(ClickEvent clickEvent){
        SceneManager.LoadScene("Credits");
    }

    private void OnExit(ClickEvent clickEvent){
        Application.Quit();
    }
}
