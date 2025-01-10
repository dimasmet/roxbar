using UnityEngine;

public class ScreenGame : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Animator _animScreen;

    public virtual void ShowScreen()
    {
        if (_animScreen == null)
            _screen.SetActive(true);
        else
            _animScreen.Play("Show");
    }

    public virtual void HideScreen()
    {
        if (_animScreen == null)
            _screen.SetActive(false);
        else
            _animScreen.Play("Hide");
    }
}
