using UnityEngine;
using UnityEngine.EventSystems;

public class HeartViewController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Camera mainCamera;

    [Header("Control Speed")]
    public float rotateSpeed = 120f;
    public float zoomSpeed = 15f;
    public float panSpeed = 0.01f;

    [Header("Zoom Limit")]
    public float minDistance = 0.2f;
    public float maxDistance = 100f;

    private bool leftDragStartedOnUI = false;
    private bool rightDragStartedOnUI = false;

    void Update()
    {
        if (target == null) return;
        if (mainCamera == null) mainCamera = Camera.main;

        bool pointerOverUI = EventSystem.current != null &&
                             EventSystem.current.IsPointerOverGameObject();

        // Check if mouse drag started on UI
        if (Input.GetMouseButtonDown(0))
        {
            leftDragStartedOnUI = pointerOverUI;
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightDragStartedOnUI = pointerOverUI;
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftDragStartedOnUI = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightDragStartedOnUI = false;
        }

        // Zoom with mouse wheel
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll) > 0.001f && mainCamera != null && !pointerOverUI)
        {
            ZoomCamera(scroll);
        }

        // If dragging started on UI, do not rotate or pan
        if (leftDragStartedOnUI || rightDragStartedOnUI)
        {
            return;
        }

        // Left mouse drag = rotate heart
        if (Input.GetMouseButton(0) && !pointerOverUI)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            target.Rotate(Vector3.up, -mouseX, Space.World);
            target.Rotate(Vector3.right, mouseY, Space.World);
        }

        // Right mouse drag = pan heart
        if (Input.GetMouseButton(1) && !pointerOverUI)
        {
            float panX = -Input.GetAxis("Mouse X") * panSpeed;
            float panY = -Input.GetAxis("Mouse Y") * panSpeed;

            target.Translate(new Vector3(panX, panY, 0), Space.World);
        }
    }

    void ZoomCamera(float scroll)
{
    if (mainCamera == null) return;

    // Orthographic camera
    if (mainCamera.orthographic)
    {
        mainCamera.orthographicSize -= scroll * zoomSpeed * 0.1f;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 0.1f, 20f);
        return;
    }

    // Perspective camera: zoom by changing Field of View
    mainCamera.fieldOfView -= scroll * zoomSpeed;
    mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 15f, 80f);
}
}