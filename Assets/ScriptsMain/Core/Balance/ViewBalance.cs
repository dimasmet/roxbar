using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewBalance : MonoBehaviour
{
    [SerializeField] private Text _balanceText;
    [SerializeField] private Animator _animator;

    public void SetValueView(int value, bool isScale = false)
    {
        _balanceText.text = value.ToString();

        if (isScale)
        {
            _animator.Play("Scale");
        }
    }
}
