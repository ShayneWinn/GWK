using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    public float maxSpeed = 5f;

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
    }
}
