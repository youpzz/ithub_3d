using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenUiTimer : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private float timerDuration = 10f;

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        if (progressBar == null) return;

        progressBar.fillAmount = 1f;

        progressBar.DOFillAmount(0f, timerDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("Молодец! Холодец!");
            });
    }

    public void ResetTimer()
    {
        // вообще изначально была идея прикрутить к кнопке в action map`е, но мне впадлу
        progressBar.DOKill();
        StartTimer();
    }
}
