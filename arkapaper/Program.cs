using System;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll")]
    public static extern bool FreeConsole();
    // Windows API fonksiyonlarını içe aktarın
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    // Windows masaüstü arka planını değiştirmek için kullanılan sabitler
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;
    static void Main()
    {
        FreeConsole();
        while (true)
        {
            // Arka plan resminin dosya yolu kayıtlıdır
            string wallpaperPath = GetWallpaperPath();

            // Dosyanın adını almak için Path sınıfını kullanabilirsiniz
            string wallpaperFileName = Path.GetFileName(wallpaperPath);

            if (wallpaperFileName != "Ataturk.jpg")
            {
                string yeniArkaPlanYolu = "C:\\Arka Plan\\Ataturk.jpg";

                // Arka planı değiştirme işlemi
                int sonuc = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, yeniArkaPlanYolu, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            }
            System.Threading.Thread.Sleep(300000);

        }
    }

    // Masaüstü arka plan resminin dosya yolunu almak için bu fonksiyonu kullanabilirsiniz
    static string GetWallpaperPath()
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", false);
        string wallpaperPath = key.GetValue("Wallpaper").ToString();
        key.Close();

        return wallpaperPath;
    }
}