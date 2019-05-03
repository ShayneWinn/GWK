using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    public float maxSpeed = 5f;
    public LayerMask Interactable;
    public GameObject InteractText;


    enum Dir { UP, DOWN, LEFT, RIGHT};
    private Dir m_Dir;
    private CharacterController m_CharacterController;
    private Vector2 m_NextMovement;

    public List<RaycastHit2D> m_Interactable;

    // Start is called before the first frame update
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Interactable = new List<RaycastHit2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Interact Input
        if(Input.GetAxis("Interact") > 0)
        {
            if (m_Interactable.Count > 0)
                m_Interactable[0].collider.gameObject.GetComponent<Interactable>().Interact();
        }
        //=====\\


        //Set Movement vectors
        float _moveX = (Input.GetAxis("Horizontal") * maxSpeed) * Time.deltaTime;
        float _moveY = (Input.GetAxis("Vertical") * maxSpeed) * Time.deltaTime;
        m_NextMovement = new Vector2(_moveX, _moveY);
        m_CharacterController.move(m_NextMovement);
        //=====\\


        //Choose Direction to send the interactables raycast
        if (Input.GetAxis("Horizontal") > 0)
            m_Dir = Dir.RIGHT;
        if (Input.GetAxis("Horizontal") < 0)
            m_Dir = Dir.LEFT;
        if (Input.GetAxis("Vertical") > 0)
            m_Dir = Dir.UP;
        if (Input.GetAxis("Vertical") < 0)
            m_Dir = Dir.DOWN;
        //=====\\


        //Get Interactables
        m_Interactable.Clear();

        RaycastHit2D _hit = Physics2D.Raycast(transform.position, GetDir(m_Dir), 0.5f, Interactable);
        if (_hit)
            m_Interactable.Add(_hit);
        Debug.DrawRay(transform.position, GetDir(m_Dir));
        //=====\\

        Debug.Log(m_Interactable.Count);

        //Set Interact Text
        if (m_Interactable.Count > 0)
            InteractText.SetActive(true);
        else
            InteractText.SetActive(false);
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
