using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowNumberController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] int number = 0;

    private void Start()
    {
        //number = 0;
        //numberText.text = "";
    }

    public void SetInfo(int value)
    {
        numberText.text = value.ToString();
        number = value;
    }

    public void HideInfo()
    {
        number = 0;
        numberText.text = "";
    }
}
