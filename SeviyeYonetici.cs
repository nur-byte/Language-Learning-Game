using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeviyeYonetici : MonoBehaviour
{
    [Header("Level Butonları (Sırasıyla L1, L2, L3... L7 koyulmalı)")]
    public Button[] levelButonlari;

    void Start()
    {
        // Hafızadan oyuncunun ulaştığı maksimum seviyeyi çekiyoruz. 
        // Eğer oyuna hiç başlamadıysa varsayılan olarak 1. seviye açıktır.
        int ulasilanLevel = PlayerPrefs.GetInt("MevcutLevelHafiza", 1);

        Debug.Log("Leveller Ekranı Açıldı. Oyuncunun Hafızadaki Seviyesi: " + ulasilanLevel);

        // Bütün butonları sırayla kontrol ediyoruz
        for (int i = 0; i < levelButonlari.Length; i++)
        {
            int butonunLevelNo = i + 1;//level1 den baslar

            if (butonunLevelNo <= ulasilanLevel)
            {
                // Eğer oyuncunun seviyesi bu butona yetiyorsa butonu AÇIK yap
                levelButonlari[i].interactable = true;
            }
            else
            {
                // Eğer oyuncu henüz bu seviyeye gelemediyse butonu KİLİTLİ yap
                levelButonlari[i].interactable = false;//kılıtlı 
            }
        }
    }

    // level butonlarına basılınca çalışacak olan sahneyegitmefonksiyonu
    public void LevelSec(int secilenLevelNo)
    {
        // Oyuncunun seçtiği levelı hafızaya yazıyoruz ki SampleScene hangi kelimeleri getireceğini bilsin
        PlayerPrefs.SetInt("MevcutLevelHafiza", secilenLevelNo);
        PlayerPrefs.Save();//oyunu kapastak da levl kaydedılsın

        // Kelime oyunumuzun döndüğü ana sahneye (SampleScene) ışınlıyoruz
        SceneManager.LoadScene("SampleScene");
    }

    // Giriş ekranına doner fonksyoon
    public void GeriDon()
    {
        SceneManager.LoadScene("GirisEkrani");//bu fonksıyonlar tek tek eklenır butonların on clicke
    }
}
