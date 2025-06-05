using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float MoveSpeed = 10.0f;
    private readonly float CameraSpeed = 100f;

    private Transform CameraTransform;
    private float CameraPitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.CameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // mouse
        var mouseX = Input.GetAxis("Mouse X") * this.CameraSpeed * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * this.CameraSpeed * Time.deltaTime;

        this.transform.Rotate(Vector3.up * mouseX);

        this.CameraPitch -= mouseY;
        this.CameraPitch = Mathf.Clamp(this.CameraPitch, -90f, 90f);
        this.CameraTransform.localEulerAngles = new Vector3(this.CameraPitch, 0f, 0f);

        // mouse click
        if (Input.GetMouseButtonDown(0))
        {
            var ray = new Ray(this.CameraTransform.position, this.CameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log($"Hit: {hitInfo.collider.name} at {hitInfo.point}");
            }
        }

        // keyboard
        var inputX = Input.GetAxis("Horizontal");
        var inputZ = Input.GetAxis("Vertical");

        var move = this.transform.right * inputX + this.transform.forward * inputZ;
        move = move.normalized * this.MoveSpeed * Time.deltaTime;

        this.transform.position += move;
    }
}
