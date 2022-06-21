using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonNumberController : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI textValue;
    [SerializeField] int value;

    private void Start()
    {
        button.onClick.AddListener(() => OnPressButton());
    }

    public void SetInfo(int value)
    {
        this.value = value;
        textValue.text = value.ToString();
    }

    public void OnPressButton()
    {
        GamePlayController.Instance.OnPressHandle(value);
    }
}
