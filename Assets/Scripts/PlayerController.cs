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
    public Animator PlayerAnimator;
    public Camera MyCamera;
    public bool TagIt;


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
    private bool climbing = false;
    private bool climbingCooldown = false;
    private GameObject glider;
    private bool dizzy = false;
    private bool moving = false;

    private float jumpStartPos;

    private SkinnedMeshRenderer myRenderer;

    private AudioSource myAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myConstantForce = GetComponent<ConstantForce>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void UnDizzy() {
        dizzy = false;
    }

    public void SetAsIt() {
        if(myRenderer == null)
            myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        TagIt = true;
        myRenderer.material = GameManager.Instance.ItColor;
        PlayerAnimator.SetTrigger("Dizzy");
        dizzy = true;
        Invoke("UnDizzy", 2f);
    }

    public void SetAsNotIt() {
        if (myRenderer == null)
            myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        TagIt = false;
        myRenderer.material = GameManager.Instance.NormalColor;
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
                    if (jumpStartPos - transform.position.y > 2f && !gliding) {
                        PlayerAnimator.SetTrigger("Dizzy");
                        dizzy = true;
                        Invoke("UnDizzy", 2f);
                    }

                    if (gliding) {
                        StopGliding();
                    }
                    jumping = false;

                    if (!hit1.collider.CompareTag("BuildingBlock"))
                        return;

                    BuildingBlock bb2 = hit1.collider.GetComponent<BuildingBlock>();
                    if (bb2.Bouncy) {
                        Vector3 bounce = new Vector3(myRigidbody.velocity.x, Mathf.Clamp(-myRigidbody.velocity.y, 0f, 5f), myRigidbody.velocity.z) * BounceStrength;
                        myRigidbody.AddForce(bounce);
                        PlayerAnimator.SetTrigger("Jump");
                    }

                    if (curtainRidingCooldown)
                        curtainRidingCooldown = false;

                    if (chairingCooldown)
                        chairingCooldown = false;

                    if (climbingCooldown)
                        climbingCooldown = false;
                }
            }
            return;
        }
        else {
            RaycastHit hit1;
            if (Physics.Raycast(transform.position, -transform.up, out hit1, 1f)) {
                if (hit1.distance > 0.51f) {
                    jumping = true;
                    jumpStartPos = transform.position.y;
                }
            }
            else {
                jumping = true;
                jumpStartPos = transform.position.y;
            }
        }
    }

    private void CurtainRiding() {
        transform.position = currentCurtain.position + curtainRelativePos;
    }

    private void Move() {
        if (moveDir.magnitude < 0.1f || curtainRiding || chairing || climbing || dizzy) {
            moving = false;
            myAudioSource.Stop();
            return;
        }

        if (!moving && !gliding && !jumping) {
            myAudioSource.loop = true;
            myAudioSource.clip = AudioManager.Instance.PlayerRun;
            myAudioSource.Play();
            moving = true;
        }
        else {
            if(gliding || jumping) {
                moving = false;
                myAudioSource.Stop();
            }
        }

        Vector3 targetDir = new Vector3(moveDir.x, 0f, moveDir.y);
        targetDir = MyCamera.transform.TransformDirection(targetDir);
        targetDir.y = 0.0f;

        transform.position = transform.position + targetDir * Time.deltaTime * MoveSpeed;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime * RotateSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnMove(InputValue context) {
        moveDir = context.Get<Vector2>();
        PlayerAnimator.SetFloat("Speed", context.Get<Vector2>().magnitude);
    }

    private void OnJump() {
        if (curtainRiding) {
            myRigidbody.isKinematic = false;
            curtainRiding = false;
            PlayerAnimator.SetBool("Hanging", false);
            return;
        }

        if (chairing) {
            myRigidbody.isKinematic = false;
            transform.parent = null;
            chairing = false;
            PlayerAnimator.SetBool("Chair", false);
        }

        if (jumping || dizzy)
            return;

        myRigidbody.AddForce(Vector3.up * JumpForce);
        PlayerAnimator.SetTrigger("Jump");
    }

    private void StartGliding() {
        hasGlider = false;
        gliding = true;
        myRigidbody.useGravity = false;
        myConstantForce.force = new Vector3(0f, -2f, 0f);
        PlayerAnimator.SetBool("Glide", true);
    }

    private void StopGliding() {
        gliding = false;
        myRigidbody.useGravity = true;
        myConstantForce.force = Vector3.zero;
        PlayerAnimator.SetBool("Glide", false);
        Destroy(glider);
    }

    private void OnCollisionEnter(Collision collision) {
        CheckForBB(collision);
        CheckForCurtain(collision);
        CheckForPlayer(collision);
    }

    private void CheckForPlayer(Collision collision) {
        if (!collision.collider.CompareTag("Player"))
            return;

        if (TagIt) {
            SetAsNotIt();
        }
        else {
            SetAsIt();
        }

    }

    private void CheckForChair(Collider other) {
        if (!other.CompareTag("Chair") || chairingCooldown)
            return;

        Vector3 targetDir = new Vector3(moveDir.x, 0f, moveDir.y);
        targetDir = MyCamera.transform.TransformDirection(targetDir);
        targetDir.y = 0.0f;

        other.transform.parent.GetComponent<Rigidbody>().AddForce(targetDir * 200f);

        transform.position = other.transform.parent.position + Vector3.up * 2f;
        transform.parent = other.transform.parent;

        myRigidbody.isKinematic = true;
        chairing = true;
        chairingCooldown = true;
        PlayerAnimator.SetBool("Chair", true);
    }

    private void OnTriggerEnter(Collider other) {
        CheckForChair(other);

        if (!other.CompareTag("Glider"))
            return;

        glider = other.gameObject;
        glider.transform.position = transform.position + Vector3.up * 0.5f;
        glider.transform.parent = transform;
        glider.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
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
        PlayerAnimator.SetBool("Hanging", true);
    }

    private void CheckForBB(Collision collision) {
        if (jumping || climbing || climbingCooldown) {
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
            if(Physics.Raycast(bb.transform.position + Vector3.up * 1f, -bb.transform.up, out hit, 3f)) {
                if (hit.transform == bb.transform) {
                    //PlayerAnimator.SetTrigger("Climb");
                    transform.position = transform.position + Vector3.up * 0.5f + transform.forward * 0.2f;
                    climbing = true;
                    Invoke("DoClimb", 0.1f);
                }

            }
        }
    }

    private void DoClimb() {
        //transform.position = transform.position + Vector3.up * 0.5f + transform.forward * 0.2f;
        climbing = false;
        climbingCooldown = true;
    }
}
