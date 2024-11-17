using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private UIDocument root; 
    private Button  Level1; 
    private Button  Level2; 
    private Button Level3;

    // Start is called before the first frame update
    void Start(){
        root = GetComponent<UIDocument>();
        Level1 = root.rootVisualElement.Q("Level1") as Button; 
        Level1.RegisterCallback<ClickEvent>(onLevel1);   
        Level2 = root.rootVisualElement.Q("Level2") as Button; 
        Level2.RegisterCallback<ClickEvent>(onLevel2); 
        Level3 = root.rootVisualElement.Q("Level3") as Button; 
        Level3.RegisterCallback<ClickEvent>(onLevel3);     
         
        
    }


    private void onDisable(){
        Level1.UnregisterCallback<ClickEvent>(onLevel1); 
        Level2.UnregisterCallback<ClickEvent>(onLevel2); 
        Level3.UnregisterCallback<ClickEvent>(onLevel3); 
         
    }

   

    private void onLevel1(ClickEvent clickEvent){
     SceneManager.LoadScene("Scadoomerang");
    }

    private void onLevel2(ClickEvent clickEvent){
        SceneManager.LoadScene("Level2");
    }
   
     private void onLevel3(ClickEvent clickEvent){
        SceneManager.LoadScene("Level2");
    }

   
}
