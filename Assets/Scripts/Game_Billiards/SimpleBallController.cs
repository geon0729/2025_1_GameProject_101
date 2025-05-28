using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class SimpleBallController : MonoBehaviour
{
    [Header("기본 설정")]
    public float power = 10f;                //타격 힘
    public Sprite arrowSprite;               //화살표 이미지

    private Rigidbody rb;                    //공의 물리
    private GameObject arrow;                //화살표 오브젝트
    private bool isDragging = false;         //드래그 중인지 확인 하는 bool
    private Vector3 startPos;                //드래그 시작 위치

    // Start is called before the first frame update
    void Start()
    {
        SetupBall();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdataArrow();
    }

    void SetupBall()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        //물리 설정
        rb.mass = 1;
        rb.drag = 1;
    }

    public bool IsMoving()                                  //공이 움직이고 있는지 확인
    {
        return rb.velocity.magnitude > 0.2f;                //공이 속도를 가지고 있으면 움직인다고 판단
    }

    void HandleInput()
    {
        if (!SimpleTurnManager.canPlay) return;
        if (SimpleTurnManager.anyBallMoveing) return;

        if (IsMoving()) return;                             //공이 움직이고 있으면 조작 불가

        if (Input.GetMouseButtonDown(0))                    //마우스 클릭 시작
        {
            StartDrag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)        //드래그 중이였는데 마우스 버튼 업 했을 때
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseDelta = Input.mousePosition - startPos;
        float force = mouseDelta.magnitude * 0.01f * power;

        if (force < 5) force = 5;

        Vector3 direction = new Vector3(-mouseDelta.x, 0, - mouseDelta.y).normalized;

        rb.AddForce(direction *  force, ForceMode.Impulse);

        SimpleTurnManager.OnBallHit();

        //공 발사 이후 변수들 정리
        isDragging = false;
        Destroy(arrow);
        arrow = null;

        Debug.Log("발사! 힘 : " +  force);
    }

    void CreateArrow()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }

        arrow = new GameObject("Arrow");
        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();

        sr.sprite = arrowSprite;
        sr.color = Color.green;
        sr.sortingOrder = 10;

        arrow.transform.position = transform.position + Vector3.up;
        arrow.transform.localScale = Vector3.one;
    }

    void UpdataArrow()
    {
        if(!isDragging || arrow == null) return;

        Vector3 mouseDelta = Input.mousePosition - startPos;
        float distance = mouseDelta.magnitude;

        float size = Mathf.Clamp(distance * 0.01f, 0.5f, 2.0f);
        arrow.transform.localScale = Vector3.one * size;

        SpriteRenderer sr = arrow.GetComponent<SpriteRenderer>();
        float colorRatio = Mathf.Clamp01(distance * 0.005f);
        sr.color = Color.Lerp(Color.green, Color.red, colorRatio);

        if(distance > 10f)
        {
            Vector3 direction = new Vector3(-mouseDelta.x, 0, -mouseDelta.y);


            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(90, angle, 0);
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                startPos = Input.mousePosition;
                CreateArrow();
                Debug.Log("드래그 시작");
            }
        }
    }
}
