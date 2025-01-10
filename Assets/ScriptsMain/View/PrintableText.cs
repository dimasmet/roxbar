using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrintableText
{
    private float _speed;
    private Text _textField;
    private string _textToPrint;

    public PrintableText(Text text,float speed)
    {
        _textField = text;
        _speed = speed;
    }

    public void SetTextPrint(string printString)
    {
        _textToPrint = printString;
        Reset();
    }

    public void Reset()
    {
        _textField.text = "";
    }

    public IEnumerator WaitPrint()
    {
        foreach (char c in _textToPrint)
        {
            _textField.text += c;
            yield return new WaitForSeconds(_speed);
        }
    }
}
