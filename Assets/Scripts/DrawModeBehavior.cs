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
    public float moveDistance = 10f;
    private List<GameObject> points = new List<GameObject>();
    private Vector3 input, originalPos;
    
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

        Vector3 cursorPos = new Vector3(playerPos.position.x, playerPos.position.y + 5, playerPos.position.z);
        GameObject cursor = Instantiate(cursorPrefab, cursorPos, transform.rotation) 
            as GameObject;
    }

    void Update() {
        if(drawMode) {
            GameObject cursor = GameObject.FindGameObjectWithTag("Cursor");

            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            originalPos = cursor.transform.position;
            //make it local to the cursor
            //input = (cursor.transform.right * moveHorizontal + cursor.transform.forward * moveVertical).normalized;

            // MOVE RIGHT
            if(moveHorizontal > 0.5) {
                cursor.transform.position = new Vector3(cursor.transform.position.x + moveDistance, 
                cursor.transform.position.y, cursor.transform.position.z);
                
                if(originalPos.x + 0.1 < cursor.transform.position.x) { 
                    GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                        as GameObject;

                    points.Add(spawnedPoint);
                    originalPos = cursor.transform.position;
                }
            }

            // MOVE LEFT
            if(moveHorizontal < -0.5) {
                cursor.transform.position = new Vector3(cursor.transform.position.x - moveDistance, 
                cursor.transform.position.y, cursor.transform.position.z);
                
                if(originalPos.x + 0.1 > cursor.transform.position.x) { 
                    GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                        as GameObject;

                    points.Add(spawnedPoint);
                    originalPos = cursor.transform.position;
                }
            }

            // MOVE UP
            if(moveVertical > 0.5) {
                cursor.transform.position = new Vector3(cursor.transform.position.x, 
                cursor.transform.position.y, cursor.transform.position.z + moveDistance);
                
                if(originalPos.z + 0.1 > cursor.transform.position.z) { 
                    GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                        as GameObject;

                    points.Add(spawnedPoint);
                    originalPos = cursor.transform.position;
                }
            }

            // MOVE DOWN
            if(moveVertical < -0.5) {
                cursor.transform.position = new Vector3(cursor.transform.position.x, 
                cursor.transform.position.y, cursor.transform.position.z - moveDistance);
                
                if(originalPos.z + 0.1 < cursor.transform.position.z) { 
                    GameObject spawnedPoint = Instantiate(pointPrefab, cursor.transform.position, transform.rotation)
                        as GameObject;

                    points.Add(spawnedPoint);
                    originalPos = cursor.transform.position;
                }
            }
        }
    }
}
