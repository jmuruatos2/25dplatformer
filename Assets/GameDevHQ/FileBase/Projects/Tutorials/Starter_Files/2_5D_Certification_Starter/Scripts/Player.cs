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
    private float _speed = 4.0f, _ladderSpeed = 1.0f;
    [SerializeField]
    private float _velocidadVertical, _velocidadHorizontal;
    [SerializeField]
    private Animator _anim, _animCollider;
    private bool _jumping;
    private bool _ledgeGrabbed;
    private Vector3 _standUpCoord;
    private int _coins;
    private UIManager _uiManager;
    private bool _ladderAvailable;
    private bool _onLadder;
    private Vector3 _ladderBottom, _ladderTop, _landingTop, _landingBottom;
    private bool _exitingLadder;
    private bool _rolling;

   

    CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Error: CharacterController es null");
        }

        //_anim = GetComponentInChildren<Animator>();

        //if (_anim == null)
        //{
        //    Debug.LogError("Error: Animator es null");
        //}

        //_animCollider = GetComponent<Animator>();

        //if (_animCollider == null)
        //{
        //    Debug.LogError("Error: Animator Collider es null");
        //}

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager== null)
        {
            Debug.LogError("Error: UIManager Nulo");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && _ladderAvailable && !_onLadder)
        {
            _controller.enabled = false;
            _onLadder = true;
            _uiManager.LadderTextEnable(false);
            _anim.SetTrigger("OnLadder");
            transform.position = _ladderBottom;
            
        }

        if (_onLadder)
        {
            OnLadder();
            return;
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

            if (!_jumping && Input.GetKeyDown(KeyCode.LeftShift) && !_rolling)
            {
                _anim.SetTrigger("Roll");
                _animCollider.SetTrigger("Roll");
                _rolling = true;
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
        
        
    }

    public void ClimbedUp()
    {
        _onLadder = false;
        transform.position = _landingTop;
        _controller.enabled = true;
        _exitingLadder = false;
        _anim.SetBool("ClimbingLadder", false);
        _anim.SetBool("DescendingLadder", false);

    }

    public void Descend()
    {
        _onLadder = false;
        transform.position = _landingBottom;
        _controller.enabled = true;
        _exitingLadder = false;
        _anim.SetBool("ClimbingLadder", false);
        _anim.SetBool("DescendingLadder", false);

    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoins(_coins);
    }

    public void SetLadderAvailable(bool available, Vector3 ladderBottom, Vector3 ladderTop, Vector3 landingTop, Vector3 landingBottom)
    {
        _ladderBottom = ladderBottom;
        _ladderTop = ladderTop;
        _landingTop = landingTop;
        _landingBottom = landingBottom;
        _ladderAvailable = available;
        
        _uiManager.LadderTextEnable(available);

    }

    public void FinishedRolling()
    {
        _rolling = false;
    }
    public void OnLadder()
    {
        if (_exitingLadder)
        {
            return;
        }

        float verticalAxis = Input.GetAxisRaw("Vertical");
        float verticalMovement = verticalAxis * _ladderSpeed;

        if (transform.position.y <= _ladderBottom.y && verticalAxis < 0)
        {
            _anim.SetTrigger("Descend");
            _exitingLadder = true;
            Descend();

        }

        if (transform.position.y >= _ladderTop.y && verticalAxis > 0)
        {
            _anim.SetTrigger("LadderTop");
            _exitingLadder = true;
            
        }

        Debug.Log("Player Position: + " + transform.position.y + " , TopLadder poisition: " + _ladderTop.y + " vertical axis: " + verticalAxis);

       
        if (_exitingLadder)
        {
            return;
        }


        transform.position += new Vector3(0, verticalMovement, 0) * Time.deltaTime;
        switch (verticalAxis)
        {
            case 0:
                _anim.SetBool("ClimbingLadder", false);
                _anim.SetBool("DescendingLadder", false);
                break;
            case 1:
                _anim.SetBool("ClimbingLadder", true);
                _anim.SetBool("DescendingLadder", false);
                break;
            case -1:
                _anim.SetBool("ClimbingLadder", false);
                _anim.SetBool("DescendingLadder", true);
                break;
        }

        //if (verticalMovement > 0)
        //{
        //    _anim.SetBool("ClimbingLadder", true);
            
        //} else 
        //{
        //    _anim.SetBool("ClimbingLadder", false);
        //}

        //if (verticalMovement < 0)
        //{
        //    _anim.SetBool("DescendingLadder", true);
        //} else
        //{
        //    _anim.SetBool("DescendingLadder", false);
        //}

        //_controller.Move(new Vector3(0, verticalMovement, 0) * Time.deltaTime);


    }
}
