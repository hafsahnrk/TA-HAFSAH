using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int skor = 0; // Variabel untuk menyimpan skor
    public Text scoreText; // Referensi ke objek teks untuk menampilkan skor

    public int GetSkor()
    {
        return skor;
    }

    
    void Start()
    {
        // Jika Anda ingin memulai permainan dengan skor awal yang sudah ditetapkan, Anda bisa mengambil skor dari PlayerPrefs di sini.
        // Misalnya:
        skor = PlayerPrefs.GetInt("skor", 0);
       
        // UpdateScoreText();
    }

    // Fungsi untuk menambah skor
    public void TambahSkor(int nilaiTambah)
    {
        skor += nilaiTambah; // Menambah skor dengan nilaiTambah yang diberikan
        UpdateScoreText(); // Memperbarui tampilan skor
    }


    // Fungsi untuk memperbarui tampilan teks skor
    void UpdateScoreText()
    {
        scoreText.text = skor.ToString();
    }
}
