using UnityEngine;
using DG.Tweening;

public class PlayerTweenController : MonoBehaviour
{
    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private float jumpPower = 1.5f;
    [SerializeField] private float tweenDuration = 0.5f;
    [Space(5)]
    [SerializeField] private float rotationDuration = 0.25f;

    private PlayerControls _input;
    private bool _isAnimating = false;

    private void Awake()
    {
        _input = new PlayerControls();
        _input.Player.Jump.performed += _ => JumpForward();
        _input.Player.Move.performed += ctx => HandleRotation(ctx.ReadValue<Vector2>().x);
    }

    private void JumpForward()
    {
        if (_isAnimating) return;
        
        _isAnimating = true;

        Vector3 jumpTarget = transform.position + transform.forward * jumpDistance;
        transform.DOJump(jumpTarget, jumpPower, 1, tweenDuration).OnComplete(() => _isAnimating = false).SetEase(Ease.Linear);
    }

    private void HandleRotation(float direction)
    {
        if (DOTween.IsTweening(transform)) return;
        if (Mathf.Abs(direction) < 0.1f) return;

        float rotationAngle = direction > 0 ? 90f : -90f;
        transform.DOBlendableRotateBy(new Vector3(0, rotationAngle, 0), rotationDuration).SetEase(Ease.OutQuad);
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();
}
