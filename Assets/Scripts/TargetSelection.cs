using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetType
{
    Red = 0,
    Green = 1,
    Blue = 2,
}

public class TargetSelection : MonoBehaviour
{
    [Serializable]
    private class TargetSetup
    {
        public TargetType TargetType = default;
        public GameObject Object = default;
    }

    [SerializeField] private List<TargetSetup> _targetList = default;
    [SerializeField] private Button _buttonLeft = default;
    [SerializeField] private Button _buttonRight = default;

    private int _currentTargetIndex = default;
    private int _previousTargetIndex = default;

    private void Start()
    {
        _buttonLeft.onClick.AddListener(SetPrevTarget);
        _buttonRight.onClick.AddListener(SetNextTarget);
    }

    private void SetNextTarget()
    {
        _previousTargetIndex = _currentTargetIndex;
        _currentTargetIndex = (_currentTargetIndex + 1) % _targetList.Count;
        SetTarget(_currentTargetIndex);
    }

    private void SetPrevTarget()
    {
        _previousTargetIndex = _currentTargetIndex;
        _currentTargetIndex = _currentTargetIndex - 1 < 0 ? _targetList.Count - 1 : (_currentTargetIndex - 1) % _targetList.Count;
        SetTarget(_currentTargetIndex);
    }

    private void SetTarget(int index)
    {
        _targetList[_previousTargetIndex].Object.SetActive(false);
        _targetList[index].Object.SetActive(true);
    }
}
