using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfirmationDialog : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action confirmAction;

    public void ShowDialog(string title, string message, Action onConfirm)
    {
        titleText.text = title;
        messageText.text = message;
        confirmAction = onConfirm;
        gameObject.SetActive(true);
        
        // 重置按钮状态
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        
        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
    }

    private void Confirm()
    {
        Debug.Log("confirm");
        confirmAction?.Invoke();
        gameObject.SetActive(false);
    }

    private void Cancel()
    {
        Debug.Log("cancel");

        gameObject.SetActive(false);
    }
}