using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairController : MonoBehaviour
{
    public Animator anim;
    private CharacterController _cc;
    Vector3 velocity;
    [SerializeField] float _speed = 3.5f;
    [SerializeField] private float _speedRotation = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float side = 0;
        float move = 0;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            side = 0;
            move = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            side = 1;
            move = 1;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            side = -1;
            move = 1;
        }
        velocity.x = side * _speed;
        velocity.z = move * _speed;
        anim.SetFloat("turn", side);
        anim.SetFloat("move", move);
        //_cc.Move(velocity * Time.deltaTime);
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += side * _speedRotation;
        transform.localEulerAngles = rotation;
    }
}
