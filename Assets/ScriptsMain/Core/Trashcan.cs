using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private VisualSelectionObject _visualSelectionObject;

    public void SelectState(VisualSelectionObject.Select select)
    {
        _visualSelectionObject.ChangeState(select);
    }
}
