using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography; // AES���g�����߂ɒǉ�
using System.IO;
using System.Text;

public static class AesExample
{
    // �������x�N�g��"<���p16����[1byte=8bit, 8bit*16=128bit]>"
    //private static string[] AES_IV_256 = new string[50];  // ���񃉃��_���ɂ���ꍇ
    private static Dictionary<int, string> AES_IV_256 = new Dictionary<int, string>();
    // �Í�����<���p32����[8bit*32����=256bit]>
    private const string AES_KEY_256 = @"kxvuA&k|WDRkzgG47yAsuhwFzkQZMNf3";

    // �Í����̂��߂̊֐�
    // �����͈Í����������f�[�^(string), �X�e�[�W�ԍ�(int)
    public static string EncryptStringToBytes_Aes(string plainText, int stageNum)
    {
        byte[] encrypted;

        using(Aes aesAlg = Aes.Create())
        {
            // AES�̐ݒ�
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            
            // �X�e�[�W�ԍ����ݒ肳��Ă��Ȃ������珈��
            if(!AES_IV_256.ContainsKey(stageNum))
            {
                // �X�e�[�W�ԍ���16���̃����_���ȉp�������Z�b�g�Őݒ�
                AES_IV_256.Add(stageNum, System.Guid.NewGuid().ToString("N").Substring(0, 16));
            }
            //AES_IV_256[stageNum] = System.Guid.NewGuid().ToString("N").Substring(0, 16);  // ���񃉃��_���ɂ���ꍇ
            //Debug.Log("�Í��ԍ��F" + stageNum + "�@IV�F" + AES_IV_256[stageNum]);
            aesAlg.IV = Encoding.ASCII.GetBytes(AES_IV_256[stageNum]);
            aesAlg.Key = Encoding.ASCII.GetBytes(AES_KEY_256);

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
    // �����͈Í������ꂽ�f�[�^(string), �X�e�[�W�ԍ�(int)
    public static string DecryptStringFromBytes_Aes(string cipherText, int stageNum)
    {
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            // AES�̐ݒ�(�Í��Ɠ���)
            aesAlg.BlockSize = 128;
            aesAlg.KeySize = 256;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            
            // �X�e�[�W�ԍ����ݒ肳��Ă����珈��
            if (AES_IV_256.ContainsKey(stageNum))
            {
                //Debug.Log("�����ԍ��F" + stageNum + "�@IV�F" + AES_IV_256[stageNum]);
                aesAlg.IV = Encoding.ASCII.GetBytes(AES_IV_256[stageNum]);
                aesAlg.Key = Encoding.ASCII.GetBytes(AES_KEY_256);

                // �X�g���[���ϊ������s����f�N���v�^�[���쐬
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // �������Ɏg�p����X�g���[�����쐬
                using (MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // �������X�g���[�����畜�������ꂽ�o�C�g�̓ǂݎ��A������ɔz�u
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        return plaintext;
    }
}
