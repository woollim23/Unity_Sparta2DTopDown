using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputController : TopDownController
{
    private Camera _camera;
    protected override void Awake()
    {
        base.Awake(); // 부모 클래스의 Awake 함수를 실행토록 함
        _camera = Camera.main; // 메인 카메라 라는 태그를 붙은 카메라를 가져온다
    }

    public void OnMove(InputValue value) // W input 값 같은 애들이 여기로 들어온다
    {
        // Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput); // TopDownController에서 상속 받아옴
        // 실제 움직이는 것은 여기서 하는 것이 아니라, PlayerMovement에서 함
    }

    public void OnLook(InputValue value) // W input 값 같은 애들이 여기로 들어온다
    {
        Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>(); // 마우스 위치 이기 떄문에 normalized 하면 안된다.
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;
        // transform.position 에서 월드가 어느 방향에 있는지 묻고 값을 얻는 것
        // 마우스(카메라)가 찍고 있는 방향을 월드좌표로 바꿔준다

        if (newAim.magnitude >= .9f)
        // Vector 값을 실수로 변환
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
