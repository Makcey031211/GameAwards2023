using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

//- ステージクリア状況の保存読み込みを行う
public class SaveManager : MonoBehaviour
{

    private const int STAGE_NUM = 50;    //総ステージ数
    private string FILE_PATH;            //セーブデータパス
    private static bool[] stageflag = new bool[STAGE_NUM];//ステージ数分フラグ配列を作成

    private void Start()
    {
        //- データ読み込み
        DataLoad();
    }

    /// <summary>
    /// ステージクリアしたかを設定する
    /// </summary>
    /// <param name="stageNum"></param>
    public void SetStageClear(int Stage)
    {
        if(Stage >= 1 && Stage <= STAGE_NUM)
        {
            //- 配列にクリア状況を保存
            stageflag[Stage - 1] = true;
            //- 書き込み
            DataSave(Stage);
        }
    }

    /// <summary>
    /// ステージがクリアされているかを返す
    /// </summary>
    /// <param name="stageNum"></param>
    /// <returns></returns>
    public bool GetStageClear(int Stage)
    {
        //- ステージ数が範囲内か
        if(Stage >= 1 && Stage <= STAGE_NUM)
        {
            //- ステージのクリア状況フラグを返す
            return stageflag[Stage - 1];
        }
        return false;
    }

    /// <summary>
    /// セーブデータロード
    /// </summary>
    private void DataLoad()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor上でのセーブファイルパス
        //- パスの位置にファイルが存在するか
        if (File.Exists(FILE_PATH))
        {
            //- テキストを読み込む
            using (StreamReader sr = new StreamReader(FILE_PATH,Encoding.ASCII)) //using:自動的にクローズする
            {
                string line;    //1行分の文字列を読み込む
                int i = 0;      
                //- 読み込んでいる行がnullじゃないかつ行数分ループする
                while((line = sr.ReadLine()) != null && i < STAGE_NUM)
                {
                    string DecryptFlag = AesExample.DecryptStringFromBytes_Aes(line);   // 暗号化されたフラグの復号
                    bool.TryParse(DecryptFlag, out stageflag[i]);  //Line文字列をbool型に変換し、フラグ配列に設定
                    i++;

                    //--- 復号の確認 ---
                    //Debug.Log("復号化：" + DecryptFlag);
                }
            }
        }
        else
        {
            //- ファイルが存在しなければ生成する
            CreateSaveData();
        }
    }

    /// <summary>
    /// データのセーブを行う
    /// </summary>
    private void DataSave(int Stage)
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor上でのセーブファイルパス
        //- テキストを書き込む
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- ステージ数分更新する
            for(int i = 0; i < STAGE_NUM; i++)
            {
                //Debug.Log(stageflag[i]);
                //- ステージフラグを文字列に変更して、暗号化したものを書き込む
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);

                //--- 暗号化の確認 ---
                //Debug.Log("暗号化：" + EncryptFlag);
            }
        }
    }

    /// <summary>
    /// セーブの初期化
    /// </summary>
    private void CreateSaveData()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor上でのセーブファイルパス
        //- ディレクトリパスを取得する
        string directoryPath = new FileInfo(FILE_PATH).Directory.FullName;
        //- ディレクトリが存在しているか
        if(!Directory.Exists(directoryPath))
        {
            //- 存在していなかったら作成する
            Directory.CreateDirectory(directoryPath);
        }
        //- テキストを書き込む
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- ステージ分初期化する
            for(int i = 0; i < STAGE_NUM; i++)
            {
                stageflag[i] = false;
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);
            }
        }
    }

    /// <summary>
    /// セーブデータをリセットする
    /// </summary>
    public void ResetSaveData()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor上でのセーブファイルパス
        //- テキストを書き込む
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- ステージ分初期化する
            for (int i = 0; i < STAGE_NUM; i++)
            {
                stageflag[i] = false;
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);
            }
        }
    }

    /*　◇ーーーーーー拡張コードーーーーーー◇　*/
#if UNITY_EDITOR
    //- Inspector拡張クラス
    [CustomEditor(typeof(SaveManager))]
    public class InitSaveData : Editor
    {
        private SaveManager save;
        public override void OnInspectorGUI()
        {
            //- 基礎のInspector表示
            base.OnInspectorGUI();
            //- ボタン表示
            if(GUILayout.Button("セーブデータリセット"))
            {
                save = new SaveManager();
                save.ResetSaveData();
                Debug.Log("[UI]DataReset");
                
            }
        }
    }
#endif
}
