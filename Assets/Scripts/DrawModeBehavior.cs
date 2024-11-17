using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawModeBehavior : MonoBehaviour
{
    /*
    public GameObject cameraPos;
    public GameObject cursorPrefab;
    public transform playerPos;
    private UIDocument root;
    private Button drawButton; 
    private bool drawMode;
    private GameObject cursor;
    private float totalDrawDistance;
    private float moveDistance;
    
    void Start()
    {
        drawMode = false;
        cursor = null;
        root = GetComponent<UIDocument>();
        drawButton = root.rootVisualElement.Q("Draw") as drawButton;
        drawButton.RegisterCallback<ClickEvent>(onDraw);

        if(cameraPos == null) {
            cameraPos = GameObject.FindGameObjectWithTag("MainCamera");
        }

        if(cursor == null) {
            cursor = GameObject.FindGameObjectWithTag("Cursor");
        }
    }

    private void onDisable() {
        drawButton.UnregisterCallback<ClickEvent>(onDraw);
    }

    private void onDraw(ClickEvent clickEvent) {
        drawModeTurnsOn();
    }

    // UNCLICK FUNCTION ------------
    // drawMode = false;
    // move camera back to perspective
    // totalDrawDistance = 0;

    private void drawModeTurnsOn() {
        Debug.Log("Draw mode turned on.");
        drawMode = true;
        Vector3 pos3DView = new Vector3(-10.1f, 5.8f, -74.8f);
        Vector3 pos2DAboveView = new Vector3(-10.1f, 32, -7.7f);

        //lerp to this position!!!

        //3D to 2D
        //cameraPos.transform.Rotate(90, 0, 0);
        //Camera.main.orthographic = true;

        //2D to 3D 
        //cameraPos.transform.Rotate(-90, 0, 0);

        cameraPos = new Vector3();

        cursor = Instantiate(cursorPrefab, playerPos, transform.rotation) 
            as GameObject;
        

    }

    void Update() {
        if(drawMode) {
            if(Input.GetButton("W")) {
                totalDrawDistance += moveDistance;
                // UP
                // increase z
                cursor.transform = new Vector3(cursor.transform.x, 
                    cursor.transform.y, cursor.transform.z + moveDistance);
                
                GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                    as GameObject;
            }

            if(Input.GetButton("S")) {
                totalDrawDistance += moveDistance;
                // DOWN
                // decrease z
                cursor.transform = new Vector3(cursor.transform.x, 
                    cursor.transform.y, cursor.transform.z - moveDistance);
                GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                    as GameObject;
            }

            if(Input.GetButton("A")) {
                totalDrawDistance += moveDistance;
                // LEFT
                // decrease x
                cursor.transform = new Vector3(cursor.transform.x - moveDistance, 
                    cursor.transform.y, cursor.transform.z);
                GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                    as GameObject;
            }

            if(Input.GetButton("D")) {
                totalDrawDistance += moveDistance;
                // RIGHT
                // increase x
                cursor.transform = new Vector3(cursor.transform.x + moveDistance, 
                    cursor.transform.y, cursor.transform.z);
                GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                    as GameObject;
            }
        }
    }
    */
}
