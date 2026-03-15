using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageProgressEvents;
    [SerializeField] private TMP_Text floorInfoText;
    [SerializeField] private TMP_Text itemCountText;
    [SerializeField] private TMP_Text enemyCountText;

    [SerializeField] private Image[] lifeImg;

    private int _displayedKillCount = 0;
    private Coroutine _killCountCoroutine;

    private void OnEnable()
    {
        stageProgressEvents.OnEnemyKillCountChanged += ChangeEnemyCountUI;
        stageProgressEvents.OnLivesChanged += ChangeLifeUI;
        stageProgressEvents.OnFloorChanged += ChangeFloorInfoUI;
    }

    private void OnDisable()
    {
        stageProgressEvents.OnEnemyKillCountChanged -= ChangeEnemyCountUI;
        stageProgressEvents.OnLivesChanged -= ChangeLifeUI;
        stageProgressEvents.OnFloorChanged -= ChangeFloorInfoUI;
    }

    private void ChangeFloorInfoUI(int currentFloor, int totalFloor)
    {
        floorInfoText.text = $"제 {currentFloor} / {totalFloor} 층";
    }

    private void ChangeItemCountUI(int total)
    {
        itemCountText.text = $"{total}";
    }

    private void ChangeLifeUI(int currentLives)
    {
        for (int i = 0; i < lifeImg.Length; i++)
        {
            lifeImg[i].enabled = i < currentLives;
        }
    }

    private void ChangeEnemyCountUI(int total)
    {
        if (_killCountCoroutine != null)
            StopCoroutine(_killCountCoroutine);

        _killCountCoroutine = StartCoroutine(AnimateText(enemyCountText, _displayedKillCount, total));
    }

    private IEnumerator AnimateText(TMP_Text target, int from, int to)
    {
        float originalSize = target.rectTransform.localScale.x;
        float punchSize = originalSize * 1.3f;
        float duration = 0.12f;

        for (int i = from + 1; i <= to; i++)
        {
            target.text = $"{i}";

            // 확장
            DOTween.Kill(target);
            target.rectTransform.DOScale(punchSize, duration * 0.4f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                    target.rectTransform.DOScale(originalSize, duration * 0.6f)
                        .SetEase(Ease.InQuad));

            _displayedKillCount = i;
            yield return new WaitForSeconds(duration);
        }

        _killCountCoroutine = null;
    }


}
