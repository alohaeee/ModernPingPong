using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_walls;
    [SerializeField] private Transform m_transform;
    [SerializeField] private float m_speed;
    private SpriteRenderer m_sprite;

    public ControllerSetting controller;

    public static float baseSpeed;
    public float speed { get => m_speed; set => m_speed = value; }

    private void Start()
    {
        baseSpeed = m_speed;
        m_sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<ControllerSetting>();
    }
    private void Update()
    {
        Move();
    }
    public void ResetSpeed()
    {
        m_speed = baseSpeed;
    }
    private void Move()
    {
        var vr = controller.Vertical();
        var yCordinate = m_transform.position.y + vr * m_speed * Time.deltaTime;
        var top = m_walls.bounds.center.y + m_walls.bounds.extents.y;
        var bottom = m_walls.bounds.center.y - m_walls.bounds.extents.y;

        if (yCordinate + m_sprite.bounds.extents.y < top && yCordinate - m_sprite.bounds.extents.y > bottom)
        {
            m_transform.Translate(0,vr * m_speed * Time.deltaTime,0);
        }
    }
}
