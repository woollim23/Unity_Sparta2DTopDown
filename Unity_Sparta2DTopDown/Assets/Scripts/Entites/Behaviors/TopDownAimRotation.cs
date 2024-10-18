using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        // 마우스의 위치가 들어오는 OnLookEvent에 등록하는 것
        // 마우스의 위치를 받아서 팔을 돌리는 데 활용할 것임.
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        // OnLook
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        // Atan2는 직각삼각형이 있다고 할 때 세로가 y, 가로가 x일 때 그 각도를 라디안 [-Pi,Pi]로 나타내는 함수임
        // 라디안의 -Pi는 -180도, Pi는 180도 이므로 Mathf.Rad2Deg는 약 57.29임 (180 / 3.14)

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 오일러에 넣어야 하므로 디그리로 변경해야 함
        // Atan2이므로 매개변수는 y x 순이어야함
        // 캐릭터에서 마우스를 바라보는 각도를 얻음
        // 좌표(y, x) -> 라디안 -> 각도

        // [1. 캐릭터 뒤집기]
        // 이때 각도는 오른쪽(1,0 방향)이 0도이므로,
        // -90~90도에서는 오른쪽을 바라보는 게 맞지만, -90도 미만 90도 초과라면 왼쪽을 바라보는 것임.
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        // 절대값이 90도 보다 크면 플립
        // 왼쪽 구간에 있으면 뒤집어라

        armRenderer.flipY = characterRenderer.flipX;

        // [2. 팔 돌리기]
        // 팔을 돌릴 때는 나온 각도를 그대로 적용하는데, 이때 유니티 내부에서 사용하는 쿼터니언으로 변환한다.
        // 쿼터니으로 변형하는 방법 두 가지
        // 1) Vector3를 Quaternion으로 변환해서 넣는 방법
        //    Quaternion.Euler(x 회전, y 회전, z 회전) : 오일러 각 기준으로 값을 넣으면 쿼터니언으로 변환됨
        // 2) eulerAngles를 통해 자동으로 변환되게 하는 방법 - rotation이랑 비슷하게 변환이 되어서 들어간다고 생각하면 됩니다.
        //    Transform.eulerAngles을 변경
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ); // z축으로 회전해야함
        // (2번 방법으로 하면) armPivot.eulerAngles = new Vector3(0, 0, rotZ);
        // Quaternion.Euler를 이용해 각도를 지정해줌
    }
}