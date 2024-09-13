using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
//sử dụng thư viện AI
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public ParticleSystem[] muzzleFlash;
    public TrailRenderer tracerEffect;
    public Transform[] gunBarrel;
    private Rigidbody rigidbody;
    private StateMachine stateMachine;
    //Mạng lưới các điểm để boss có thể di chuyển
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;
    //Nhận vào vị trí của người chơi để update các farm theo thời gian thực
    //tạo ra phương thức Agent để lấy giá trị từ agent
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnowPos {  get => lastKnowPos; set => lastKnowPos = value; }
    //Tuyến đường di chuyển của boss
    public Path path;
    //Vị trí cuối cùng của khi nhìn thấy player.
    public GameObject debugSphere;
    private Animator animator;
    [Header("Signt Values")]
    //Tầm nhìn của boss
    public float signtDistance = 20f;
    public float fielOfView = 85f;
    public float eyeHeight;
    [Header("Weanpons Values")]
    //vị trí điểm bắn viên đạn

    //tốc độ bắn
    // câu lệnh tạo ra một thanh kéo có giá trị từ 0.1 - 10
    [Range(0.1f, 10f)]
    //tốc độ bắn bắn của boss
    public float fireRare = 3f;
    //dùng để Debug

    [SerializeField]
    private string currentState;

    protected Ray ray;
    protected RaycastHit hitInfo;

    public AudioClip clickSound;  // Gán âm thanh click chuột vào đây
    private AudioSource audioSource;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        //khởi tạo trạng thại cho boss
        stateMachine.Initialise();
        //FindGameObjectWithTag phương thức tìm kiếm vị trị của đối tượng player
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        debugSphere.transform.position = lastKnowPos;
        // Lấy vận tốc hiện tại của nhân vật
        Vector3 currentVelocity = agent.velocity;
       
        if (currentVelocity != Vector3.zero)
        {
            animator.SetBool("isRun", true);
            
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        if(currentState == "AttackState")
        {
            animator.SetBool("isRun", false);
            rigidbody.isKinematic = true;
            agent.speed = 0f;
        }
        else
        {
            agent.speed = 3f;
        }
        
      
        currentState = stateMachine.activeState.ToString();
        debugSphere.transform.position = lastKnowPos;

    }
    //tầm nhìn của boss
     public bool CanSeePlayer()
     {
        if(player != null)
        {
            //xác định vị của player nằm trong phạm vi tầm nhìn của boss
            if(Vector3.Distance(transform.position, player.transform.position) < signtDistance) 
            {
                //xác định phướng của player
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                if(angleToPlayer >= -fielOfView && angleToPlayer <= fielOfView)
                {
                    
                    ray =  new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    //kiểm tra xem đường truyền tia Raycast bị chặn hay không
                    hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, signtDistance))
                    {
                        //Nếu dường tia bị chăn và kiểm tra xem đó có phải là một player hay không
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * signtDistance);
                            return true;
                        }
                    }
                }

            }
        }
        return false;
     }

    public void PlayClickSound()
    {
        audioSource.volume = 0.4f;
        // Phát âm thanh click chuột từ AudioSource
        audioSource.PlayOneShot(clickSound);
    }
}
