using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DogQuote : MonoBehaviour
{
    [SerializeField]
    private float _showQuoteFor;

    private Text _text;
    private string _currentQuote;

    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "";
    }
    
    public void ShowQuote(Dog dog)
    {
        if (_text.text != "")
            return;

        var quotes = dog.Profile.Quotes;
        int r = Random.Range(0, quotes.Length);
        StartCoroutine(ShowQuote(quotes[r]));
    }

    private IEnumerator ShowQuote(string quote)
    {
        _text.text = quote;
        yield return new WaitForSeconds(_showQuoteFor);
        _text.text = "";
    }
}
