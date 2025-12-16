using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    [SerializeField] int money;
    public int total_money;
    public Text moneyText;
    public GameObject effect;
    public GameObject button;
    public AudioSource audioSource;

    private Coroutine idleCoroutine; 
    
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        money = PlayerPrefs.GetInt("money", 0);
        total_money = PlayerPrefs.GetInt("total_money", 0);
        if (effect == null)
            Debug.LogWarning("Effect GameObject не назначен в инспекторе!");
        if (button == null)
            Debug.LogWarning("Button GameObject не назначен в инспекторе!");
        if (moneyText == null)
            Debug.LogWarning("MoneyText не назначен в инспекторе!");
            
        StartIdleFarm();
    }

    private void StartIdleFarm()
    {
        if (idleCoroutine != null)
            StopCoroutine(idleCoroutine);
            
        idleCoroutine = StartCoroutine(IdleFarm());
    }

    public void ButtonClick()
    {
        money++;
        total_money++;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("total_money", total_money);
        PlayerPrefs.Save(); 
        
        if (effect != null && button != null)
        {
            Vector3 buttonPosition = button.transform.position;
            Instantiate(effect, buttonPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Не могу создать эффект: effect или button равен null!");
        }
        audioSource.Play();
    }

    IEnumerator IdleFarm()
    {
        Debug.Log("Пассивный доход запущен!");
        
        while (true) 
        {
            yield return new WaitForSeconds(1);
            
            money++;
            total_money++;
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("total_money", total_money);
            Debug.Log($"Пассивный доход: money={money}, total={total_money}");
        }
    }

    // Удаляем лишний метод или используем его вместо корутины
    private void StartIdleFarmSimple()
    {
        InvokeRepeating(nameof(AddIdleMoney), 1f, 1f);
    }
    
    private void AddIdleMoney()
    {
        money++;
        total_money++;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("total_money", total_money);
        Debug.Log($"Пассивный доход: {money}");
    }

    public void ToAchievements()
    {
        PlayerPrefs.Save();
        
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    void Update()
    {
        if (moneyText != null)
            moneyText.text = money.ToString();
    }
    
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    
    // Добавляем метод для очистки
    void OnDestroy()
    {
        if (idleCoroutine != null)
            StopCoroutine(idleCoroutine);
    }
}