using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogQuote : MonoBehaviour
{
    [SerializeField]
    private float _minDisplayTime;
    [SerializeField]
    private float _maxDisplayTime;
    
    [SerializeField]
    private Text _text;
    private string _currentQuote;

    private float _startShow;

    void Start()
    {
        _text.text = "";
    }
    
    public void ShowQuote(Dog dog)
    {
        if (_text.text != "" && Time.time - _startShow < _minDisplayTime)
            return;
        
        var quotes = dog.Profile.Quotes;
        int r = Random.Range(0, quotes.Length);
        _startShow = Time.time;
        StartCoroutine(ShowQuote(quotes[r]));
        dog.Bork();
    }

    private IEnumerator ShowQuote(string quote)
    {
        _text.text = quote;
        yield return new WaitForSeconds(_maxDisplayTime);
        _text.text = "";
    }
}
