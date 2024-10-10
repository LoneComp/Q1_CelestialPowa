using UnityEngine;
using UnityEngine.InputSystem;

public class S_CameraMovement : MonoBehaviour
{
    [Header("Assignable")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [Space(10)]
    [Header("Variables")]
    [SerializeField, Range(-1000, -10)] private float distanceToTarget;
    
    private Vector3 _previousPosition;

    private void Start()
    {
        //Place camera to a good position
        cam.transform.position = new Vector3(target.position.x, target.position.y, target.position.z + distanceToTarget);
    }

    private void Update()
    {
        GetPreviousPosition();
        MoveCamera();
    }

    private void GetPreviousPosition()
    {
        //Get start of right button
        if (Input.GetMouseButtonDown(1))
        {
            _previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void MoveCamera()
    {
        //Get update info of right button
        if (Input.GetMouseButton(1))
        {
            Vector3 direction = _previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = target.position;
            
            //Update all position and rotation
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Translate(0, 0, distanceToTarget);
            
            _previousPosition = cam.ScreenToViewportPoint(Input.mousePosition); //Reset previous position
        }
    }

    public void ZoomInOut(InputAction.CallbackContext context)
    {
        //Get input value
        Vector2 zoomInput = context.ReadValue<Vector2>();
        zoomInput.y = Mathf.Clamp(zoomInput.y, -1, 1);
        
        //Adjust field of view
        cam.fieldOfView += zoomInput.y;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 3, 100);
    }

}
