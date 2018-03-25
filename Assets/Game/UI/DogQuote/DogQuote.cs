using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DogQuote : MonoBehaviour
{
    [SerializeField]
    private float _minDisplayTime;
    [SerializeField]
    private float _maxDisplayTime;
    
    private Text _text;
    private string _currentQuote;
    private SFXManager _sfx;

    private float _startShow;

    void Start()
    {
        _sfx = this.Find<SFXManager>(GameTags.Audio);
        _text = GetComponent<Text>();
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
    }

    private IEnumerator ShowQuote(string quote)
    {
        _sfx.PlayPitchedSound(GameTags.Woof);
        _text.text = quote;
        yield return new WaitForSeconds(_maxDisplayTime);
        _text.text = "";
    }
}
