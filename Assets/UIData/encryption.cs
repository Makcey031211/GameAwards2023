using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography; // AES���g�����߂ɒǉ�
using System.IO;
using System.Text;

public static class AesExample
{
    // �������x�N�g��"<���p16����[1byte=8bit, 8bit*16=128bit]>"
    private const string AES_IV_256 = @"mER5Ve6jZ/F8CY%~";
    // �Í�����<���p32����[8bit*32����=256bit]>
    private const string AES_KEY_256 = @"kxvuA&k|WDRkzgG47yAsuhwFzkQZMNf3";

    // �Í����̂��߂̊֐�
    // �����͈Í����������f�[�^(string)
    public static string EncryptStringToBytes_Aes(string plainText)
    {
        byte[] encrypted;

        using(Aes aesAlg = Aes.Create())
        {
            // AES�̐ݒ�
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            
            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV_256);
            aesAlg.Key = Encoding.UTF8.GetBytes(AES_KEY_256);

            // �X�g���[���ϊ������s���邽�߂̈Í����@�\���쐬
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // �Í����Ɏg�p����X�g���[�����쐬
            using(MemoryStream msEncrypt = new MemoryStream())
            {
                using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using(StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // ���ׂẴf�[�^���X�g���[���ɏ�������
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // ������ �X�g���[������Í������ꂽ�o�C�g��ԋp(byte[] �� string�ɕϊ�)
        return System.Convert.ToBase64String(encrypted);
    }


    // �����̂��߂̊֐�
    // �����͈Í������ꂽ�f�[�^(string)
    public static string DecryptStringFromBytes_Aes(string cipherText)
    {
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            // AES�̐ݒ�(�Í��Ɠ���)
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            aesAlg.IV = Encoding.UTF8.GetBytes(AES_IV_256);
            aesAlg.Key = Encoding.UTF8.GetBytes(AES_KEY_256);

            // �X�g���[���ϊ������s����f�N���v�^�[���쐬
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // �������Ɏg�p����X�g���[�����쐬
            using(MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using(StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // �������X�g���[�����畜�������ꂽ�o�C�g�̓ǂݎ��A������ɔz�u
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
