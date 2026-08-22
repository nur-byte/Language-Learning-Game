using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio; 

public class SahneButonlari : MonoBehaviour
{ //ınspector sekıllendırme yerı
    [Header("Audio Mixer Bağlantısı")]//ınspectordakı duzenlı baslık ve yazılar
    public AudioMixer anaMixer;//public ıle gorunur hale gelır bos slot acılır ınspectorda

    [Header("Ayarlar Menüsü Slider Bileşenleri")]
    public Slider menuMuzikSlider;
    public Slider oyunMuzikSlider;

    [Header("Oyun Tamamen Bitince Açılacak Kupa Resmi (Final.png)")]
    public GameObject kupaGorseliNesnesi;

    [Header("Değişecek Butonun Text Bileşeni")]
    public TMP_Text sonrakiLevelButonMetni;

    [Header("Oyun Bitince Kapatılacak Eski Arka Plan (Sarı Kedili Resim)")]
    public GameObject normalKazandinArkaPlani;

    [Header("Oyun Bitince Kapatılacak Diğer Butonlar")]
    public GameObject anasayfaButonu;
    public GameObject ayarlarButonu;
    public GameObject retryButonu;

    void Start()
    {
        // Eğer ayarlar sahnesindeysek Slider'ların konumunu eski ayarlara göre ayarla

        if (menuMuzikSlider != null)
        {//hıc ayarlanmadıysa sevıye 0.75f te kalsın her ıkı ses ıcın de aynı oyun ve menu
            float kaydedilenMenuSes = PlayerPrefs.GetFloat("MenuSesHafiza", 0.75f);
            menuMuzikSlider.value = kaydedilenMenuSes;
            SetMenuMuzikSes(kaydedilenMenuSes);
        }

        if (oyunMuzikSlider != null)
        {
            float kaydedilenOyunSes = PlayerPrefs.GetFloat("OyunSesHafiza", 0.75f);
            oyunMuzikSlider.value = kaydedilenOyunSes;
            SetOyunMuzikSes(kaydedilenOyunSes); //bu ve oncekı fonksıyorunlar sesı o sevıyeye ayarlamak ıcın
        }




        // sahne kontrolu
        if (SceneManager.GetActiveScene().name == "KazandinSahnesi")
        {
            int bitmeDurumu = PlayerPrefs.GetInt("OyunTamamenBittiMi", 0);

            if (bitmeDurumu == 1)
            {
                if (normalKazandinArkaPlani != null) normalKazandinArkaPlani.SetActive(false);
                if (anasayfaButonu != null) anasayfaButonu.SetActive(false);
                if (ayarlarButonu != null) ayarlarButonu.SetActive(false);
                if (retryButonu != null) retryButonu.SetActive(false);
                if (kupaGorseliNesnesi != null) kupaGorseliNesnesi.SetActive(true);
                if (sonrakiLevelButonMetni != null) sonrakiLevelButonMetni.text = "Ana Sayfaya Dön";
            }
        }
    }

    // sliderların ayarı bu kısım matematıksel 
    
    public void SetMenuMuzikSes(float sliderDegeri)
    {
        if (sliderDegeri > 0) //degıstırmek ıstenılen surgunun dugmenın unıtyde verılen ısımlerı menuparam..
        {
            anaMixer.SetFloat("MenuSesParam", Mathf.Log10(sliderDegeri) * 20);
        }
        else//ses ayarları ondalıklı float 
        {
            anaMixer.SetFloat("MenuSesParam", -80f); // Tamamen sessiz
        }
        PlayerPrefs.SetFloat("MenuSesHafiza", sliderDegeri); // Ayarı hafızaya kaydet
    }

    public void SetOyunMuzikSes(float sliderDegeri)
    {
        if (sliderDegeri > 0)
        {
            anaMixer.SetFloat("OyunSesParam", Mathf.Log10(sliderDegeri) * 20);
        }
        else
        {
            anaMixer.SetFloat("OyunSesParam", -80f); // Tamamen sessiz
        }
        PlayerPrefs.SetFloat("OyunSesHafiza", sliderDegeri); // Ayarı hafızaya kaydet
    }


    // butonlar sahneelrı acıyorrr
    public void ButonAnaSayfa()
    {//ses doluysa buton sesı cal butona bastıkve gırıs ekranı ac
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("GirisEkrani");
    }

    public void ButonAyarlarMenu() //btuon sesı cal ve menu ac
    {
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("menu");
    }

    public void ButonTekrarDene()
    {
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        SceneManager.LoadScene("SampleScene");//retry butonu yenıden aynı sample scebe yuklenır
    }

    public void ButonSonrakiLevel()
    {
        if (AudioManger.instance != null) AudioManger.instance.ButonSesiCal();
        //hafızadan getınt ıle tam sayı (level sayısı)getırıyoruz,eger bıtmedıyse 0 bıttıyse 1 
        int bitmeDurumu = PlayerPrefs.GetInt("OyunTamamenBittiMi", 0);

        if (bitmeDurumu == 1)//son level bıttıyse 
        {
            PlayerPrefs.SetInt("OyunTamamenBittiMi", 0);//oyun 0dan baslayacak artık
            PlayerPrefs.Save();//ılk levele kaydeder 
            SceneManager.LoadScene("GirisEkrani");//kupa ekrannından sonra gırıse gıtsın
        }
        else
        {
            SceneManager.LoadScene("SampleScene");//son level degılse normal samplescene ekranına yonlendrır
        }
    }
}
