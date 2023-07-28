using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject CameraHolder;
    [SerializeField] float mouseSensitvity, sprintSpeed, wlakSpeed, jumpForce, smothTime;
    private float verticalLookRotation;
    private bool grounded;
    private Vector3 smothMoveVelocity;
    private Vector3 moveAmoun;
    Rigidbody rb;
    PhotonView PV;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    private void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();
    }
    private void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmoun = Vector3.SmoothDamp(moveAmoun, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : wlakSpeed), ref smothMoveVelocity, smothTime);
    }
    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitvity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitvity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
        CameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    public void SetGroundState(bool ground)
    {
        grounded = ground;
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmoun) * Time.fixedDeltaTime);
    }
}
