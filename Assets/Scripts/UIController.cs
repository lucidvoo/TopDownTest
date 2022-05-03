using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// Управление логикой UI, действия кнопок.

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject objectInfo;
    [SerializeField] private TextMeshProUGUI objInfoText;

    // если панель ObjectInfo активна, то здесь хранится объект о котором речь.
    private InteractiveObjectBase currentActiveObj = null;
    private int prevHealth = -1, 
                prevExp = -1;


    private void Start()
    {
        deathScreen.gameObject.SetActive(false);
        objectInfo.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        Events.onPlayerDeath.AddListener(ShowDeathScreen);
        Events.onInteractiveObjectClicked.AddListener(ShowObjectInfo);
        Events.onObjectBecomeNotInteractive.AddListener(TryHideObjInfo);
    }


    private void OnDisable()
    {
        Events.onPlayerDeath.RemoveListener(ShowDeathScreen);
        Events.onInteractiveObjectClicked.RemoveListener(ShowObjectInfo);
        Events.onObjectBecomeNotInteractive.RemoveListener(TryHideObjInfo);
    }


    private void ShowDeathScreen(string obj)
    {
        deathScreen.gameObject.SetActive(true);
    }


    // Обработчик кнопки Restart на экране смерти
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        // значения индикаторов присваиваются только если они изменились
        if (prevHealth != playerStats.Health)
        {
            prevHealth = playerStats.Health;
            healthText.text = playerStats.Health.ToString();
        }

        if (prevExp != playerStats.Exp)
        {
            prevExp = playerStats.Exp;
            expText.text = playerStats.Exp.ToString();
        }
    }


    // отображение панели с информацией об объекте, который был нажат
    private void ShowObjectInfo(InteractiveObjectBase objClicked)
    {
        if (currentActiveObj == objClicked)
        {
            return;
        }
        else
        {
            currentActiveObj = objClicked;
        }
        
        objectInfo.gameObject.SetActive(true);
        objInfoText.text = objClicked.description;
    }


    // если игрок вышел из области интерактивности объекта, то нужно спрятать Object Info panel
    private void TryHideObjInfo(InteractiveObjectBase objLostInteractivity)
    {
        if (currentActiveObj != objLostInteractivity)
        {
            return;
        }

        currentActiveObj = null;
        objectInfo.gameObject.SetActive(false);
    }

}
