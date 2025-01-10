using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewBook : ScreenGame
{
    [SerializeField] private Button _closeBtn;

    [SerializeField] private Transform _container;
    [Header("Show character")]
    [SerializeField] private Image _characterImage;
    [SerializeField] private Animator _anim;

    private Sprite nextSprite;

    private void Awake()
    {
        _closeBtn.onClick.AddListener(() =>
        {
            HandlerScreensGame.Instance.CloseActiveScreen(this);

            SoundsGame.Instance.PlayShotSound(SoundName.Click);

            WorkBar.Instance.ActiveMaskObjects(true);
        });
    }

    public void ShowCharacter(Sprite sprite)
    {
        nextSprite = sprite;
        _anim.Play("Show");
        StartCoroutine(WaitChangeSprite());
    }

    private IEnumerator WaitChangeSprite()
    {
        yield return new WaitForSeconds(0.25f);
        _characterImage.sprite = nextSprite;
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        BookHandler.Instance.UpdateView();

        EventsGame.OnTrakingTouchActive?.Invoke(false);

        WorkBar.Instance.ActiveMaskObjects(false);
    }

    public override void HideScreen()
    {
        base.HideScreen();

        /*Vector2 pos = _container.transform.position;
        pos.y = 0;
        _container.transform.position = pos;*/

        EventsGame.OnTrakingTouchActive?.Invoke(true);
    }
}
