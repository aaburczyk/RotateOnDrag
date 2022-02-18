using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RotateOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private float _autoBackDuration = 1.5f;
    [SerializeField] private GameObject _rotationTarget = default;
    [SerializeField] private bool _autoRotateBack = true;

    private float _rotationVelocity = default;
    private float _timeRemaining = default;
    private Vector3 _originalRotation = default;
    private Tween _tween = default;

    private void Start()
    {
        _originalRotation = _rotationTarget.transform.eulerAngles;
    }

    private void UpdateTarget(GameObject target)
    {
        _rotationTarget = target;
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _tween.Kill();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rotationVelocity = eventData.delta.x * _rotationSpeed;
        _rotationTarget.transform.Rotate(Vector3.up, -_rotationVelocity, Space.World);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TriggerAutoback();
    }

    private void TriggerAutoback()
    {
        if (!_autoRotateBack || _rotationTarget == null)
        {
            return;
        }

        _timeRemaining = Mathf.Abs(_autoBackDuration - (_autoBackDuration * _rotationTarget.transform.rotation.eulerAngles.y / 180));
        _tween = _rotationTarget.transform.DORotate(_originalRotation, _timeRemaining);
    }

    void OnApplicationQuit()
    {
        _tween.Kill();
    }
}
