using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float MoveSpeed = 10f;
    private readonly float CameraSpeed = 100f;
    private readonly float JumpForce = 5f;
    private readonly float FallDamageThreshold = 5f;
    private readonly float FallDamagePerMeter = 10f;

    private Transform CameraTransform;
    private float CameraPitch = 0f;

    private Rigidbody Rigidbody;
    private bool IsGrounded = true;
    private bool isFalling = false;
    private float FallStartY;

    public PlayerStatus PlayerStatus;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.CameraTransform = GetComponentInChildren<Camera>().transform;
        this.Rigidbody = GetComponent<Rigidbody>();
        this.PlayerStatus = new PlayerStatus();
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

        if (Input.GetKeyDown(KeyCode.Space) && this.IsGrounded)
        {
            this.Rigidbody.AddForce(Vector3.up * this.JumpForce, ForceMode.Impulse);
            this.IsGrounded = false;
        }

        // fall
        if (!this.IsGrounded && !this.isFalling && this.Rigidbody.linearVelocity.y < -0.1f)
        {
            this.isFalling = true;
            this.FallStartY = this.transform.position.y;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (this.isFalling)
            {
                var fallDistance = this.FallStartY - this.transform.position.y;
                if (fallDistance > this.FallDamageThreshold)
                {
                    var damage = Mathf.CeilToInt((fallDistance - this.FallDamageThreshold) * this.FallDamagePerMeter);
                    this.PlayerStatus.HP -= damage;
                    Debug.Log($"Player took {damage} fall damage. Remaining HP: {this.PlayerStatus.HP}");
                }
                this.isFalling = false;
            }

            this.IsGrounded = true;
        }
    }
}
