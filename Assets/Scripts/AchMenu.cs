using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AchMenu : MonoBehaviour
{
    public int money;
    public int total_money;
    [SerializeField] Button firstAch;
    [SerializeField] bool isFirst;
    
    void Start()
    {
        money = PlayerPrefs.GetInt("money", 0);
        total_money = PlayerPrefs.GetInt("total_money", 0);
        isFirst = PlayerPrefs.GetInt("isFirst", 0) == 1;
        StartCoroutine(IdleFarm());
    }
    
 
    public void GetFirst()
    {
        if (!isFirst && total_money >= 100)
        {
            // Увеличиваем деньги (обратите внимание: money - это поле класса)
            money += 10;
            PlayerPrefs.SetInt("money", money);
            
            isFirst = true;
            PlayerPrefs.SetInt("isFirst", 1);
            
         
        }
    }

    IEnumerator IdleFarm()
    {
        Debug.Log("IdleFarm корутина начата");
        
        while (true) // Бесконечный цикл
        {
            yield return new WaitForSeconds(1);
            
            // Увеличиваем деньги
            money++;
            total_money++;
            
            // Сохраняем
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt("total_money", total_money);
            Debug.Log($"Idle: money={money}, total={total_money}");
        }
    }

    public void ToMenu()
    {
        PlayerPrefs.Save(); // Сохраняем перед переходом
        SceneManager.LoadScene(0);
    }
}
