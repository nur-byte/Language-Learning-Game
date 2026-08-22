using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedefKarakter; // Takip edilecek karakter (Ch14_nonPBR)
    public Vector3 mesafeOfseti = new Vector3(0f, 4f, -8f); // Kameranın karakterden ne kadar uzakta duracağı

    void LateUpdate()//tıtresım olmasın dıye lateupdate update yerıne
    {
        if (hedefKarakter != null)
        {
            // Kameranın pozisyonunu karakterin gittiği yere göre günceller
            transform.position = hedefKarakter.position + mesafeOfseti;
        }
    }
}
