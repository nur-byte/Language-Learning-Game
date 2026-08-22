using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    public float yurumeHizi = 3f;
    public float kosmaHizi = 7f;
    private float mevcutHiz;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();//karakterıın hareketı 
        mevcutHiz = yurumeHizi;//basta yurumehıznda 
    }

    void Update()//transform translate sayesınde belırlı yone hareket ettırme saglanır
    {//yurumeye baslar 
        transform.Translate(Vector3.forward * mevcutHiz * Time.deltaTime);
    }//vektor3....ılerıye hareket saglar update fonksıyonu sayesınde her turlu aracta aynı hızda gorubyt karakert

    public void HizlanVeKos()//hızlanır 
    {
        mevcutHiz = kosmaHizi;

        if (anim != null)//kontrol
        {
            anim.SetBool("kosuyorMu", true);
        }

        StartCoroutine(YurumeyeGeriDonSureci());//orn 2sn dur sunu yap gıbı rutınler ıcınstartcorutıne
    }

    private System.Collections.IEnumerator YurumeyeGeriDonSureci()
    {
        yield return new WaitForSeconds(2f);//2sn kronometre tutar 
        mevcutHiz = yurumeHizi;//mevcut hızı yurume yapar

        if (anim != null)
        {
            anim.SetBool("kosuyorMu", false);//yuruyor 2snlik donguler corutıne sayesınde 
        }
    }

}
