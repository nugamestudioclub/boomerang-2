using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawModeBehavior : MonoBehaviour
{
    public GameObject cursorPrefab;
    public Transform playerPos;
    public GameObject pointPrefab;
    private UIDocument root;
    private Button drawButton; 
    private bool drawMode;
    private float totalDrawDistance;
    private float moveDistance;
    
    void Start()
    {
        drawMode = false;
        root = GetComponent<UIDocument>();
        drawButton = root.rootVisualElement.Q("Draw") as Button;
        drawButton.RegisterCallback<ClickEvent>(onDraw);

        if(cursorPrefab == null) {
            cursorPrefab = GameObject.FindGameObjectWithTag("Cursor");
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
        Vector3 pos3DView = new Vector3(16.9f, 10.6f, -28.3f);
        Vector3 pos2DAboveView = new Vector3(16.9f, 10.6f, 20.6f);

        //lerp to this position!!!

        //cameraPos = new Vector3();
        Camera.main.transform.position = pos2DAboveView;
        Camera.main.transform.Rotate(83, 0, 0);
        Camera.main.orthographic = true;

        GameObject cursor = Instantiate(cursorPrefab, playerPos.position, transform.rotation) 
            as GameObject;
    }

    void Update() {
        // if(drawMode) {
        //     if(Input.GetButton("W")) {
        //         totalDrawDistance += moveDistance;
        //         // UP
        //         // increase z
        //         cursor.transform = new Vector3(cursor.transform.x, 
        //             cursor.transform.y, cursor.transform.z + moveDistance);
                
        //         GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
        //             as GameObject;
        //     }

        //     if(Input.GetButton("S")) {
        //         totalDrawDistance += moveDistance;
        //         // DOWN
        //         // decrease z
        //         Transform t = cursor.transform;
        //         t.position = new Vector3(t.position.x, 
        //             t.position.y, t.position.z - moveDistance);
        //         GameObject spawnedPoint = Instantiate(pointPrefab, t.position, transform.rotation)
        //             as GameObject;
        //     }

        //     if(Input.GetButton("A")) {
        //         totalDrawDistance += moveDistance;
        //         // LEFT
        //         // decrease x
        //         Transform t = cursor.transform;
        //         t.position = new Vector3(t.position.x - moveDistance, 
        //             t.position.y, t.position.z);
        //         GameObject spawnedPoint = Instantiate(pointPrefab, t.position, transform.rotation)
        //             as GameObject;
        //     }

        //     if(Input.GetButton("D")) {
        //         totalDrawDistance += moveDistance;
        //         // RIGHT
        //         // increase x
        //         Transform t = cursor.transform;
        //         t.position.transform = new Vector3(t.position.x + moveDistance, 
        //             t.position.y, t.position.z);
        //         GameObject spawnedPoint = Instantiate(pointPrefab, t.position.position, transform.rotation)
        //             as GameObject;
        //     }
        // }
    }
}
