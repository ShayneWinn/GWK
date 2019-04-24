using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    public float maxSpeed = 5f;
    public LayerMask Interactable;


    enum Dir { UP, DOWN, LEFT, RIGHT};
    Dir m_Dir;
    CharacterController m_CharacterController;
    Vector2 m_NextMovement;

    // Start is called before the first frame update
    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _moveX = (Input.GetAxis("Horizontal") * maxSpeed) * Time.deltaTime;
        float _moveY = (Input.GetAxis("Vertical") * maxSpeed) * Time.deltaTime;
        m_NextMovement = new Vector2(_moveX, _moveY);
        m_CharacterController.move(m_NextMovement);
        //Debug.Log(m_NextMovement);

        if (Input.GetAxis("Vertical") > 0)
            m_Dir = Dir.UP;
        if (Input.GetAxis("Vertical") < 0)
            m_Dir = Dir.DOWN;
        if (Input.GetAxis("Horizontal") > 0)
            m_Dir = Dir.RIGHT;
        if (Input.GetAxis("Horizontal") < 0)
            m_Dir = Dir.LEFT;
        
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, GetDir(m_Dir), 0.5f, Interactable);
        Debug.DrawRay(transform.position, GetDir(m_Dir));
        if (_hit && Input.GetAxis("Interact") > 0)
        {
            _hit.transform.gameObject.GetComponent<Interactable>().Interact();
        }
            
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
