using UnityEngine;
using UnityEngine.UI;

public class ClientBookButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _noActiveSprite;

    [Header("View button")]
    [SerializeField] private Text _nameClient;
    [SerializeField] private Text _percent;
    [SerializeField] private Image _imageCharacter;

    [Header("Line Mood client")]
    [SerializeField] private Image _fillImageLine;

    private Sprite _imgBig;

    private ClientData client;
    private ClientDataToChanged clientDataToChanged;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            BookHandler.Instance.ChoiceClient(this);
        });
    }

    public void InitClient(ClientData clientData, ClientDataToChanged clientDataToChanged, Sprite imgSmall, Sprite imgBig)
    {
        client = clientData;
        this.clientDataToChanged = clientDataToChanged;

        _imgBig = imgBig;

        _nameClient.text = client.nameClient;
        _imageCharacter.sprite = imgSmall;
        SetValueCurClient(clientDataToChanged.valueMood);

        if (clientDataToChanged.stateClient == StateClient.Close)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetValueCurClient(int valueMood)
    {
        float percentValueFill = valueMood / 100f;
        _fillImageLine.fillAmount = percentValueFill;

        _percent.text = "+" + valueMood + "%";
    }

    public void UpdateViewActualData()
    {
        if (clientDataToChanged.stateClient == StateClient.Open)
        {
            gameObject.SetActive(true);
        }
        SetValueCurClient(clientDataToChanged.valueMood);
    }

    public void StateChoiceClient(bool isChoiced)
    {
        if (isChoiced)
        {
            _button.GetComponent<Image>().sprite = _activeSprite;
        }
        else
        {
            _button.GetComponent<Image>().sprite = _noActiveSprite;
        }
    }

    public Sprite GetImageBig()
    {
        return _imgBig;
    }
}
