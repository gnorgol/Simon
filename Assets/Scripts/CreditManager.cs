using UnityEngine;

public class CreditManager : MonoBehaviour
{
 public GameObject CreditPanel;

    void Start()
    {
        CreditPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CreditPanel.activeSelf)
            {
                HideCreditPanel();
            }
        }
    }
    public void ShowCreditPanel()
    {
        CreditPanel.SetActive(true);
    }
    public void HideCreditPanel() {
        CreditPanel.SetActive(false);
    }
    public void ClickOnCreditButton()
    {
        if (CreditPanel.activeSelf)
        {
            HideCreditPanel();
        }
        else
        {
            ShowCreditPanel();
        }
    }
}
