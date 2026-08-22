using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; 

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance;//instance audıomanger degişkeni yanı ses

    [Header("Ses Kaynakları (Hoparlörler)")]//dosyalar tek basına muzık calamaz hoparlor ekeldık
    public AudioSource muzikSource;//butonlarla dıger muzıkler bırbırını kesmesın dıye ıksıı aynı anda calmalı
    public AudioSource efektSource;

    [Header("Audio Mixer Grupları")]//ses yukseklıgını ayarladıgım yer ıkı ayar var
    public AudioMixerGroup menuMuzikGrubu; 
    public AudioMixerGroup oyunMuzikGrubu; 

    [Header("Ses Klipleri (Dosyalar)")] //ses dosyaları tum oyun seslerı burda
    public AudioClip arkaPlanMuzigi;
    public AudioClip girisSesi;
    public AudioClip butonTiklama;
    public AudioClip oyunTiklama;
    public AudioClip kaybetmeSesi;
    public AudioClip kazanmaSesi;

    void Awake() // oyun daha start bıle olmadan callısan ksıım ses ayarı
    {
        // Bu kod sayesinde sesler sahneler arası geçişte ASLA kesilmez ve bozulmaz
        if (instance == null) //ortamda hıc audıomanger yanı ses yoksa
        {
            instance = this;//tek ses bu olacak
            DontDestroyOnLoad(gameObject);//sahne gecıslerı ses kesılmesın dıye gecısler duzgun olacak
        }
        else
        {
            Destroy(gameObject);//zaten  ses varsa sahnede fazladan ses olusturma
            return;//ses olusturmadan gec
        }
    }

    void Start()
    {
        // Sahne kontrolü yapıyoruz
        SceneManager.sceneLoaded += OnSceneLoaded;//her yenı sahne yuklendıgınde onsceneloaded fonksıyonu etkılncek
        SesleriSahneyeGoreAyarla(SceneManager.GetActiveScene().name);//o ankı acılan aktıf sahneyı getırr ve uygun olan muzıgı cagırır
        Screen.sleepTimeout = SleepTimeout.NeverSleep;//ekrana dokunmasak bıle oyun sonmesın durup dururken
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;//sahneden cıktıysak bellek kaplamasın dıye sılerız 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)//unıtyden scene bılgısı aldık
    {
        SesleriSahneyeGoreAyarla(scene.name);//koca sahne ıle ugrasmak yerıne sadece ısmını alıyoruz,sadeec ısmıyle ses ayarlanır
    }

    // Seslerin sahneye göre otomatik ayarlandığı ve yönlendirildiği yer
    void SesleriSahneyeGoreAyarla(string sahneAdi)//kazandinsahnesi gibi sadece strıng degıskene bakar ve ıf blogu yapar
    {
        // EĞER OYUN SAHNESİNDEYSEK (Normal Oyun Müziği Çalsın)
        if (sahneAdi == "SampleScene")
        {
            // Oyun müziğini ve efektleri OyunMuzik grubuna bağla eger olmazsa kelıme yenılendıkce vs muzık kesılır yenıden baslar 
            if (muzikSource != null && oyunMuzikGrubu != null) muzikSource.outputAudioMixerGroup = oyunMuzikGrubu;//bos degılse saglamsa muzık grubuna bagla
            if (efektSource != null && oyunMuzikGrubu != null) efektSource.outputAudioMixerGroup = oyunMuzikGrubu;

            if (muzikSource.clip != arkaPlanMuzigi)//eger sampleda muzık baslamadıysa 
            {
                muzikSource.clip = arkaPlanMuzigi;//arka plan muzıgı gelsın sample muzıgı
                muzikSource.loop = true;//bıtınce otomotık baslasın
                muzikSource.Play();//muzık baslasın
            }
        }
        // EĞER KAZANDIN VEYA KAYBETTİN EKRANLARINDAYSAK (sampleMüziğini tamamen durdur, efektler oyun grubundan çalsın)
        else if (sahneAdi == "KazandinSahnesi" || sahneAdi == "KaybettinSahnesi" || sahneAdi == "KupaSahnesi")
        {
            if (efektSource != null && oyunMuzikGrubu != null) efektSource.outputAudioMixerGroup = oyunMuzikGrubu;
            //efekt seslerını oyun grubuna baglı tutmaya devam et
            muzikSource.Stop();//sample muzıgı dur 
            muzikSource.clip = null;//muzık hafızası temızlensın
        }
        // GİRİŞ EKRANI VEYA DİĞER MENÜLERDEYSEK giris sesi calsınn
        else
        {
            // Menü seslerini MenuMuzik grubuna bağlıyoruz, normal muzıkanasayfadaki
            if (muzikSource != null && menuMuzikGrubu != null) muzikSource.outputAudioMixerGroup = menuMuzikGrubu;
            if (efektSource != null && menuMuzikGrubu != null) efektSource.outputAudioMixerGroup = menuMuzikGrubu;
            //null ksıımları sayesınde hoparloru falan baglamasak da ekranı cokertmemelı
            if (muzikSource.clip != girisSesi)
            {
                muzikSource.clip = girisSesi;//mzuıksource cd nın ıcıne gırıssesi dosyasını koy
                muzikSource.loop = true;//bıttıkce  baslasın
                muzikSource.Play();//cal
            }
        }
    }

    //herhagı anlık ses efektı ıcın asagıdakı kodlar 



    public void ButonSesiCal()
    {
        efektSource.PlayOneShot(butonTiklama);
    }
    //playonshot sayesındekazanma sesı ustune mesela butontıklma olabılsın 

    public void OyunTiklamaSesiCal()
    {
        efektSource.PlayOneShot(oyunTiklama);
    }

    public void KaybetmeSesiCal()
    {
        efektSource.PlayOneShot(kaybetmeSesi);
    }

 
    public void KazanmaSesiCal()
    {
        efektSource.PlayOneShot(kazanmaSesi);
    }
}
