using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private StageProgressEvents stageProgressEvents;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button replayBtn;

    private void OnEnable()
    {
        stageProgressEvents.OnGameOver += ActivateGameOverPanel;
    }

    private void OnDisable()
    {
        stageProgressEvents.OnGameOver -= ActivateGameOverPanel;
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        replayBtn.onClick.AddListener(OnClickReplayBtn);
    }

    private void ActivateGameOverPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void OnClickReplayBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
