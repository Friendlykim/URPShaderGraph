using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    Rigidbody rigid = null;
    Animator anim = null;
    GameObject AnimObj = null;


   // public KeyCode dafd;
    [SerializeField]
   // Vector3 Movement_;
    Vector3 targeting_pos;
    [SerializeField]
    public float speed_ = 4f;
    float Horizontal_;
    float Vertical_;

  //  Transform cam;
 //   Vector3 camForward;

    //이동기준 벡터
    Vector3 move;
    Vector3 MoveInput;

    float forwardAmount;
    float turnAmount;

    //타겟위치 기준 벡터
    Vector3 AnimDirVector;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
      //  anim.CrossFade("Blend Tree", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
    }

    private void LateUpdate()
    {
        FixedTick();
    }

    void Init()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        AnimObj = gameObject.transform.GetChild(0).gameObject;

      //  cam = Camera.main.transform;
    }

    void Tick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,100))
        {
            targeting_pos = hit.point;
        }

        Vector3 target_dir = targeting_pos - transform.position;
        target_dir.y = 0;

        AnimObj.transform.LookAt(transform.position + target_dir, Vector3.up);



    }
    void FixedTick()
    {
        Horizontal_ = Input.GetAxis("Horizontal");
        Vertical_ = Input.GetAxis("Vertical");

        move = Vertical_ * Vector3.forward + Horizontal_ * Vector3.right;

        Move(move);

       Vector3 Movement_ = new Vector3 (Horizontal_, 0,Vertical_);

        transform.Translate(Movement_ * speed_ * Time.deltaTime);
    }

    //이동 인풋 받아오기,노멀라이즈
    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        this.MoveInput = move;
        ConvertLocalMove();
        UpdateAnimator();
    }

    // 월드 트렌스폼을 로컬트렌스폼으로 변환
    void ConvertLocalMove()
    {
        Vector3 localMove = transform.InverseTransformDirection(MoveInput);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;
    }

    //변환한 트렌스폼의 플롯값을 애니메이터에 지정
    void UpdateAnimator()
    {
        //애니메이터 파라미터와 플롯값연결 , DampTime : (중간에 플롯들어간 부분) 애니메이션 전환을 부드럽게해줌, 높일수록 느려짐
        anim.SetFloat("Horizontal", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Vertical", turnAmount, 0.1f, Time.deltaTime);
    }
}
