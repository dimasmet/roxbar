using UnityEngine;
using UnityEngine.UI;

public class MessageClientView : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [Header("Buttons")]
    [SerializeField] private Button _nextButton;

    [Header("TextFields")]
    [SerializeField] private Text _nameClientText;
    [SerializeField] private Text _messageText;

    [Header("Speed print message")]
    [SerializeField] private float _speedPrint;
    private PrintableText _printableText;

    private bool isOpenView = true;

    private SplitText _currentSplitString;

    private void Awake()
    {
        _nextButton.onClick.AddListener(() =>
        {
            if (_currentSplitString != null)
            {
                PrintMessage(_currentSplitString.secondPart);
                _nextButton.gameObject.SetActive(false);

                Invoke(nameof(CloseMessageWindow), 10);
            }
        });
    }

    private void Start()
    {
        _printableText = new PrintableText(_messageText, _speedPrint);

        CloseMessageWindow();
    }

    public void SetMessage(string nameClient, string message, bool isActiveBtnNext)
    {
        if (isOpenView == false)
        {
            isOpenView = true;
            _anim.Play("Show");
        }

        _nameClientText.text = nameClient;

        PrintMessage(message);

        _nextButton.gameObject.SetActive(isActiveBtnNext);
    }

    public void SetLongMessage(SplitText splitText)
    {
        _currentSplitString = splitText;
        PrintMessage(_currentSplitString.firstPart);
        _nextButton.gameObject.SetActive(true);
    }

    public void CloseMessageWindow()
    {
        if (isOpenView)
        {
            isOpenView = false;
            _anim.Play("Hide");
        }
    }

    private void PrintMessage(string message)
    {
        StopAllCoroutines();
        _printableText.SetTextPrint(message);
        StartCoroutine(_printableText.WaitPrint());
    }
}
