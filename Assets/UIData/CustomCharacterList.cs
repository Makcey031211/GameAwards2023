//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEditor;
//using UnityEngine.UIElements;

//public class CustomCharacterList : MonoBehaviour
//{
//    [SerializeField, Header("使用する文字を出力するテキスト")]
//    private string outputTextPath = null;

//    [MenuItem("Tools/SettingOutputText")]
//    public static void CreateCustomCharacterList()
//    {
//        CustomCharacterList characterList = FindObjectOfType<CustomCharacterList>();
//        if (characterList != null)
//        {
//            characterList.OutputText();
//        }
//    }

//    private void OutputText()
//    {
//        // 出力先パスを選択
//        string path = EditorUtility.SaveFilePanel("Save text file", "", "", "txt");
//        if (path == "")
//        {
//            Debug.LogError("パスが指定されていません");
//            return;
//        }

//        outputTextPath = path;

//        List<char> characterList = new List<char>();

//        // ひらがな
//        for (int i = 0; i < 82; i++)
//        {
//            characterList.Add((char)(0x3041 + i));
//        }

//        // カタカナ
//        for (int i = 0; i < 86; i++)
//        {
//            characterList.Add((char)(0x30A1 + i));
//        }

//        // 半角カタカナ
//        for (int i = 0; i < 96; i++)
//        {
//            characterList.Add((char)(0xFF61 + i));
//        }

//        // 全角数字
//        for (int i = 0; i < 10; i++)
//        {
//            characterList.Add((char)(0x0030 + i));
//        }

//        // 半角数字
//        for (int i = 0; i < 10; i++)
//        {
//            characterList.Add((char)(0x0030 + i + 0xFEE0));
//        }

//        // 漢字（第一水準）
//        for (int i = 0; i < 3000; i++)
//        {
//            characterList.Add((char)(0x4E00 + i));
//        }

//        // その他特殊文字
//        characterList.Add((char)0x3002);  // 。
//        characterList.Add((char)0x300C);  // 「
//        characterList.Add((char)0x300D);  // 」
//        characterList.Add((char)0x3001);  // 、
//        characterList.Add((char)0x300E);  // 『
//        characterList.Add((char)0x300F);  // 』
//        characterList.Add((char)0x301C);  // 〜
//        characterList.Add((char)0x2018);  // ‘
//        characterList.Add((char)0x2019);  // ’
//        characterList.Add((char)0x201C);  // “
//        characterList.Add((char)0x201D);  // ”
//        characterList.Add((char)0xFF08);  // （
//        characterList.Add((char)0xFF09);  // ）
//        characterList.Add((char)0xFF01);  // ！
//        characterList.Add((char)0xFF0C);  // ，
//        characterList.Add((char)0xFF1A);  // ：
//        characterList.Add((char)0xFF1B);  // ；
//        characterList.Add((char)0xFF1F);  // ？
//        characterList.Add((char)0xFF3B);  // ［
//        characterList.Add((char)0xFF3D);  // ］
//        characterList.Add((char)0xFF5B);  // ｛
//        characterList.Add((char)0xFF5D);  // ｝
//        characterList.Add((char)0x3000);  // 　


//        // テキストファイルに出力
//        using (StreamWriter writer = new StreamWriter(outputTextPath, false))
//        {
//            foreach (char c in characterList)
//            {
//                writer.Write(c); // １文字ずつ出力
//            }
//        }
//        Debug.Log(outputTextPath + "にファイルを出力しました");
//    }
//}
