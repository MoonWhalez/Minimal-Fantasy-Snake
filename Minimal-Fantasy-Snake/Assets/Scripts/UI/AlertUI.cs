using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour
{
    public static AlertUI instance;

    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI _textAlert;
    [SerializeField] private TextMeshProUGUI _subtextAlert;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void SetAlertText(string _text, string _subText)
    {
        _textAlert.text = _text;
        _subtextAlert.text = _subText;
    }

    public void ShowAlert(int time = 2)
    {
        StopAllCoroutines();
        UI.SetActive(true);
        gameObject.transform.localScale = Vector3.one * 0.5f;

        _confirmButton.gameObject.SetActive(false);
        _cancelButton.gameObject.SetActive(false);

        StartCoroutine(AutoHide(time));
    }

    public void ShowConfirmAlert(bool _isActive)
    {
        StopAllCoroutines();
        gameObject.transform.localScale = Vector3.one;

        _confirmButton.gameObject.SetActive(true);
        _cancelButton.gameObject.SetActive(true);

        UI.SetActive(_isActive);
    }

    IEnumerator AutoHide(int _time)
    {
        yield return new WaitForSeconds(_time);
        UI.SetActive(false);
    }

    public void SetAlertButtonsAction(Action _confirmAction = null, Action _cancelAction = null)
    {
        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(() => { _confirmAction(); UI.SetActive(false); });

        _cancelButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.AddListener(() => { _cancelAction(); UI.SetActive(false); });
    }
}
