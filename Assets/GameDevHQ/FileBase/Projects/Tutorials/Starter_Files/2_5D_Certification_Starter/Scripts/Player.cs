using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _potenciaSalto = 5.0f;
    [SerializeField]
    private float _gravedad = -1.0f;
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _velocidadVertical, _velocidadHorizontal;
    private Animator _anim;
    private bool _jumping;
    private bool _ledgeGrabbed;
    private Vector3 _standUpCoord;


    CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Error: CharacterController es null");
        }

        _anim = GetComponentInChildren<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Error: Animator es null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /////DEBUG DE SPACE KEY
        if(Input.GetKeyDown(KeyCode.Space) && !_controller.isGrounded)
        {
            Debug.Log("espacio y no esta grounded!");
        }


        if (_controller.enabled == true)
        {
            Movement();
        }
        if (_ledgeGrabbed && Input.GetKeyDown(KeyCode.E))
        {
            _anim.SetTrigger("ClimbUp");
        }
    }

    public void Movement()
    {

        if (_controller.isGrounded)
        {
            _velocidadVertical = _gravedad;

            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Saltar");
                _velocidadVertical = _potenciaSalto;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }

            _velocidadHorizontal = Input.GetAxisRaw("Horizontal") * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(_velocidadHorizontal));

            if (_velocidadHorizontal > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (_velocidadHorizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }



        }
        else
        {
            _velocidadVertical += _gravedad;
        }

        _controller.Move((new Vector3(0, _velocidadVertical, _velocidadHorizontal) * Time.deltaTime));


    }

 public void EdgeGrab(float xCoord,float yCoord, float zCoord, Vector3 standUpCoord)
    {
        _anim.SetTrigger("LedgeGrab");
        _anim.SetFloat("Speed", 0.0f);
        _anim.SetBool("Jumping", false);
        //_gravedad = 0.0f;
        _velocidadVertical = 0.0f;
        _controller.enabled = false;
        transform.position = new Vector3(xCoord, yCoord, zCoord);
        _ledgeGrabbed = true;
        _standUpCoord = standUpCoord;
        
    }

    public void StandedUp()
    {
        _ledgeGrabbed = false;
        transform.position = _standUpCoord;
        _controller.enabled = true;
        Debug.Log("Controller enabled");
        
    }
}
