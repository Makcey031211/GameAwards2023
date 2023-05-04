using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography; // AESを使うために追加
using System.IO;
using System.Text;

public static class AesExample
{
    // 初期化ベクトル"<半角16文字[1byte=8bit, 8bit*16=128bit]>"
    private const string AES_IV_256 = @"mER5Ve6jZ/F8CY%~";
    // 暗号化鍵<半角32文字[8bit*32文字=256bit]>
    private const string AES_KEY_256 = @"kxvuA&k|WDRkzgG47yAsuhwFzkQZMNf3";

    // 暗号化のための関数
    // 引数は暗号化したいデータ(string)
    public static string EncryptStringToBytes_Aes(string plainText)
    {
        byte[] encrypted;

        using(Aes aesAlg = Aes.Create())
        {
            // AESの設定
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            
            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV_256);
            aesAlg.Key = Encoding.UTF8.GetBytes(AES_KEY_256);

            // ストリーム変換を実行するための暗号化機能を作成
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // 暗号化に使用するストリームを作成
            using(MemoryStream msEncrypt = new MemoryStream())
            {
                using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using(StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // すべてのデータをストリームに書き込み
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // メモリ ストリームから暗号化されたバイトを返却(byte[] → stringに変換)
        return System.Convert.ToBase64String(encrypted);
    }


    // 復号のための関数
    // 引数は暗号化されたデータ(string)
    public static string DecryptStringFromBytes_Aes(string cipherText)
    {
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            // AESの設定(暗号と同じ)
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV_256);
            aesAlg.Key = Encoding.UTF8.GetBytes(AES_KEY_256);

            // ストリーム変換を実行するデクリプターを作成
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // 復号化に使用するストリームを作成
            using(MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using(StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // 復号化ストリームから復号化されたバイトの読み取り、文字列に配置
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
