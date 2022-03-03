using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    //private Animator playerAnimator;

    private float horizontal;
    private float vertical;

    private bool isSitDown = false;
    private bool isJumping = false;
    private bool isJumpCommandWaiting = false;
    private float groundCheckLimit = 0.05f+1f;
    private float distanceFromGround = 0;

    public float walkForce = 200;
    public float jumpForce = 200;
    public float additionForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        //playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        GroundCheck();
        Walk();

        if (Input.GetKey(KeyCode.LeftControl))
            isSitDown = true;
        else
            isSitDown = false;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (distanceFromGround < groundCheckLimit && !isJumping)
            {
                Jump();
            }
        }
        if (distanceFromGround < groundCheckLimit)
        {
            isJumping = false;
        }



        //playerAnimator.SetFloat("Horizontal", horizontal);
        //playerAnimator.SetFloat("Vertical", vertical);
        //playerAnimator.SetFloat("DistanceFromGround", distanceFromGround);
        //playerAnimator.SetBool("Crouch", isSitDown);
    }
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast((player.position + Vector3.up), Vector3.down, out hit))
            distanceFromGround = player.position.y - hit.point.y;
    }
    void ChangeState(ref bool state)
    {
        if (state)
            state = false;
        else
            state = true;
    }
    void Jump()
    {
        //playerAnimator.SetTrigger("Jump");
        isJumping = true;
        player.AddRelativeForce(new Vector3(jumpForce * horizontal, jumpForce, jumpForce * vertical));
    }
    void Walk()
    {
        if(isSitDown)
            player.velocity = player.transform.rotation *
            new Vector3
            (horizontal * (walkForce / 2/2) * Time.deltaTime,//SideWalk
            player.velocity.y,//up
            vertical * walkForce/2 * Time.deltaTime);//front/ back
        else
            player.velocity = player.transform.rotation *
            new Vector3
            (horizontal * (walkForce / 2) * Time.deltaTime,//SideWalk
            player.velocity.y,//up
            vertical * walkForce * Time.deltaTime);//front/ back
    }
}

//=========================================================== 애니메이션 포함 =====================================================//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public Rigidbody player;
//    private Animator playerAnimator;

//    private float horizontal;
//    private float vertical;

//    private bool isSitDown = false;
//    private bool isJumping = false;
//    private bool isJumpCommandWaiting = false;
//    private float groundCheckLimit = 0.05f;
//    private float distanceFromGround = 0;

//    public float walkForce = 200;
//    public float jumpForce = 200;
//    public float additionForce = 100;

//    // Start is called before the first frame update
//    void Start()
//    {
//        playerAnimator = player.GetComponent<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        horizontal = Input.GetAxis("Horizontal");
//        vertical = Input.GetAxis("Vertical");

//        GroundCheck();
//        Walk();

//        if (Input.GetKey(KeyCode.LeftControl))
//            isSitDown = true;
//        else
//            isSitDown = false;

//        /*jump command wait*/
//        if (distanceFromGround < groundCheckLimit &&
//            !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
//            isJumpCommandWaiting)
//        {
//            Jump();
//            isJumpCommandWaiting = false;
//        }
//        /*jump command wait*/

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            /*jump command wait*/
//            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
//                isJumping == true)
//                isJumpCommandWaiting = true;
//            /*jump command wait*/

//            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
//                isJumping = true;
//            else
//                isJumping = false;

//            if (distanceFromGround < groundCheckLimit && !isJumping)
//            {
//                Jump();
//            }
//        }



//        playerAnimator.SetFloat("Horizontal", horizontal);
//        playerAnimator.SetFloat("Vertical", vertical);
//        playerAnimator.SetFloat("DistanceFromGround", distanceFromGround);
//        playerAnimator.SetBool("Crouch", isSitDown);
//    }
//    void GroundCheck()
//    {
//        RaycastHit hit;
//        if (Physics.Raycast((player.position + Vector3.up), Vector3.down, out hit))
//            distanceFromGround = player.position.y - hit.point.y;
//    }
//    void ChangeState(ref bool state)
//    {
//        if (state)
//            state = false;
//        else
//            state = true;
//    }
//    void Jump()
//    {
//        playerAnimator.SetTrigger("Jump");
//        isJumping = true;
//        player.AddRelativeForce(new Vector3(jumpForce * horizontal, jumpForce, jumpForce * vertical));
//    }
//    void Walk()
//    {
//        if (isSitDown)
//            player.velocity = player.transform.rotation *
//            new Vector3
//            (horizontal * (walkForce / 2 / 2) * Time.deltaTime,//SideWalk
//            player.velocity.y,//up
//            vertical * walkForce / 2 * Time.deltaTime);//front/ back
//        else
//            player.velocity = player.transform.rotation *
//            new Vector3
//            (horizontal * (walkForce / 2) * Time.deltaTime,//SideWalk
//            player.velocity.y,//up
//            vertical * walkForce * Time.deltaTime);//front/ back
//    }
//}