using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Movement Variables
    CharacterController charController;
    Vector3 GForce = Vector3.zero;
    bool isGrounded;
    Transform cameraPos;
    AudioSource audioSource;

    //Slope Variables
    Vector3 slopeTangent;
    Vector3 slopeNormal;
    float slopeAngle;

    //Camera Animation Variables
    float movementSpeed;
    float moveAnimTime;
    Vector2 initialCamPos;
    float camAnimSpeed;

    //Jump Recoil Variables
    bool isAnimRecoil = false;
    float recoilY;
    Keyframe[] landAnimKeys;

    [Header("Movement Settings")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float walkSpeed = 10, runSpeed = 25;
    [SerializeField] private float runBuildUp = 1;
    [SerializeField] private AudioClip walkingSound;
    [Header("Jump Settings")]
    [SerializeField] private float GForceVal = -9.81f;
    [SerializeField] private float jumpHeight = 10;
    [Header("Slope Settings")]
    [SerializeField] private float slopeForce;
    [SerializeField, Range(0,1)] private float friction;
    [Header("Camera Jitter Animation Settings")]
    [SerializeField] private AnimationCurve movingJitter;
    [SerializeField] private float animYChangeMultiplier = 0.267f;
    [SerializeField] private float animSpeedWalk = 3, animSpeedRun = 5;
    [SerializeField] private float runBuildUpAnim = 1;
    [Header("Jump Recoil Settings")]
    [SerializeField] private float maxjumpRecoil = 0.3f;
    [SerializeField] private float landAnimSpeed = 0.1f;
    
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        charController = this.GetComponent<CharacterController>();
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        initialCamPos = cameraPos.localPosition;
        audioSource.clip = walkingSound;

        movementSpeed = walkSpeed;
        moveAnimTime = 0;
        camAnimSpeed = animSpeedWalk;

        landAnimKeys = new Keyframe[3];
        landAnimKeys[0] = new Keyframe(0.0f, 1.0f);
        landAnimKeys[0].outTangent = -0.5f;
        landAnimKeys[1] = new Keyframe(0.5f, 0);
        landAnimKeys[2] = new Keyframe(1.0f, 1.0f);
    }

    void Update()
    {
        BuildUpSpeed();
        isGrounded = charController.isGrounded;
        GetFloorInfo();
        LandAnimCheck();
        bool isOnSlope = (Vector3.Angle(Vector3.up, slopeNormal) >= charController.slopeLimit);
        if(isGrounded && GForce.y < 0 && !isOnSlope)
            GForce = new Vector3(0,-2f,0);

        CharacterMovement();

        if (Input.GetButtonDown("Jump") && isGrounded)
            GForce = Mathf.Sqrt(jumpHeight*-2f*GForceVal)* slopeNormal;

        if(!isGrounded)
            GForce.y += GForceVal * Time.deltaTime;
        else if (isOnSlope)
            GForce += GForceVal * slopeTangent * slopeAngle * Time.deltaTime * -1 * (1f - friction);

        charController.Move(GForce * Time.deltaTime);
    }

    private void CharacterMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            Vector3 move = this.transform.right * x + this.transform.forward * z;
            move = Vector3.ClampMagnitude(move, 1.0f);
            charController.Move(move * movementSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)
                audioSource.Play();

            moveAnimTime += camAnimSpeed * Time.deltaTime;
            float timeChanged = (Mathf.Sin(moveAnimTime) + 1) / 2;
            float yAnim = movingJitter.Evaluate(timeChanged) * animYChangeMultiplier;
            cameraPos.localPosition = new Vector3(cameraPos.localPosition.x, yAnim + initialCamPos.y, cameraPos.localPosition.z);
        }
        else
            audioSource.Stop();
    }

    private void BuildUpSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUp);
            camAnimSpeed = Mathf.Lerp(camAnimSpeed, animSpeedRun, Time.deltaTime * runBuildUpAnim);
        }    
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUp);
            camAnimSpeed = Mathf.Lerp(camAnimSpeed, animSpeedWalk, Time.deltaTime * runBuildUpAnim);
        }
    }

    private void LandAnimCheck()
    {
        if (GForce.y*-1 <= jumpHeight || !isGrounded || isAnimRecoil)
            return;

        float jumpRecoil = Mathf.Abs(GForce.y*-1-jumpHeight) /GForceVal*-1 /4f;
        recoilY = Mathf.Min(maxjumpRecoil, jumpRecoil);

        StartCoroutine("LandAnim");
    }

    private IEnumerator LandAnim()
    {
        isAnimRecoil = true;
        Vector3 scale = this.gameObject.transform.localScale;
        landAnimKeys[1] = new Keyframe(0.5f, 1f-recoilY);
        AnimationCurve landAnimCurve = new AnimationCurve(landAnimKeys);
        float timer = 0;
        while (timer < 1)
        {
            this.gameObject.transform.localScale = new Vector3(scale.x, scale.y * landAnimCurve.Evaluate(timer), scale.z);
            timer += Time.deltaTime * landAnimSpeed;
            yield return new WaitForSeconds(Time.deltaTime * landAnimSpeed);
        }
        isAnimRecoil = false;
        yield return null;
    }

    private void GetFloorInfo()
    {
        if (!isGrounded)
            return;

        //In hit will be stored objects that was hitted by ray
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.down, out hit, charController.height))
        {
            //if normal is not up then it is slope
            if(hit.normal != Vector3.up)
            {
                slopeNormal = hit.normal;
                slopeTangent = Vector3.Cross(hit.normal, hit.transform.right);
                slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                slopeAngle = Mathf.Sin(slopeAngle);
                return;
            }
        }

        slopeNormal = Vector3.up;
    }
}
