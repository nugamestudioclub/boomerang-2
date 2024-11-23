using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class DrawModeBehavior : MonoBehaviour
{
    private List<Vector3> m_points = new(); // gameobject isn't really necessary; we just want the positions
    private Vector3 LoggedMousePos
    {
        get => m_points[^1];
        set => m_points[^1] = value;
    }

    [SerializeField] private GameObject m_cursorPrefab; // if no other script uses a property, use serialize field instead of "public". Just OoD things.
    [SerializeField] private Transform m_playerPos;
    [SerializeField] private LineRenderer m_renderer;
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private AudioSource m_drawSource;

    [Space(10)]

    [SerializeField] private AnimationCurve m_colorCurve;
    [SerializeField] private Transform m_cameraBasicTransform;
    [SerializeField] private Transform m_cameraOrthoTransform;

    [Space(10)] // just to clean up the inspector a bit and separate component reference fields from primitive fields

    [SerializeField] private float m_cameraSlerpSpeed; // i dunno what this should be by default :>
    [SerializeField] private float m_moveDelta = 5f; // renamed from "moveDistance" for clarity
    [SerializeField] private float m_maxDrawDistance = 5f; // renamed from "totalDrawDistance" for clarity

    private Transform m_cursorInstanceTransform; // changed data type to Transform for simplicity and renamed field
    private Vector2 m_currentInput;
    private Vector2 m_previousInput; // changed "input" type to Vector2 and renamed
    private bool m_isInDrawMode = false; // booleans should usually be named as questions
    private float m_currentDistanceDrawn;

    private float m_tentativePositionDistance;


    void Start()
    {
        m_cursorInstanceTransform = Instantiate(m_cursorPrefab).transform;
        m_cursorInstanceTransform.position += Vector3.up * 2f;
        m_cursorInstanceTransform.gameObject.SetActive(false);
    }

    public bool ToggleDrawState()
    {
        m_isInDrawMode = !m_isInDrawMode;

        m_renderer.enabled = m_isInDrawMode;

        m_cursorInstanceTransform.gameObject.SetActive(m_isInDrawMode);
        m_cursorInstanceTransform.position = m_playerPos.position;

        m_points.Clear();
        m_currentDistanceDrawn = m_tentativePositionDistance = 0f;
        m_points.Add(m_playerPos.position);
        m_points.Add(m_cursorInstanceTransform.position);

        UpdateRenderer(true);

        Debug.Log($"Draw mode turned {(m_isInDrawMode ? "on" : "off")}.");

        // starts the animation for the camera transition.
        // generally, you should cache the result of calls to Camera.main since it can be expensive in larger scenes.
        // this is a game jam, so i dont really care :sunglasses:
        StopAllCoroutines(); // we only have 1 coroutine on this gameobject, so this method is fine
        StartCoroutine(IETransition(Camera.main.transform, m_isInDrawMode ? m_cameraOrthoTransform : m_cameraBasicTransform));

        return m_isInDrawMode;
    }

    private IEnumerator IETransition(Transform camera_transform, Transform to_transform)
    {
        int parity = m_isInDrawMode ? 1 : -1;

        if (m_isInDrawMode) m_drawSource.Play();

        // you could use a vector3 or a quat here, but since we're lerping two datatypes at the same time,
        // a float representing percent-completed is cleaner.
        float progress = 0f;

        while (progress < 0.99f)
        {
            var spherical_position = Vector3.Lerp(camera_transform.position, to_transform.position, progress);
            var spherical_rotation = Quaternion.Lerp(camera_transform.rotation, to_transform.rotation, progress);

            camera_transform.SetPositionAndRotation(spherical_position, spherical_rotation);

            progress += Time.deltaTime * m_cameraSlerpSpeed;

            yield return null;
        }

        camera_transform.SetPositionAndRotation(to_transform.position, to_transform.rotation);

        if (!m_isInDrawMode) m_drawSource.Stop();

        // Camera.main.orthographic = m_isInDrawMode;
    }

    private void Update()
    {
        // this is called a "guard statement". It does the same thing as wrapping a block of
        // code in an if (preventing some code from running), but is more terse and readable.
        if (!m_isInDrawMode) return;

        /*
         * Rather than doing individual checks for every input, i figured it'd be easier to just
         * check to see when the player's input changes. This works out because the only data that
         * matters for the boomerang's path is when it changes direction.
        */
        m_currentInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        float dot = Vector2.Dot(m_currentInput, m_previousInput);
        bool is_different_vector = dot < 0.9f && m_previousInput != Vector2.zero;

        // if we're heading in a new direction, lay down a point.
        if (is_different_vector && CanDrawMore())
        {
            m_currentDistanceDrawn += PlacePoint();
        }

        var target_position = 
            m_cursorInstanceTransform.position + 
            m_moveDelta * Time.deltaTime * new Vector3(m_currentInput.x, 0f, m_currentInput.y);


        // bc im too lazy to rework the code to have a different handling for this :sunglasses:
        m_drawSource.volume = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));


        m_cursorInstanceTransform.position = Vector3.MoveTowards(m_cursorInstanceTransform.position, target_position, m_moveDelta * Time.deltaTime);

        // set the distance from the most recently placed point to our cursor
        // we haven't updated the cursor in our list yet, so dont use m_points[^1];
        m_tentativePositionDistance = Vector3.Distance(m_cursorInstanceTransform.position, m_points[^2]);

        // if we can still draw things, continue updating the position of the cursor in the points array
        if (CanDrawMore())
        {
            // update our cursor's position in the list
            LoggedMousePos = m_cursorInstanceTransform.position;

            // update the renderer so that it properly displays the color and cursor pos
            UpdateRenderer();
        }

        m_previousInput = m_currentInput;
    }

    // places a point and adds it to the point array, returning the distance from the previous point position to this one
    private float PlacePoint()
    {
        var current_pos = m_cursorInstanceTransform.position;

        // funky list operator that gets the 2nd to last element of the list (i.e. the most recent BESIDES the cursor).
        //
        // our points list always has at least two elements in it (the player's start position and cursor pos),
        // so we can safely access it without checking.
        float distance = Vector3.Distance(m_points[^2], current_pos); // if we used LoggedMousePos/m_points[^1] (the cursor's pos), we'll be behind by 1 update tick.

        // if we end up overdrawing, clamp us to the furthest possible distance
        if (m_currentDistanceDrawn + distance > m_maxDrawDistance)
        {
            float distance_remaining = m_maxDrawDistance - m_currentDistanceDrawn;

            // calculate the maximum position using the last point we added, not the mouse's slightly-delayed "current" position.
            current_pos = m_points[^2] + new Vector3(m_previousInput.x, 0f, m_previousInput.y) * distance_remaining;
        }

        m_points.Add(current_pos);

        return distance;
    }

    private void UpdateRenderer(bool update_all = false)
    {
        float progress = m_colorCurve.Evaluate((m_currentDistanceDrawn + m_tentativePositionDistance) / m_maxDrawDistance);
        var color = Color.Lerp(Color.green, Color.red, progress);
        m_renderer.startColor = m_renderer.endColor = color;

        if (update_all)
        {
            m_renderer.positionCount = m_points.Count;
            m_renderer.SetPositions(m_points.ToArray());

            return;
        }

        // if (m_renderer.positionCount == m_points.Count) return;

        m_renderer.positionCount = m_points.Count;
        m_renderer.SetPosition(m_renderer.positionCount - 1, LoggedMousePos);
    }

    private void ToggleDrawSource()
    {
        if (m_isInDrawMode)
        {
            
        }
    }

    private bool CanDrawMore() => m_currentDistanceDrawn + m_tentativePositionDistance < m_maxDrawDistance - 0.05f;

    public bool IsDrawing() => m_isInDrawMode;
    public List<Vector3> GetPath() => m_points;

    private void OnDrawGizmosSelected()
    {
        if (!m_cursorInstanceTransform) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(m_cursorInstanceTransform.position, 0.75f);

        Gizmos.color = CanDrawMore() ? Color.green : Color.red;
        foreach (var point in m_points)
        {
            Gizmos.DrawSphere(point, 0.75f);
        }
    }
}
