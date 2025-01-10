using UnityEngine;

public class StarsView : MonoBehaviour
{
    [SerializeField] GameObject[] _starsActive;

    public void SetCountStar(int count)
    {
        count = Mathf.Clamp(count, 0, 5);

        HideAllStars();

        for (int i = 0; i < count; i++)
        {
            _starsActive[i].SetActive(true);
        }
    }

    private void HideAllStars()
    {
        for (int i = 0; i < _starsActive.Length; i++)
        {
            _starsActive[i].SetActive(false);
        }
    }
}
