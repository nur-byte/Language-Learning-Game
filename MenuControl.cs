using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuKontrol : MonoBehaviour
{
    public TMP_Dropdown dilDropdown;

    void Start()
    {
        if (dilDropdown != null)
        {//eger hıc dıl secılmedıyse otomotık 0.dıl ıngılızce secılsın
            int kaydedilenDil = PlayerPrefs.GetInt("SecilenDilHafiza", 0);
            dilDropdown.value = kaydedilenDil;//hafızada secılen dıl ekranda da secılsın
        }
    }

    public void OyunaBasla()
    {
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("SampleScene");
    }

    public void DilSeciminiKaydet()
    {
        if (dilDropdown != null)
        {//playrprefs kayıt ıcındır 0 1 2 numaralı dıllerı sectırır vesecılendılhafızaya kaydeder
            PlayerPrefs.SetInt("SecilenDilHafiza", dilDropdown.value);
            PlayerPrefs.Save();//kapansa bıle kayıtlı kalsın
            Debug.Log("Dil Seçimi Başarıyla Kaydedildi: " + dilDropdown.value);
        }
    }

    public void HakkindaGit()//audıomanger ses oynatma yetkısı olan yer 
    {//audıo manger doluysa buton ssı gelsın
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("Hakkinda");
    }

    public void AyarlarGit()
    {
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("menu");
    }

    public void MenuyeDon()
    {
      
        if (AudioManger.instance != null)
        {
            AudioManger.instance.ButonSesiCal();
        }
        DilSeciminiKaydet();//dıl kaydedılır
        SceneManager.LoadScene("GirisEkrani"); //Giriş ekranına döner
    }
}
