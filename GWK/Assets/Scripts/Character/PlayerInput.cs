using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    public float maxSpeed = 5f;
    public LayerMask Interactable;


    enum Dir { UP, DOWN, LEFT, RIGHT};
    private Dir m_Dir; // The player direction
    private CharacterController m_CharacterController; // The character controller to handle movement
    private Vector2 m_NextMovement; // The movement for the next frame
    private Animator m_Animator; // Player Sprite Animator


    private RaycastHit2D m_Interactable; // Interactables over time
    private bool m_CanInteract;

    // Start is called before the first frame update
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Interact Input
        if(Input.GetAxis("Interact") > 0)
        {
            if (m_CanInteract)
                m_Interactable.collider.gameObject.GetComponent<Interactable>().Interact();
        }
        //=====\\



        //Set Movement vectors
        float _moveX = (Input.GetAxisRaw("Horizontal") * maxSpeed) * Time.deltaTime;
        float _moveY = (Input.GetAxisRaw("Vertical") * maxSpeed) * Time.deltaTime;
        m_NextMovement = new Vector2(_moveX, _moveY);
        m_CharacterController.move(m_NextMovement);
        //=====\\



        //Choose Direction to send the interactables raycast
        if (Input.GetAxisRaw("Horizontal") > 0)
            m_Dir = Dir.RIGHT;
        if (Input.GetAxisRaw("Horizontal") < 0)
            m_Dir = Dir.LEFT;
        if (Input.GetAxisRaw("Vertical") > 0)
            m_Dir = Dir.UP;
        if (Input.GetAxisRaw("Vertical") < 0)
            m_Dir = Dir.DOWN;
        //=====\\



        //Animation Setting
        m_Animator.SetFloat("Blend", (float)m_Dir / 3);
        if (_moveX != 0 || _moveY != 0)
            m_Animator.SetBool("Walking", true);
        else
            m_Animator.SetBool("Walking", false);
        //=====\\



        //Get Interactables
            //Get interactables this frame
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, GetDir(m_Dir), 0.5f, Interactable);

        if(_hit)
        {
            m_Interactable = _hit;
            if (!m_CanInteract)
            {
                m_CanInteract = true;
                m_Interactable.collider.gameObject.GetComponent<Interactable>().SendMessage("Prompt");
            }
        }
        else
        {
            if (m_CanInteract)
            {
                m_Interactable.collider.gameObject.GetComponent<Interactable>().SendMessage("UnPrompt");
                m_CanInteract = false;
            }
        }
                


        Debug.DrawRay(transform.position, GetDir(m_Dir));
        //=====\\
    }
    Vector2 GetDir(Dir t_dir)
    {

        switch (t_dir)
        {
            case Dir.UP:
                return Vector2.up;

            case Dir.DOWN:
                return Vector2.down;

            case Dir.LEFT:
                return Vector2.left;

            case Dir.RIGHT:
                return Vector2.right;
        }
        return Vector2.up;
    }
}
