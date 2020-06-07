using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSetting : MonoBehaviour
{
    [SerializeField] private string m_up;
    [SerializeField] private string m_down;
    [SerializeField] private string m_left;
    [SerializeField] private string m_right;

    public string up { get => m_up;  }
    public string down { get => m_down; }
    public string left { get => m_left; }
    public string right { get => m_right; }
    public float Horizontal()
    {
        var left = Input.GetKey(m_left);
        var right = Input.GetKey(m_right);
        if(left && right)
        {
            return 0;
        }
        else if(left)
        {
            return -1;
        }
        else if(right)
        {
            return 1;
        }
        return 0;
    }
    public float Vertical()
    {
        var up = Input.GetKey(m_up);
        var down = Input.GetKey(m_down);
        if (up && down)
        {
            return 0;
        }
        else if (up)
        {
            return 1;
        }
        else if (down)
        {
            return -1;
        }
        return 0;
    }
}
