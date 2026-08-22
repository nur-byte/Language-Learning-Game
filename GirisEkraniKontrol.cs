using UnityEngine;
using UnityEngine.SceneManagement;

public class GirisEkraniKontrol : MonoBehaviour
{
    // Oyna butonu için
    public void OyunuBaslat()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Leveller butonu için
    public void LevellerSayfasinaGit()
    { 
        SceneManager.LoadScene("leveller");
    }
}
