using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace OgrenciDersYonetimSistemi
{
    class Program
    {
        static string ogrencilerPath = "ogrenciler.json";
        static string ogretimGorevlileriPath = "ogretimGorevlileri.json";
        static string derslerPath = "dersler.json";

        static List<Ogrenci> ogrenciler = new List<Ogrenci>();
        static List<OgretimGorevlisi> ogretimGorevlileri = new List<OgretimGorevlisi>();
        static List<Ders> dersler = new List<Ders>();

        static void Main(string[] args)
        {
            DosyalariOku();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===============================================");
                Console.WriteLine("   Öğrenci ve Ders Yönetim Sistemi");
                Console.WriteLine("===============================================");
                Console.WriteLine("1. Yeni Öğrenci Ekle");
                Console.WriteLine("2. Yeni Öğretim Görevlisi Ekle");
                Console.WriteLine("3. Yeni Ders Ekle");
                Console.WriteLine("4. Derslere Öğrenci Ekle");
                Console.WriteLine("5. Ders Bilgilerini Listele");
                Console.WriteLine("6. Öğrencileri Listele");
                Console.WriteLine("7. Öğretim Görevlilerini Listele");
                Console.WriteLine("0. Çıkış");
                Console.WriteLine("===============================================");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        OgrenciEkle();
                        break;
                    case "2":
                        OgretimGorevlisiEkle();
                        break;
                    case "3":
                        DersEkle();
                        break;
                    case "4":
                        DerseOgrenciEkle();
                        break;
                    case "5":
                        DersleriListele();
                        break;
                    case "6":
                        OgrencileriListele();
                        break;
                    case "7":
                        OgretimGorevlileriniListele();
                        break;
                    case "0":
                        DosyalariKaydet();
                        Console.WriteLine("Sistemden çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void OgrenciEkle()
        {
            int id = GirisAlInt("Öğrenci ID: ");
            string adSoyad = GirisAlAdSoyad("Öğrenci Ad Soyad: ");
            ogrenciler.Add(new Ogrenci(id, adSoyad));
            Console.WriteLine("\nÖğrenci başarıyla eklendi.");
            DevamEt();
        }

        static void OgretimGorevlisiEkle()
        {
            int id = GirisAlInt("Öğretim Görevlisi ID: ");
            string adSoyad = GirisAlAdSoyad("Öğretim Görevlisi Ad Soyad: ");
            string unvan = GirisAlString("Ünvan: ");
            ogretimGorevlileri.Add(new OgretimGorevlisi(id, adSoyad, unvan));
            Console.WriteLine("\nÖğretim Görevlisi başarıyla eklendi.");
            DevamEt();
        }

        static void DersEkle()
        {
            string ad = GirisAlString("Ders Adı: ");
            int kredi = GirisAlInt("Ders Kredisi: ");

            Console.WriteLine("Mevcut Öğretim Görevlileri:");
            for (int i = 0; i < ogretimGorevlileri.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ogretimGorevlileri[i].AdSoyad}");
            }
            int secim = GirisAlInt("Dersi veren öğretim görevlisinin numarası: ") - 1;

            if (secim >= 0 && secim < ogretimGorevlileri.Count)
            {
                var ogretimGorevlisi = ogretimGorevlileri[secim];
                dersler.Add(new Ders(ad, kredi, ogretimGorevlisi));
                Console.WriteLine("\nDers başarıyla eklendi.");
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            DevamEt();
        }

        static void DerseOgrenciEkle()
        {
            Console.WriteLine("Mevcut Dersler:");
            for (int i = 0; i < dersler.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dersler[i].Ad}");
            }
            int dersSecim = GirisAlInt("Öğrenci eklemek istediğiniz dersin numarası: ") - 1;

            if (dersSecim >= 0 && dersSecim < dersler.Count)
            {
                if (ogrenciler.Count == 0)
                {
                    Console.WriteLine("Önce bir öğrenci ekleyiniz.");
                    DevamEt();
                    return;
                }

                Console.WriteLine("Mevcut Öğrenciler:");
                for (int i = 0; i < ogrenciler.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ogrenciler[i].AdSoyad}");
                }

                int ogrenciSecim = GirisAlInt("Derse eklemek istediğiniz öğrencinin numarası: ") - 1;

                if (ogrenciSecim >= 0 && ogrenciSecim < ogrenciler.Count)
                {
                    dersler[dersSecim].OgrenciEkle(ogrenciler[ogrenciSecim]);
                    Console.WriteLine("\nÖğrenci derse başarıyla eklendi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
            }
            DevamEt();
        }

        static void DersleriListele()
        {
            if (dersler.Count == 0)
            {
                Console.WriteLine("Henüz ders eklenmedi.");
            }
            else
            {
                foreach (var ders in dersler)
                {
                    ders.BilgiGoster();
                    Console.WriteLine();
                }
            }
            DevamEt();
        }

        static void OgrencileriListele()
        {
            if (ogrenciler.Count == 0)
            {
                Console.WriteLine("Henüz öğrenci eklenmedi.");
            }
            else
            {
                foreach (var ogrenci in ogrenciler)
                {
                    ogrenci.BilgiGoster();
                }
            }
            DevamEt();
        }

        static void OgretimGorevlileriniListele()
        {
            if (ogretimGorevlileri.Count == 0)
            {
                Console.WriteLine("Henüz öğretim görevlisi eklenmedi.");
            }
            else
            {
                foreach (var ogretimGorevlisi in ogretimGorevlileri)
                {
                    ogretimGorevlisi.BilgiGoster();
                }
            }
            DevamEt();
        }

        static void DosyalariOku()
        {
            if (File.Exists(ogrencilerPath))
                ogrenciler = JsonSerializer.Deserialize<List<Ogrenci>>(File.ReadAllText(ogrencilerPath));

            if (File.Exists(ogretimGorevlileriPath))
                ogretimGorevlileri = JsonSerializer.Deserialize<List<OgretimGorevlisi>>(File.ReadAllText(ogretimGorevlileriPath));

            if (File.Exists(derslerPath))
                dersler = JsonSerializer.Deserialize<List<Ders>>(File.ReadAllText(derslerPath));
        }

        static void DosyalariKaydet()
        {
            File.WriteAllText(ogrencilerPath, JsonSerializer.Serialize(ogrenciler));
            File.WriteAllText(ogretimGorevlileriPath, JsonSerializer.Serialize(ogretimGorevlileri));
            File.WriteAllText(derslerPath, JsonSerializer.Serialize(dersler));
        }

        static int GirisAlInt(string mesaj)
        {
            int deger;
            while (true)
            {
                Console.Write(mesaj);
                if (int.TryParse(Console.ReadLine(), out deger))
                    return deger;
                else
                    Console.WriteLine("Geçersiz giriş, lütfen tekrar deneyin.");
            }
        }

        static string GirisAlString(string mesaj)
        {
            string deger;
            while (true)
            {
                Console.Write(mesaj);
                deger = Console.ReadLine();
                if (!string.IsNullOrEmpty(deger))
                    return deger;
                else
                    Console.WriteLine("Boş değer girilemez, lütfen tekrar deneyin.");
            }
        }

        static string GirisAlAdSoyad(string mesaj)
        {
            string deger;
            while (true)
            {
                Console.Write(mesaj);
                deger = Console.ReadLine();
                if (deger.All(c => char.IsLetter(c) || c == ' '))
                {
                    return deger;
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş! Ad ve soyad sadece harflerden oluşmalı.");
                }
            }
        }

        static void DevamEt()
        {
            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }

    // JSON serileştirme için sınıflara boş kurucular eklenmeli
    class Ogrenci : Kisi
    {
        public Ogrenci() : base(0, "") { }
        public Ogrenci(int id, string adSoyad) : base(id, adSoyad) { }

        public override void BilgiGoster()
        {
            Console.WriteLine($"Öğrenci ID: {Id}, Ad Soyad: {AdSoyad}");
        }
    }

    class OgretimGorevlisi : Kisi
    {
        public string Unvan { get; set; }
        public OgretimGorevlisi() : base(0, "") { }
        public OgretimGorevlisi(int id, string adSoyad, string unvan) : base(id, adSoyad)
        {
            Unvan = unvan;
        }

        public override void BilgiGoster()
        {
            Console.WriteLine($"Öğretim Görevlisi ID: {Id}, Ad Soyad: {AdSoyad}, Ünvan: {Unvan}");
        }
    }

    class Ders
    {
        public string Ad { get; set; }
        public int Kredi { get; set; }
        public OgretimGorevlisi OgretimGorevlisi { get; set; }
        public List<Ogrenci> Ogrenciler { get; set; }

        public Ders() { Ogrenciler = new List<Ogrenci>(); }
        public Ders(string ad, int kredi, OgretimGorevlisi ogretimGorevlisi)
        {
            Ad = ad;
            Kredi = kredi;
            OgretimGorevlisi = ogretimGorevlisi;
            Ogrenciler = new List<Ogrenci>();
        }

        public void OgrenciEkle(Ogrenci ogrenci)
        {
            Ogrenciler.Add(ogrenci);
        }

        public void BilgiGoster()
        {
            Console.WriteLine($"Ders Adı: {Ad}, Kredi: {Kredi}");

            if (OgretimGorevlisi != null)
                Console.WriteLine($"Dersi Veren: {OgretimGorevlisi.AdSoyad} ({OgretimGorevlisi.Unvan})");

            if (Ogrenciler.Count > 0)
            {
                Console.WriteLine("Kayıtlı Öğrenciler:");
                foreach (var ogrenci in Ogrenciler)
                    ogrenci.BilgiGoster();
            }
            else
            {
                Console.WriteLine("Bu derse kayıtlı öğrenci yok.");
            }
        }
    }

    abstract class Kisi
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }

        protected Kisi(int id, string adSoyad)
        {
            Id = id;
            AdSoyad = adSoyad;
        }

        public abstract void BilgiGoster();
    }
}
