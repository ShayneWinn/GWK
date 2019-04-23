using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float SmoothX;
    public float SmoothY;

    private float m_posX;
    private float m_posY;
    private Vector2 m_velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_posX = Mathf.SmoothDamp(Target.position.x, transform.position.x, ref m_velocity.x, SmoothX);
        m_posY = Mathf.SmoothDamp(Target.position.y, transform.position.y, ref m_velocity.y, SmoothY);

        transform.position = new Vector3(m_posX, m_posY, transform.position.z);
    }
}
