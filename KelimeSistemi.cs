using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class KelimeSistemi : MonoBehaviour
{
    [Header("Görsel Bileşenler")]//ınspector kısmının baslıkları atadıgımız kısım on click ıle baglayabılmek ıcın
    public TMP_Text ekrandakiSoruMetni;
    public KarakterKontrol karakterKodu;

    [Header("4 Şık Butonunun Textleri")]
    public TMP_Text[] sikMetinleri;

    [Header("Dil Ayarı (0=İng, 1=Arap, 2=Alm)")]
    public int secilenDil = 0;//ılk secılen dıl 0 yanı ıngılızce ayarlandı 

    [Header("Mevcut Seviye")]
    public int aktifLevel = 1;//ılk level ayarlandı 1

    private int bilinenKelimeSayisi = 0;//baslangıcta bılınen sayısı ve kelıme ındexım 0
    private int kelimeIndeksi = 0;

    private string ekranaGelecekYabanciKelime;
    private string dogruTurkceCevap;

    // 7 LEVELLİK dil anlamlarııı
    private string[] ingilizceL1 = { "eat", "drink", "walk", "run", "write", "stand up", "go to bed", "wake up", "come", "go" };
    private string[] arapcaL1 = { "yakul", "yasrab", "yamsi", "yarkud", "yaktub", "yakum", "yanam", "yastaykiz", "yati", "yadhab" };
    private string[] almancaL1 = { "essen", "trinken", "gehen", "laufen", "schreiben", "aufstehen", "schlafen gehen", "aufwachen", "kommen", "gehen" };
    private string[] turkceL1 = { "yemek", "içmek", "yürümek", "koşmak", "yazmak", "kalkmak", "yatmak", "uyanmak", "gelmek", "gitmek" };

    private string[] ingilizceL2 = { "apple", "banana", "orange", "strawberry", "grape", "potato", "tomato", "carrot", "onion", "cucumber" };
    private string[] arapcaL2 = { "tuffah", "mawz", "burtuqal", "farawila", "inab", "batatis", "tamatim", "jazar", "basal", "khiyar" };
    private string[] almancaL2 = { "Apfel", "Banane", "Orange", "Erdbeere", "Traube", "Kartoffel", "Tomate", "Karotte", "Zwiebel", "Gurke" };
    private string[] turkceL2 = { "elma", "muz", "portakal", "çilek", "üzüm", "patates", "domates", "havuç", "soğan", "salatalık" };

    private string[] ingilizceL3 = { "red", "blue", "green", "yellow", "white", "black", "sun", "moon", "star", "tree" };
    private string[] arapcaL3 = { "ahmar", "azraq", "akhdar", "asfar", "abyad", "aswad", "shams", "qamar", "najma", "shajara" };
    private string[] almancaL3 = { "rot", "blau", "grün", "gelb", "weiß", "schwarz", "Sonne", "Mond", "Stern", "Baum" };
    private string[] turkceL3 = { "kırmızı", "mavi", "yeşil", "sarı", "beyaz", "siyah", "güneş", "ay", "yıldız", "ağaç" };

    private string[] ingilizceL4 = { "strong", "clean", "ugly", "weak", "fat", "quiet", "friendly", "outgoing", "happy", "brave" };
    private string[] arapcaL4 = { "qawiyy", "nazif", "qabih", "daif", "samin", "hadid", "wadud", "ijtimai", "said", "shuja" };
    private string[] almancaL4 = { "stark", "sauber", "hässlich", "schwach", "dick", "ruhig", "freundlich", "kontaktfreudig", "glücklich", "mutig" };
    private string[] turkceL4 = { "güçlü", "temiz", "çirkin", "zayıf", "şişman", "sessiz", "arkadaş canlısı", "dışa dönük", "mutlu", "cesur" };

    private string[] ingilizceL5 = { "I went", "you can do it", "he did it", "we are reading", "they are playing", "I love you", "she is sleeping", "birds fly", "fish swim", "time passes" };
    private string[] arapcaL5 = { "dahabtu", "tastatiu fialah", "faalahu", "nahnu naqrau", "hum yalabun", "uhibbuka", "hiya tanamu", "al-tuyur tatir", "al-asmak tasbah", "al-waqt yamurr" };
    private string[] almancaL5 = { "Ich ging", "du kannst es schaffen", "er hat es getan", "wir lesen", "sie spielen", "Ich liebe dich", "sie schläft", "Vögel fliegen", "Fische schwimmen", "Die Zeit vergeht" };
    private string[] turkceL5 = { "ben gittim", "sen yapabilirsin", "o yaptı", "biz okuyoruz", "onlar oynuyorlar", "seni seviyorum", "o uyuyor", "kuşlar uçar", "balıklar yüzer", "zaman geçer" };

    private string[] ingilizceL6 = { "Will you come with me today?", "Where are you going?", "What is your name?", "How are you?", "Is this yours?", "Can you help me?", "What time is it?", "Do you love music?", "Where is the cat?", "Are you ready?" };
    private string[] arapcaL6 = { "Hal tati mai al-yawm?", "Ila ayna tadhab?", "Ma ismuka?", "Kayfa haluka?", "Hal hadha laka?", "Hal tustatiu musaadati?", "Kam al-saa?", "Hal tuhibb al-musiqa?", "Ayna al-qitta?", "Hal anta mustaid?" };
    private string[] almancaL6 = { "Kommst du heute mit mir?", "Wohin gehst du?", "Wie heißt du?", "Wie geht es dir?", "Ist das deins?", "Kannst du mir helfen?", "Wie spät ist es?", "Liebst du Musik?", "Wo ist die Katze?", "Bist du bereit?" };
    private string[] turkceL6 = { "sen bugün benimle gelir misin?", "nereye gidiyorsun?", "adın ne?", "nasılsın?", "bu senin mi?", "bana yardım edebilir misin?", "saat kaç?", "müziği sever misin?", "kedi nerede?", "hazır mısın?" };

    private string[] ingilizceL7 = { "Knowledge is power", "Never give up on your dreams", "Practice makes perfect", "Honesty is the best policy", "Actions speak louder than words", "Don't judge a book by its cover", "Better late than never", "Every cloud has a silver lining", "Where there is a will, there is a way", "Good things take time" };
    private string[] arapcaL7 = { "al-ilmu quwwa", "la tastaslim an ahlamika abadan", "al-tadriibu yualilmu al-itqan", "al-sidqu huwa afdalu siyasa", "al-af'alu ablaghu min al-aqwal", "la tahkum ala al-kitab min ghilafihi", "an tati muta'akhiran khayrun min alla tati", "ma'a al-usri yusra", "aynama tujuad al-irada yujad al-tariq", "al-ashya'u al-jayyida ta'khudh waqtan" };
    private string[] almancaL7 = { "Wissen ist Macht", "Gib deine Träume niemals auf", "Übung macht den Meister", "Ehrlichkeit is die beste Politik", "Taten sagen mehr als Worte", "Beurteile ein Buch nicht nach seinem Einband", "Besser spät als nie", "Auf Regen folgt Sonnenschein", "Wo ein Wille ist, is auch ein Weg", "Gute Dinge brauchen Zeit" };
    private string[] turkceL7 = { "bilgi güçtür", "hayallerinden asla vazgeçme", "pratik yapmak mükemmelleştirir", "dürüstlik en iyi politikadır", "eylemler kelimelerden daha etkilidir", "kitabı kapağına göre yargılama", "geç olması hiç olmamasından iyidir", "her şerde hayır vardır", "isteğin olduğu yerde yol vardır", "güzel şeyler zaman alır" };

    void Start()
    {
        secilenDil = PlayerPrefs.GetInt("SecilenDilHafiza", 0);//ılk kayıtlar , secılen dıl 0 ve leevl1 hafızaya aktardık playerprefs ıle
        aktifLevel = PlayerPrefs.GetInt("MevcutLevelHafiza", 1);

        if (aktifLevel < 1 || aktifLevel > 7) aktifLevel = 1;//level 1 den kucuk ve 7 den buyukse level 1 olmallı yanı ılk gırınce ve oyun bıtınce otomotık 1.levele gıdılsın

        if (ekrandakiSoruMetni == null || sikMetinleri == null || sikMetinleri.Length < 4) return;//soru ve şıklar yok ve sıklarım 4 den kucukse kelıme yenıle

        KelimeYenile();//alttakı fonks
    }

    public void AnaSayfayaGit()
    {
        SceneManager.LoadScene("GirisEkrani");
    }

    void KelimeYenile()//strıng cınsınden yabancı ve turkce dıl alıyorum ılk basta yabancı 0 yanı ıngılızce aldım l1 1.sevıye aldım 
    {
        string[] secilenYabanciHavuz = ingilizceL1;
        string[] secilenTurkceHavuz = turkceL1;
        //l1 1.level , l2 2.level onları ayarlıyoruz kelımeyenılede secılen dıle gore ıng arapca ve almanca unutmuyoruz

        if (aktifLevel == 1) { secilenTurkceHavuz = turkceL1; if (secilenDil == 1) secilenYabanciHavuz = arapcaL1; else if (secilenDil == 2) secilenYabanciHavuz = almancaL1; else secilenYabanciHavuz = ingilizceL1; }
        else if (aktifLevel == 2) { secilenTurkceHavuz = turkceL2; if (secilenDil == 1) secilenYabanciHavuz = arapcaL2; else if (secilenDil == 2) secilenYabanciHavuz = almancaL2; else secilenYabanciHavuz = ingilizceL2; }
        else if (aktifLevel == 3) { secilenTurkceHavuz = turkceL3; if (secilenDil == 1) secilenYabanciHavuz = arapcaL3; else if (secilenDil == 2) secilenYabanciHavuz = almancaL3; else secilenYabanciHavuz = ingilizceL3; }
        else if (aktifLevel == 4) { secilenTurkceHavuz = turkceL4; if (secilenDil == 1) secilenYabanciHavuz = arapcaL4; else if (secilenDil == 2) secilenYabanciHavuz = almancaL4; else secilenYabanciHavuz = ingilizceL4; }
        else if (aktifLevel == 5) { secilenTurkceHavuz = turkceL5; if (secilenDil == 1) secilenYabanciHavuz = arapcaL5; else if (secilenDil == 2) secilenYabanciHavuz = almancaL5; else secilenYabanciHavuz = ingilizceL5; }
        else if (aktifLevel == 6) { secilenTurkceHavuz = turkceL6; if (secilenDil == 1) secilenYabanciHavuz = arapcaL6; else if (secilenDil == 2) secilenYabanciHavuz = almancaL6; else secilenYabanciHavuz = ingilizceL6; }
        else if (aktifLevel == 7) { secilenTurkceHavuz = turkceL7; if (secilenDil == 1) secilenYabanciHavuz = arapcaL7; else if (secilenDil == 2) secilenYabanciHavuz = almancaL7; else secilenYabanciHavuz = ingilizceL7; }

        if (kelimeIndeksi >= secilenTurkceHavuz.Length) kelimeIndeksi = 0;//kelıme sayım turkcekelıme uzunluguna esıtse buyukse 0 a esıtlıyorum

        ekranaGelecekYabanciKelime = secilenYabanciHavuz[kelimeIndeksi];//yabancıkelıme yabancıhavuzdan dogrucevap turkce havuzun o ankı ındexı oluyor
        dogruTurkceCevap = secilenTurkceHavuz[kelimeIndeksi];

        if (ekrandakiSoruMetni != null) ekrandakiSoruMetni.text = ekranaGelecekYabanciKelime;//eger bos degılse soru metnı kontrol ettık, sorumetnı yabancıkelıme oluyor

        SiklariOlustur(secilenTurkceHavuz);//ve fonksıyon alttakı 
    }

    void SiklariOlustur(string[] guncelTurkceHavuz) //yanlıs ve dogru sıkların ayarlandıgı bolum
    {
        List<string> yanlisSikHavuzu = new List<string>(guncelTurkceHavuz);
        yanlisSikHavuzu.Remove(dogruTurkceCevap);//lıste olusturuyoruz turkcehavuzdsan ve bu yanlıs lıstden dogru cevabı sılıyoruz

        int dogruSikIndeksi = Random.Range(0, 4);//4 sık arasında rasgele bır yere gelmesı ıcın dogru sıkkın yanı sagdan 3. gıbı 

        for (int i = 0; i < 4; i++)
        {
            if (i == dogruSikIndeksi)//dongude i dogru sık sırası ıse o ındexe dogru cevap yazılır soru hazırlanırken
            {
                sikMetinleri[i].text = dogruTurkceCevap;//dogru cevap elma ıse dogru sık da 2. sıra ıse 2.sıraya elma yazar 
            }
            else
            {
                if (yanlisSikHavuzu.Count > 0)
                {
                    int rst = Random.Range(0, yanlisSikHavuzu.Count);//yanlıs havuzdan rasgele sayı ınt ındexsecerr
                    sikMetinleri[i].text = yanlisSikHavuzu[rst];//secılen rst ındexın oldugu kelımeyı sıradakı o ankı butonun uzerıne yazıyor
                    yanlisSikHavuzu.RemoveAt(rst);//yazıldıktan sonra sılınıyor yanlıshavuzundan kı 4 sıkdan 2 sı aynı olmasın yanlıs olarak
                }
            }
        }
    }

    public void SikSecildi(int secilenButonNo)
    {
        if (AudioManger.instance != null) AudioManger.instance.OyunTiklamaSesiCal();//sık secınce buton tık  sesı calsın

        if (sikMetinleri[secilenButonNo].text == dogruTurkceCevap)//eger dogruysa bılınen kelıme sayısı artsın ve kelıme ındexı artsın
        {
            bilinenKelimeSayisi++;
            kelimeIndeksi++;

            if (karakterKodu != null) karakterKodu.HizlanVeKos();//bıldıgı ıcın hızlankos fonksıyon cagır yapıyoruz 

            if (kelimeIndeksi >= 10)//kelıme bıtınce 10u gecınce yanı kazanmasesı calar bu bır dosya audıomanger kasetımın ıcındekı
            {
                if (AudioManger.instance != null) AudioManger.instance.KazanmaSesiCal();

                // o ankı levelım 7 olunca kazandın der kupa sahnesı
                if (aktifLevel == 7)
                {
                    // Oyun bitti, seviyeyi hafızada 7'de sabit bırakıyoruz ki kilitlenmesin 8.level yok cunku
                    PlayerPrefs.SetInt("MevcutLevelHafiza", 7);
                    PlayerPrefs.SetInt("OyunTamamenBittiMi", 1); // Akıllı kontrol kilidi
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("KazandinSahnesi"); 
                }
                else//son kevel degılse levelı 1 artırdık mevcut level sonrakılevel olur kaydet kazandın sahnesı yıne
                {
                    int sonrakiLevel = aktifLevel + 1;
                    PlayerPrefs.SetInt("MevcutLevelHafiza", sonrakiLevel);
                    PlayerPrefs.SetInt("OyunTamamenBittiMi", 0);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("KazandinSahnesi");
                }
            }
            else//kelımem bıtmedıyse kelıme yenıle fonks calısır
            {
                KelimeYenile();
            }
        }
        else//yanlıs sectıyse kaybettın sahnesı ve sesı 
        {
            if (AudioManger.instance != null) AudioManger.instance.KaybetmeSesiCal();

            SceneManager.LoadScene("KaybettinSahnesi");
        }
    }
}
