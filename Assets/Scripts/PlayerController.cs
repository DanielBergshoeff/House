using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float RotateSpeed = 3f;
    public float JumpForce = 1f;
    public float BounceStrength = 2f;
    public InputActionAsset playerControls;

    private InputAction movement;
    private InputAction jump;
    private Vector2 moveDir;

    private Rigidbody myRigidbody;
    private ConstantForce myConstantForce;
    private float currentHeight = 0;
    private bool jumping = false;
    private bool curtainRiding = false;
    private Transform currentCurtain;
    private Vector3 curtainRelativePos;
    private bool curtainRidingCooldown = false;
    private bool hasGlider = false;
    private bool gliding = false;
    private bool chairing = false;
    private bool chairingCooldown = false;
    private GameObject glider;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myConstantForce = GetComponent<ConstantForce>();

        var playerMap = playerControls.FindActionMap("Player");
        movement = playerMap.FindAction("Move");
        movement.performed += OnMove;
        movement.canceled += OnMove;
        movement.Enable();

        jump = playerMap.FindAction("Jump");
        jump.performed += OnJump;
        jump.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (curtainRiding) {
            CurtainRiding();
        }

        if (jumping) {
            if (hasGlider && myRigidbody.velocity.y < 0f) {
                StartGliding();
            }

            RaycastHit hit1;
            if (Physics.Raycast(transform.position, -transform.up, out hit1, 1f)) {
                if (hit1.distance < 0.51f) {
                    //Landed
                    jumping = false;

                    if (gliding) {
                        StopGliding();
                    }

                    if (!hit1.collider.CompareTag("BuildingBlock"))
                        return;

                    BuildingBlock bb2 = hit1.collider.GetComponent<BuildingBlock>();
                    if (bb2.Bouncy) {
                        Vector3 bounce = new Vector3(myRigidbody.velocity.x, Mathf.Clamp(-myRigidbody.velocity.y, 0f, 5f), myRigidbody.velocity.z) * BounceStrength;
                        myRigidbody.AddForce(bounce);
                    }

                    if (curtainRidingCooldown)
                        curtainRidingCooldown = false;

                    if (chairingCooldown)
                        chairingCooldown = false;
                }
            }
            return;
        }
        else {
            RaycastHit hit1;
            if (Physics.Raycast(transform.position, -transform.up, out hit1, 1f)) {
                if (hit1.distance > 0.51f) {
                    jumping = true;
                }
            }
            else {
                jumping = true;
            }
        }
    }

    private void CurtainRiding() {
        transform.position = currentCurtain.position + curtainRelativePos;
    }

    private void Move() {
        if (moveDir.magnitude < 0.1f || curtainRiding || chairing)
            return;

        Vector3 targetDir = new Vector3(moveDir.x, 0f, moveDir.y);
        transform.position = transform.position + targetDir * Time.deltaTime * MoveSpeed;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * RotateSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnMove(InputAction.CallbackContext context) {
        moveDir = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context) {
        if (curtainRiding) {
            myRigidbody.isKinematic = false;
            curtainRiding = false;
            return;
        }

        if (chairing) {
            myRigidbody.isKinematic = false;
            transform.parent = null;
            chairing = false;
        }

        if (jumping)
            return;

        myRigidbody.AddForce(Vector3.up * JumpForce);
        
    }

    private void StartGliding() {
        hasGlider = false;
        gliding = true;
        myRigidbody.useGravity = false;
        myConstantForce.force = new Vector3(0f, -2f, 0f);
    }

    private void StopGliding() {
        gliding = false;
        myRigidbody.useGravity = true;
        myConstantForce.force = Vector3.zero;
        Destroy(glider);
    }

    private void OnCollisionEnter(Collision collision) {
        CheckForBB(collision);
        CheckForCurtain(collision);
    }

    private void CheckForChair(Collider other) {
        if (!other.CompareTag("Chair") || chairingCooldown)
            return;

        Debug.Log(moveDir);
        other.transform.parent.GetComponent<Rigidbody>().AddForce(new Vector3(moveDir.x, 0f, moveDir.y) * 200f);

        transform.position = other.transform.parent.position + Vector3.up * 1f;
        transform.parent = other.transform.parent;

        myRigidbody.isKinematic = true;
        chairing = true;
        chairingCooldown = true;
    }

    private void OnTriggerEnter(Collider other) {
        CheckForChair(other);

        if (!other.CompareTag("Glider"))
            return;

        glider = other.gameObject;
        glider.transform.position = transform.position + Vector3.up * 1f;
        glider.transform.parent = transform;
        hasGlider = true;
    }

    private void CheckForCurtain(Collision collision) {
        if (!jumping || curtainRidingCooldown)
            return;

        if (!collision.collider.CompareTag("Curtain"))
            return;

        CurtainPart cp = collision.collider.GetComponent<CurtainPart>();
        if(cp.MyCurtain.go1 == cp.gameObject && cp.MyCurtain.CurrentState == 0) {
            cp.MyCurtain.MyAnimator.SetTrigger("OpenRight");
            cp.MyCurtain.CurrentState = 2;
            StartCurtainRiding(cp.transform);
        }
        else if (cp.MyCurtain.go2 == cp.gameObject && cp.MyCurtain.CurrentState == 0) {
            cp.MyCurtain.MyAnimator.SetTrigger("OpenLeft");
            cp.MyCurtain.CurrentState = 1;
            StartCurtainRiding(cp.transform);
        }
        else if (cp.MyCurtain.go1 == cp.gameObject && cp.MyCurtain.CurrentState == 2 || cp.MyCurtain.go2 == cp.gameObject && cp.MyCurtain.CurrentState == 1) {
            cp.MyCurtain.MyAnimator.SetTrigger("Close");
            cp.MyCurtain.CurrentState = 0;
            StartCurtainRiding(cp.transform);
        }
    }

    private void StartCurtainRiding(Transform t) {
        currentCurtain = t;
        myRigidbody.isKinematic = true;
        curtainRelativePos = transform.position - t.position;
        curtainRiding = true;
        jumping = false;
        curtainRidingCooldown = true;
    }

    private void CheckForBB(Collision collision) {
        if (jumping) {
            return;
        }

        if (!collision.collider.CompareTag("BuildingBlock"))
            return;

        BuildingBlock bb = collision.collider.GetComponent<BuildingBlock>();
        if (bb.Obstructed)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f)) {
            if (hit.collider.CompareTag("BuildingBlock")) {
                currentHeight = hit.transform.position.y;
            }
        }

        if (bb.transform.position.y != currentHeight && Mathf.Abs(currentHeight - bb.transform.position.y) < 0.55f) {
            //transform.position = bb.transform.position + Vector3.up * 0.5f;
            transform.position = transform.position + Vector3.up * 0.5f;
        }
    }
}
