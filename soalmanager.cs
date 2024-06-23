using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soalmanager : MonoBehaviour
{
    public GameObject feed_benar, feed_salah;
    public ScoreManager scoreManager; // Referensi ke skrip ScoreManager
    public GameObject skorPopup; //objek pop-up skor
    public Text skorAkhirText; // Variabel bertipe Text untuk menampilkan skor akhir
    public List<GameObject> soalObjects; // Semua Pertanyaan
    private List<int> soalDipilih; // Pertanyaan yg dipilih
    private List<bool> soalDitampilkan;
    private GameObject currentSoal; // GameObject yang sedang ditampilkan sebagai soal saat ini
    private int jumlahPertanyaanDijawab = 0; // Deklarasi variabel untuk melacak jumlah pertanyaan yang sudah dijawab
    private int skorAkhir = 0; // Variabel untuk menyimpan skor akhir

    // Jmlh pertanyaan yg ditampilkan
    public int jumlahSoalUntukDitampilkan = 10;

    void Start()
    {
        InitializeQuiz();
    }
    
    private void InitializeQuiz()
    {
        soalDitampilkan = new List<bool>();
        soalDipilih = new List<int>();
        PlayerPrefs.SetInt("skor", 0);

        // Inisialisasi soalditampilkan dengan nilai false
        for (int i = 0; i < soalObjects.Count; i++)
        {
            soalDitampilkan.Add(false);
        }

        // pilih pertanyaan acak
        SelectRandomQuestions();
        GantiSoal();
    }

    // pilih pertanyaan acak dari bank soal
    void SelectRandomQuestions()
    {
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < soalObjects.Count; i++)
        {
            availableIndexes.Add(i);
        }

        for (int i = 0; i < jumlahSoalUntukDitampilkan; i++)
        {
            if (availableIndexes.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableIndexes.Count);
            soalDipilih.Add(availableIndexes[randomIndex]);
            availableIndexes.RemoveAt(randomIndex);
        }
    }

    public void jawaban(bool jawab)
    {
        jumlahPertanyaanDijawab++;
        Debug.Log("Method jawaban() dipanggil.");
        Debug.Log("Jawaban: " + jawab);

        if (jawab)
        {
            feed_benar.SetActive(true);
            feed_salah.SetActive(false);
            scoreManager.TambahSkor(10);
        }
        else
        {
            feed_benar.SetActive(false);
            feed_salah.SetActive(true);
        }

        // int indeksSoal = soalDipilih.IndexOf(soalObjects.IndexOf(currentSoal));
        // soalDitampilkan[indeksSoal] = true;
        soalDitampilkan[soalObjects.IndexOf(currentSoal)] = true;

        if (jumlahPertanyaanDijawab >= soalDipilih.Count)
        {
            skorAkhir = scoreManager.GetSkor();
            TampilkanSkorAkhir();
        }
        else
        {
            StartCoroutine(TungguDanGantiSoal(3f)); //menunggu 3 detik 
        }
    }

    private IEnumerator TungguDanGantiSoal(float delay)
    {
        yield return new WaitForSeconds(delay);
        GantiSoal();
    }

    public void GantiSoal()
    {
        List<int> indeksPertanyaanBelumDijawab = new List<int>();
        foreach (int index in soalDipilih)
        {
            if (!soalDitampilkan[index])
            {
                indeksPertanyaanBelumDijawab.Add(index);
            }
        }

        if (indeksPertanyaanBelumDijawab.Count > 0)
        {
            int indexSoal = Random.Range(0, indeksPertanyaanBelumDijawab.Count);
            int indeksSoalBaru = indeksPertanyaanBelumDijawab[indexSoal];

            if (currentSoal != null)
            {
                currentSoal.SetActive(false);
            }
            currentSoal = soalObjects[indeksSoalBaru];
            currentSoal.SetActive(true);

            feed_benar.SetActive(false);
            feed_salah.SetActive(false);
        }
        else
        {
            Debug.Log("Semua pertanyaan sudah dijawab.");
        }
    }

    void TampilkanSkorAkhir()
    {
        skorAkhirText.text = "Skor Akhir: " + skorAkhir.ToString();
        skorPopup.SetActive(true);
    }

    public void RestartQuiz()
    {
        jumlahPertanyaanDijawab = 0;
        skorAkhir = 0;
        skorPopup.SetActive(false);

        // offkan soal 
        if (currentSoal != null)
        {
            currentSoal.SetActive(false);
        }

        // Reinitialize kuiz
        InitializeQuiz();
    }

    // Update 
    void Update()
    {

    }
}
