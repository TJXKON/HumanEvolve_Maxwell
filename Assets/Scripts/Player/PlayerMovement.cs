using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private bool facingRight = true;
    public bool IsCrouching = false;
    private bool isAttacking = false;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider crouchDisableCollider;
    [SerializeField] public Animator animator;

    [SerializeField] public float speed = 400f;
    [SerializeField] public float jumpForce = 300f;
    [SerializeField] public float exJumpRate = 0.5f;
    [SerializeField] public float crouchSpeedRate = 0.4f;
    [SerializeField] private float pushForce = 10f;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        isAttacking = GetComponent<PlayerAttack>().isAttacking;

        if (isAttacking&&isGrounded()){
            rb.velocity=new Vector3(0, rb.velocity.y, rb.velocity.z); //Freeze movement when attacking
        }
        //Horizontal Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        //animator.SetBool("isCrouching", IsCrouching);

        //Move and Ilde animations
        //animator.SetFloat("speed",Mathf.Abs(horizontal));


        //Flip player object
        Flip();

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded() && !IsCrouching && !isAttacking){
            //Change velocity of player object
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.fixedDeltaTime);
            //animator.SetBool("isJumping", true);
        }
        else if (isGrounded()){
            //animator.SetBool("isJumping",false);
        }
        //Reduce jump foce while fast tapping
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0f){
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*exJumpRate);
            
        }

        //Crouch
        if ((Input.GetAxisRaw("Vertical")<0)|| IsHeadHitted() && isGrounded() && !isAttacking){
            IsCrouching = true;
            Debug.Log("Is crouching");
            //Disable upper collider while crouching
            if (crouchDisableCollider != null){
                crouchDisableCollider.enabled = false;
            }
        }
        else {
            IsCrouching = false;
            if (crouchDisableCollider != null){
                crouchDisableCollider.enabled = true;
            }     

        }

         if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithDoor();
        }
    }

    void FixedUpdate()
    {
        //Movement of object (Speed depends of crouching condition)
        if (!isAttacking){
            if (IsCrouching){
                rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime * crouchSpeedRate, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
            }
        }

         if (!isAttacking)
        {
            // Check if the player is holding the interact key (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithBox();
            }
        }

    }

    private void Flip(){
        if (!isAttacking && (facingRight && horizontal < 0f || !facingRight && horizontal > 0f) ){
            facingRight = !facingRight;
            //Vector3 localScale = transform.localScale;
            //localScale.x *= -1f;
            //transform.localScale = localScale;

       

            transform.Rotate(0f,180f,0f);
        }
    }


    bool isGrounded(){

        Vector3 pos = groundCheck.position + Vector3.up*0.9f;

        return Physics.CheckSphere(pos, 0.9f, groundLayer);
    }

    private bool IsHeadHitted(){

        if (Physics.Raycast(headCheck.position, Vector3.up, out RaycastHit hit, 1.0f, groundLayer))
        {

            return true;
        }
        else
        {
            return false;
        }
    }

     private void InteractWithBox()
    {
        // Check if the player is grounded and close to a box
        if (isGrounded())
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f, LayerMask.GetMask("Box"));

          foreach (Collider hitCollider in hitColliders)
    {
        Rigidbody boxRigidbody = hitCollider.GetComponent<Rigidbody>();
        if (boxRigidbody != null)
        {
            // Use the pushForce variable to apply force to the box
            Vector3 pushDirection = facingRight ? transform.right : -transform.right;
            boxRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
        }
    }

    private void InteractWithDoor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.TryOpenDoor();
                }
            }
        }
    }

}
