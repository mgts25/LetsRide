using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Test End")]
    public bool showLogs;

    [SerializeField] private float h = 0f;
    [SerializeField] private float v = 0f;

    private float curDir = 0f;
    private Vector3 curNormal = Vector3.up;
    private Transform tr;
    private Rigidbody rb;
    private AngleChecker angleChecker;

    static public float distanceToGround = 0f;
    private Vector3 currentPos;

    public float power = 10f;
    public float rotateSpeed = 3f;

    [Space(10)]
    public Transform rotatedtransform;
    public Transform raycasttransform;

    public SphereCollider sphereCollider;
    public float downForce = -80f;
    public bool reverseTilt = false;

    [Space(10)]
    public Transform fakie;
    public Transform brakes;
    public Transform character;
    public Transform playerRoot;
    public Transform playerPivot;

    [Space(10)]
    public float tiltAngle = 5f;
    private float tiltAroundZ;
    private float tiltAroundX;
    public float raycastDistance = 1f;

    [Space(10)]
    public Transform Board;
    public bool tiltBoard;

    [Space(10)]
    public float maxVelocity;
    public float carveSpeed = 5;
    private float maxCarve;

    private Quaternion grndTilt;

    [Space(10)]
    public Animator anim;
    public Animator mainAnim;

    [Space(10)]
    public GameObject swipe;
    public GameObject joystick;

    [Space(10)]
    public float rotatationAngle = 180;
    public float controlsSensitivity = 5;

    [Space(10)]
    public float sidewaysForce = 20f;
    public float angle;

    [Space(10)]
    public bool isGrounded;

    [Space(10)]
    [Range(-1, 1)]
    public float tilt;
    //public float appliedForce;
    [Range(0, 1)]
    public float thrust;

    [Space(10)]
    public Vector3 forwardMoveOffset;
    public Vector3 backwardMoveOffset;
    public float moveOffsetSpeed = 5;
    public float moveOffset;
    public float smoothOffset;
    public float smoothOffsetSpeed;


    [Space(10)]
    public float fakieAngle = 180;
    public bool isFakie;
    public float fakieSpeed = 1;
    public bool braking;

    [Header("DEBUG")]
    public bool align;
    public bool allowRotation;

    [Space(10)]
    public float checkInterval = 1;
    private float initCheckInterval = 1;

    [System.Serializable]
    public class VelocitySettings
    {
        [Tooltip("To maintain base to max speed")]
        public float offset1;
        [Tooltip("To maintain min to base speed")]
        public float offset2;
        public float baseValue;
    }
    [Space]
    public VelocitySettings velocitySettings;

    [System.Serializable]
    public class MidAirTrickSettings
    {
        public Transform character;
        public float returningAngleSpeed;
        public float smoothAngle;
        public float midAirRayDistance = 2;
        public bool isMidair;
        public int trickIndex;
        public bool trickPerformed;

        public float joystickAngle;

        public bool Open(int currentTrick = 0)
        {
            // Debug.Log("Open: " + currentTrick + " | " + trickIndex + " | " + (trickIndex == currentTrick).ToString());
            // Selection is open for any
            if (trickIndex == 0)
                return true;
            else if (currentTrick == 0)
            {
                trickPerformed = false;
                trickIndex = 0;
                return true;
            }
            // Only open for selected trick
            else
                return trickIndex == currentTrick;
        }

        public bool CurrentState(int state)
        {
            return state == trickIndex;
        }
    }
    public MidAirTrickSettings midAirTrickSettings;

    [System.Serializable]
    public class JumpSettings
    {
        [Range(0, 1)] public float max_h_Tojump;
        public float force;
        public bool isLocked;
        public float lockTime = 1;
        [HideInInspector] public float lockDuration;
    }
    public JumpSettings jumpSettings;

    [System.Serializable]
    public class FakieSettings
    {
        [Range(-1, 0)] public float minLeftThreshold;
        [Range(0, 1)] public float minRightThreshold;

        public float onLeftTime;
        public float onRightTime;
        public int joyStickOnLeft = 0;

        public float threshold;
        public float timeDiff;

        public float bounceTime1 = 1;
        public float bounceValue;
        public float bounceSpeed;

    }
    public FakieSettings fakieSettings;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        angleChecker = GetComponent<AngleChecker>();

        maxCarve = carveSpeed;

        initCheckInterval = checkInterval;
    }

    public float hValueHandler = 0;
    public bool isFaceOnLeft;

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (ControlFreak2.CF2Input.GetKeyUp(KeyCode.P))
            Application.LoadLevel(Application.loadedLevel);


        AlignRampPlayer();
        CheckInput();


        if (isGrounded)
        {
            #region PLAYER ROTATION / CARVING
            if (allowRotation)
            {
                playerRoot.localEulerAngles = new Vector3(playerRoot.localEulerAngles.x, (rotatationAngle * h * (NormalizedThrust() * 2)), playerRoot.localEulerAngles.z);
                //playerRoot.Rotate(0, carveSpeed * h * Time.deltaTime, 0);
            }
            #endregion PLAYER ROTATION / CARVING

            #region MOVE OFFSET
            //PrintLog("v - > " + v + " | thrust -> " + NormalizedThrust());
            float offsetValue = NormalizedThrust() < 0.5f ? -1 * (1 - NormalizedThrust() * 2) : (NormalizedThrust() - 0.5f) * 2;
            moveOffset = moveOffsetSpeed * offsetValue * angle;
            //PrintLog("forwardMoveOffset - > " + forwardMoveOffset);
            fakie.localPosition = moveOffset != 0 ? forwardMoveOffset * SmoothValue(ref smoothOffset, moveOffset, smoothOffsetSpeed) : forwardMoveOffset * offsetValue;
            fakie.localPosition = new Vector3(fakie.localPosition.x, fakie.localPosition.y, Mathf.Clamp(fakie.localPosition.z, -forwardMoveOffset.z, forwardMoveOffset.z));
            #endregion MOVE OFFSET

            #region FAKIE
            // if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.F))
            // {
            //     if (isFakie)
            //     {
            //         isFakie = false;
            //     }
            //     else
            //     {
            //         isFakie = true;
            //     }

            //     mainAnim.SetTrigger("jump");
            // }
            // float timeDiff = Mathf.Abs(fakieSettings.onLeftTime - fakieSettings.onRightTime);

            // if (h < fakieSettings.minLeftThreshold)
            // {
            //     fakieSettings.onLeftTime = Time.time;

            //     // Coming from right
            //     if (fakieSettings.joyStickOnLeft == 1)
            //     {
            //         if (timeDiff > 0 && timeDiff < fakieSettings.threshold)
            //         {
            //             isFaceOnLeft = !isFaceOnLeft;
            //             mainAnim.SetTrigger("jump");
            //         }
            //     }
            //     fakieSettings.joyStickOnLeft = -1;
            // }

            // if (h > fakieSettings.minRightThreshold)
            // {
            //     fakieSettings.onRightTime = Time.time;

            //     // Coming from left
            //     if (fakieSettings.joyStickOnLeft == -1)
            //     {
            //         if (timeDiff > 0 && timeDiff < fakieSettings.threshold)
            //         {
            //             isFaceOnLeft = !isFaceOnLeft;
            //             mainAnim.SetTrigger("jump");
            //         }
            //     }
            //     fakieSettings.joyStickOnLeft = 1;
            // }


            // if (timeDiff > 0 && timeDiff < fakieSettings.timeDiff)
            //     fakieSettings.timeDiff = timeDiff;



            // fakie.localRotation = Quaternion.Slerp(fakie.localRotation, Quaternion.AngleAxis(isFakie ? fakieAngle : 0, transform.up), Time.deltaTime * fakieSpeed);

            if (fakieSettings.bounceTime1 <= 1)
            {
                fakieSettings.bounceTime1 += Time.deltaTime * fakieSettings.bounceSpeed;

                float x = skateBoard.localPosition.x;
                float y = skateBoard.localPosition.y;
                float z = skateBoard.localPosition.z;

                skateBoard.localPosition = isFaceOnLeft ? new Vector3(x, y, Mathf.Lerp(0, -fakieSettings.bounceValue, fakieSettings.bounceTime1)) : new Vector3(x, y, Mathf.Lerp(0, fakieSettings.bounceValue, fakieSettings.bounceTime1));
            }

            #endregion FAKIE

            #region BRAKES
            //brakes.localRotation = Quaternion.Euler(0, NormalizedThrust() < 0.5 ? (1 - NormalizedThrust() * 2) * 90 : 0, NormalizedThrust() < 0.5 ? -33 * (1 - 2 * NormalizedThrust()) : 0);
            //PrintLog("(" + (v < 0 ? Mathf.Abs(90 * v) : 0) + ", " + (NormalizedThrust() < 0.5 ? (1 - NormalizedThrust() * 2) * 90 : 0) + ") (" + (v < 0 ? -33 * -v : 0) + ", " + (NormalizedThrust() < 0.5 ? -33 * (1 - 2 * NormalizedThrust()) : 0) + ")");

            if (v > -0.1)
                hValueHandler = h;

            //brakes.localRotation = Quaternion.Euler(0, v < 0 ? 90 * v * (Mathf.RoundToInt(h)): 0, v < 0 ? -33 * -v : 0);
            brakes.localRotation = Quaternion.Euler(0, v < 0 ? (Mathf.Abs(90 * v) * (hValueHandler < 0 ? -1 : 1)) : 0, v < 0 ? (hValueHandler < 0 ? 33 : -33) * -v : 0);
            //brakes.localRotation = Quaternion.Slerp(brakes.localRotation, Quaternion.Euler(0, v < 0 ? 90 : 0, v < 0 ? -33 : 0), Mathf.Abs(angle * 1));
            // brakes.localRotation = Quaternion.Slerp(brakes.localRotation, Quaternion.AngleAxis(v < 0 ? 90 * Mathf.Abs(v) : 0, transform.up), angle);
            //else
            //mainAnim.SetFloat("brakes", Mathf.MoveTowards(mainAnim.GetFloat("brakes"), 0, Time.deltaTime));
            #endregion BRAKES

            //if (h != 0)
            //{
            //    checkInterval = Mathf.Clamp(checkInterval - Time.deltaTime, 0, initCheckInterval);

            //    if (checkInterval > 0.7f && checkInterval <= 0.8f && (h <= -0.8f || h >= 0.8f))
            //    {
            //        if (h <= -0.8f && !isFakie)
            //        {
            //            isFakie = true;
            //            mainAnim.SetTrigger("jump");
            //        }
            //        else if (h >= 0.8f && isFakie)
            //        {
            //            isFakie = false;
            //            mainAnim.SetTrigger("jump");
            //        }
            //    }
            //}
            //else
            //{
            //    checkInterval = initCheckInterval;
            //}
        }
        else
        {
            //Debug.Log("Character Forward: " + charForward);
        }


        tilt = (tiltAroundZ / tiltAngle) * NormalizedThrust() * Mathf.Clamp(v, 0, 1);

        thrust = rb.velocity.magnitude / maxVelocity;

        RefManager.I.gameplayMenu.SetSpeedUI(SpeedMPH);

        anim.SetFloat("Vertical", NormalizedThrust());
        anim.SetFloat("Horizontal", tilt);

        // Midle Jump
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space))
        {
            if (joystick.activeSelf)
            {
                joystick.SetActive(false);
                swipe.SetActive(true);
            }
            else
            {
                joystick.SetActive(true);
                swipe.SetActive(false);
            }
        }

        angle = Vector3.Dot(rotatedtransform.up, Vector3.up);
    }

    Transform skateBoard => RefManager.I.skateBoard;

    public void PerFormFakie()
    {
        isFaceOnLeft = !isFaceOnLeft;
        fakieSettings.bounceTime1 = 0;
    }

    public float SpeedMPH { get => thrust * 70; }

    [Header("Trick List")]
    [SerializeField] private List<int> trick1List;
    [SerializeField] private List<int> trick2List;
    [SerializeField] private List<int> trick3List;
    [SerializeField] private List<int> trick4List;

    public int trick1Ind = 0;
    public int trick2Ind = 0;
    public int trick3Ind = 0;
    public int trick4Ind = 0;
    void CheckInput()
    {

        if (Input.GetKey(KeyCode.C))
            Jump(); // For testng



        if (isGrounded)
        {
            h = ControlFreak2.CF2Input.GetAxis("Horizontal");//Mathf.MoveTowards(h, ControlFreak2.CF2Input.GetAxis("Horizontal"), Time.deltaTime * controlsSensitivity);
            v = ControlFreak2.CF2Input.GetAxis("Vertical");

            if (anim.GetInteger("Trick") != 0)
            {
                anim.SetInteger("Trick", 0);
                RefManager.I.playerAnimEventListener.ChangeSkateBoardParent();
            }

            LerpReturningAngle();

            midAirTrickSettings.Open(); // Reset
            //anim.SetFloat("FlipSpeed", 0);
        }
        else
        {
            v = ControlFreak2.CF2Input.GetAxis("Vertical");

            #region Christ Air
            if ((Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isTrick1Pressed) && midAirTrickSettings.isMidair)
            {
                midAirTrickSettings.trickIndex = 2;

                if (!midAirTrickSettings.trickPerformed)
                {
                    anim.SetInteger("Trick", midAirTrickSettings.trickIndex);
                    /// J: @{
                    //anim.SetInteger("TrickType", trick1List[Random.Range(0, trick1List.Count)]);
                    anim.SetInteger("TrickType", trick1List[trick1Ind]);
                    trick1Ind = (trick1Ind + 1) % trick1List.Count;
                    /// J: @}
                    midAirTrickSettings.trickPerformed = true;
                }
            }
            else if ((Input.GetKey(KeyCode.B) || RefManager.I.gameplayMenu.isTrick2Pressed) && midAirTrickSettings.isMidair)
            {
                midAirTrickSettings.trickIndex = 2;

                if (!midAirTrickSettings.trickPerformed)
                {
                    anim.SetInteger("Trick", midAirTrickSettings.trickIndex);
                    /// J: @{
                    //anim.SetInteger("TrickType", trick2List[Random.Range(0, trick2List.Count)]);
                    anim.SetInteger("TrickType", trick2List[trick2Ind]);
                    trick2Ind = (trick2Ind + 1) % trick2List.Count;
                    /// J: @}
                    midAirTrickSettings.trickPerformed = true;
                }
            }
            else if ((Input.GetKey(KeyCode.N) || RefManager.I.gameplayMenu.isTrick3Pressed) && midAirTrickSettings.isMidair)
            {
                midAirTrickSettings.trickIndex = 2;

                if (!midAirTrickSettings.trickPerformed)
                {
                    anim.SetInteger("Trick", midAirTrickSettings.trickIndex);
                    /// J: @{
                    //anim.SetInteger("TrickType", trick3List[Random.Range(0, trick3List.Count)]);
                    anim.SetInteger("TrickType", trick3List[trick3Ind]);
                    trick3Ind = (trick3Ind + 1) % trick3List.Count;
                    /// J: @}
                    midAirTrickSettings.trickPerformed = true;
                }
            }
            else if ((Input.GetKey(KeyCode.M) || RefManager.I.gameplayMenu.isTrick4Pressed) && midAirTrickSettings.isMidair)
            {
                midAirTrickSettings.trickIndex = 2;

                if (!midAirTrickSettings.trickPerformed)
                {
                    anim.SetInteger("Trick", midAirTrickSettings.trickIndex);
                    /// J: @{
                    //anim.SetInteger("TrickType", trick4List[Random.Range(0, trick4List.Count)]);
                    anim.SetInteger("TrickType", trick4List[trick4Ind]);
                    trick4Ind = (trick4Ind + 1) % trick4List.Count;
                    /// J: @}
                    midAirTrickSettings.trickPerformed = true;
                }
            }
            else
            {
                // Invoke("StopChristAir", 0);
                // StopChristAir();
                // midAirTrickSettings.trickIndex = -1;
                if (anim.GetInteger("Trick") != 1 && RefManager.I.playerAnimEventListener.animCompleted)
                {
                    anim.SetInteger("Trick", 1);
                    //RefManager.I.playerAnimEventListener.ChangeSkateBoardParent();
                }
            }
            #endregion Christ Air

            float joyStickAngle = Mathf.Atan2(ControlFreak2.CF2Input.GetAxis("Horizontal"), ControlFreak2.CF2Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            //PrintLog("JA -> " + joyStickAngle);


            #region Air Flips
            if (v != 0 && midAirTrickSettings.isMidair)
            {
                float y = midAirTrickSettings.character.localEulerAngles.y;

                // PrintLog("lR -> " + midAirTrickSettings.character.localEulerAngles);
                // PrintLog("y -> " + y + " | eq1 -> " + (Mathf.Abs(v) * midAirTrickSettings.smoothAngle));
                // midAirTrickSettings.character.localRotation = Quaternion.Euler(0, (y + (v * midAirTrickSettings.smoothAngle * (SpeedMPH / 70))) % 360, 0);

                // float newY = Mathf.Lerp(y, joyStickAngle, Mathf.Abs((joyStickAngle < 0 ? 360 + joyStickAngle: joyStickAngle) - y) / 360);
                midAirTrickSettings.joystickAngle = joyStickAngle < 0 ? 360 + joyStickAngle : joyStickAngle;
                midAirTrickSettings.character.localRotation = Quaternion.Euler(0, (midAirTrickSettings.joystickAngle + 90) % 360, 0);
            }
            else
            {
                //LerpReturningAngle();
            }
            #endregion Air Flips

            // if (midAirTrickSettings.CurrentState(1) && v == 0)
            //     anim.SetFloat("FlipSpeed", SmoothV());



            // 
            // if (midAirTrickSettings.trickIndex == -1 && !(Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isCAPressed))
            // {
            //     if (anim.GetInteger("Trick") != 0)
            //     {
            //         anim.SetInteger("Trick", 0);
            //         RefManager.I.playerAnimEventListener.ChangeSkateBoardParent();
            //     }
            // }

            // Previous work
            // if (v != 0 && midAirTrickSettings.isMidair)
            // {
            //     midAirTrickSettings.trickIndex = 0;
            //     anim.SetBool("flip", true);
            //     anim.SetFloat("FlipSpeed", SmoothV());
            // }
            // else
            // {
            //     //anim.SetBool("flip", false);
            //     anim.SetFloat("FlipSpeed", SmoothV());
            // }
        }
    }

    void LerpReturningAngle()
    {
        float y = midAirTrickSettings.character.localEulerAngles.y;
        bool isOnLeftSide = transform.localPosition.x < 0;
        // PrintLog("Left -> " + isOnLeftSide);

        if (isFaceOnLeft)
        {
            if (y > 269 - midAirTrickSettings.returningAngleSpeed && y < 271 + midAirTrickSettings.returningAngleSpeed)
                y = 270;

            if (y == 270)
                midAirTrickSettings.character.localRotation = Quaternion.Euler(0, 270, 0);
            else
            {
                midAirTrickSettings.character.localRotation = Quaternion.Euler(0, y + midAirTrickSettings.returningAngleSpeed, 0);
                //PrintLog("lR -> " + midAirTrickSettings.character.localEulerAngles);
            }
        }
        else
        {
            if (y > 89 - midAirTrickSettings.returningAngleSpeed && y < 91 + midAirTrickSettings.returningAngleSpeed)
                y = 90;

            if (y == 90)
                midAirTrickSettings.character.localRotation = Quaternion.Euler(0, 90, 0);
            else
            {
                midAirTrickSettings.character.localRotation = Quaternion.Euler(0, y + midAirTrickSettings.returningAngleSpeed, 0);
                //PrintLog("lR -> " + midAirTrickSettings.character.localEulerAngles);
            }
        }
    }

    void StopChristAir()
    {
        if (Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isTrick1Pressed || anim.GetInteger("Trick") == 0)
            return;

        if (Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isTrick2Pressed || anim.GetInteger("Trick") == 0)
            return;

        if (Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isTrick3Pressed || anim.GetInteger("Trick") == 0)
            return;

        if (Input.GetKey(KeyCode.V) || RefManager.I.gameplayMenu.isTrick4Pressed || anim.GetInteger("Trick") == 0)
            return;

        anim.SetInteger("Trick", 0);
        RefManager.I.playerAnimEventListener.ChangeSkateBoardParent();
    }

    void Jump()
    {
        if (isGrounded && !jumpSettings.isLocked)
        {
            // Jump
            //PrintLog("Jump");
            rb.AddForce(Vector3.up * jumpSettings.force, ForceMode.Impulse);
            jumpSettings.isLocked = true;
        }

        if (jumpSettings.isLocked)
            jumpSettings.lockDuration -= Time.deltaTime;

        if (jumpSettings.lockDuration < 0)
        {
            jumpSettings.lockDuration = jumpSettings.lockTime;
            jumpSettings.isLocked = false;
        }
    }

    void ControlTilt()
    {
        if (reverseTilt)
        {
            tiltAroundZ = h * -tiltAngle;
            tiltAroundX = v * -tiltAngle;
        }
        else
        {
            tiltAroundZ = h * tiltAngle;
            tiltAroundX = v * tiltAngle;
        }

        if (!isGrounded)
            return;

        Quaternion target = Quaternion.Euler(/*grndTilt.eulerAngles.x*/0, transform.localRotation.eulerAngles.y, v < 0 ? 0 : tiltAroundZ);
        // Quaternion target = Quaternion.Euler(grndTilt.eulerAngles.x, transform.localRotation.eulerAngles.y, v < 0 ? 0 : tiltAroundZ);

        playerPivot.transform.localRotation = Quaternion.Slerp(playerPivot.transform.localRotation, target, Time.deltaTime * 12f);

        if (tiltBoard)
        {
            Quaternion tar = Quaternion.Euler(tiltAroundZ, transform.localRotation.eulerAngles.y, grndTilt.eulerAngles.x);
            Board.localRotation = Quaternion.Slerp(Board.localRotation, tar, Time.deltaTime * 12f);
        }
    }

    void FixedUpdate()
    {
        ControlTilt();
        GroundCheck();

        if (isGrounded)
        {
            rb.AddForce(Vector3.down * maxVelocity);
            float multiplier = angleChecker.multiplier;
            // if (angleChecker.multiplier >= 0.5f)
            //     Jump();
            // So that player will be able to carve on edges 
            if (Mathf.Abs(h) > jumpSettings.max_h_Tojump)
            {
                if (angleChecker.multiplier >= 0.6f)
                    multiplier *= 3f;
                else if (angleChecker.multiplier >= 0.3f)
                    multiplier *= 1.5f;
            }
            rb.AddForceAtPosition(new Vector3((sidewaysForce * multiplier) * h * thrust, 0f, power * FabricateV(v)), playerRoot.position, ForceMode.Acceleration);
        }

        // if (isGrounded)
        // {
        //     //appliedForce = power;
        //     //rb.AddRelativeForce(new Vector3((appliedForce / 4) * h, 0f, power * (playerRoot.forward.normalized.z < 0 ? v * -1 : v)), ForceMode.Acceleration);
        //     rb.AddForceAtPosition(new Vector3((sidewaysForce) * h * thrust, 0f, power * FabricateV(v)/*(fakie.forward.normalized.z < 0 ? v * -1 : v)*/), playerRoot.position, ForceMode.Acceleration);
        // }


        var gravityVector = new Vector3(0, downForce * 100f, 0);
        rb.AddRelativeForce(gravityVector * Time.deltaTime, ForceMode.Acceleration);

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Abs(Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity)));

        rb.drag = Mathf.Clamp(rb.velocity.magnitude / 200 * rb.mass, 0.5f, 5);

        var carveMag = rb.velocity.magnitude / (maxVelocity / 2);

        carveSpeed = Mathf.Clamp(maxCarve * carveMag, 10, maxCarve);
    }



    void AlignRampPlayer()
    {
        if (!align)
            return;

        RaycastHit hit;
        currentPos = tr.position;

        if (Physics.Raycast(raycasttransform.position, -curNormal, out hit, raycastDistance) || Physics.Raycast(raycasttransform.position, -raycasttransform.right, out hit, raycastDistance) || Physics.Raycast(raycasttransform.position, raycasttransform.right, out hit, raycastDistance))
        {
            curNormal = Vector3.Lerp(curNormal, hit.normal, rotateSpeed * Time.deltaTime);
            grndTilt = Quaternion.FromToRotation(Vector3.up, curNormal);

            if (isGrounded)
            {
                rotatedtransform.rotation = grndTilt * Quaternion.Euler(0, curDir * Time.deltaTime, 0);
            }
            else if (!isGrounded)
            {
                // Quaternion target = Quatern.Euler(0, 0, 0);
                // rotatedtransform.rotation = Quaternion.Slerp(rotatedtransform.rotation, target, Time.deltaTime * 2f);
            }

            distanceToGround = hit.distance;
            Debug.DrawRay(raycasttransform.position, Vector3.down * distanceToGround, Color.red);
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        float distance = .75f;

        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(raycasttransform.position, dir, out hit, distance))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                Feedback.instance.React("Landing");
                var charForward = Vector3.Dot(character.up, transform.up);
                isFakie = charForward < 0.25f;
            }
        }
        else
        {
            isGrounded = false;
        }

        if (Physics.Raycast(raycasttransform.position, dir, out hit, midAirTrickSettings.midAirRayDistance))
        {
            midAirTrickSettings.isMidair = false;
            //Feedback.instance.React("Landing");
        }
        else
        {
            midAirTrickSettings.isMidair = true;
            Feedback.instance.React("Mid Air Front Grab");
        }
    }

    public float SmoothValue(ref float to, float from, float smoothSpeed)
    {
        //smoothOffset = Mathf.Lerp(smoothOffset, moveOffset, Mathf.Abs(moveOffset - smoothOffset));
        if (Mathf.Abs(from - to) < Time.deltaTime * smoothSpeed)
        {
            to = from;
            return from;
        }

        if (from > to)
            to += Time.deltaTime * smoothSpeed;
        else
            to -= Time.deltaTime * smoothSpeed;

        if (to < -1) to = -1;
        else if (to > 1) to = 1;

        return to;
    }

    float FabricateV(float v)
    {
        return velocitySettings.baseValue + v * (v > 0 ? velocitySettings.offset1 : velocitySettings.offset2);
    }

    public float NormalizedThrust()
    {
        return Mathf.Clamp((thrust - 0.4f) / 0.6f, 0, 1f);
    }

    void PrintLog(string log)
    {
        if (showLogs) Debug.Log("PlayerController: " + log);
    }

}