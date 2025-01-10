using UnityEngine;

public class VisualSelectionObject : MonoBehaviour
{
    public enum Select
    {
        Select,
        AnSelect
    }

    [SerializeField] private GameObject _activeSelect;
    [SerializeField] private GameObject _noActiveSelect;

    public void ChangeState(Select select)
    {
        switch (select)
        {
            case Select.Select:
                _activeSelect.SetActive(true);
                _noActiveSelect.SetActive(false);
                break;
            case Select.AnSelect:
                _activeSelect.SetActive(false);
                _noActiveSelect.SetActive(true);
                break;
        }
    }
}
