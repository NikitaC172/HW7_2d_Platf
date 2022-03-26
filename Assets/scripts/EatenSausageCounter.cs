using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class EatenSausageCounter : MonoBehaviour
{
    private int _count = 0;
    private Text _textCount = null;

    public void Increase()
    {
        _count++;
        _textCount.text = Convert.ToString(_count);
    }

    private void Awake()
    {
        _textCount = GetComponent<Text>();
    }
}
