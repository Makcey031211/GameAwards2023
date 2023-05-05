using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

//- �X�e�[�W�N���A�󋵂̕ۑ��ǂݍ��݂��s��
public class SaveManager : MonoBehaviour
{

    private const int STAGE_NUM = 50;    //���X�e�[�W��
    private string FILE_PATH;            //�Z�[�u�f�[�^�p�X
    private static bool[] stageflag = new bool[STAGE_NUM];//�X�e�[�W�����t���O�z����쐬

    private void Start()
    {
        //- �f�[�^�ǂݍ���
        DataLoad();
    }

    /// <summary>
    /// �X�e�[�W�N���A��������ݒ肷��
    /// </summary>
    /// <param name="stageNum"></param>
    public void SetStageClear(int Stage)
    {
        if(Stage >= 1 && Stage <= STAGE_NUM)
        {
            //- �z��ɃN���A�󋵂�ۑ�
            stageflag[Stage - 1] = true;
            //- ��������
            DataSave(Stage);
        }
    }

    /// <summary>
    /// �X�e�[�W���N���A����Ă��邩��Ԃ�
    /// </summary>
    /// <param name="stageNum"></param>
    /// <returns></returns>
    public bool GetStageClear(int Stage)
    {
        //- �X�e�[�W�����͈͓���
        if(Stage >= 1 && Stage <= STAGE_NUM)
        {
            //- �X�e�[�W�̃N���A�󋵃t���O��Ԃ�
            return stageflag[Stage - 1];
        }
        return false;
    }

    /// <summary>
    /// �Z�[�u�f�[�^���[�h
    /// </summary>
    private void DataLoad()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor��ł̃Z�[�u�t�@�C���p�X
        //- �p�X�̈ʒu�Ƀt�@�C�������݂��邩
        if (File.Exists(FILE_PATH))
        {
            //- �e�L�X�g��ǂݍ���
            using (StreamReader sr = new StreamReader(FILE_PATH,Encoding.ASCII)) //using:�����I�ɃN���[�Y����
            {
                string line;    //1�s���̕������ǂݍ���
                int i = 0;      
                //- �ǂݍ���ł���s��null����Ȃ����s�������[�v����
                while((line = sr.ReadLine()) != null && i < STAGE_NUM)
                {
                    string DecryptFlag = AesExample.DecryptStringFromBytes_Aes(line);   // �Í������ꂽ�t���O�̕���
                    bool.TryParse(DecryptFlag, out stageflag[i]);  //Line�������bool�^�ɕϊ����A�t���O�z��ɐݒ�
                    i++;

                    //--- �����̊m�F ---
                    //Debug.Log("�������F" + DecryptFlag);
                }
            }
        }
        else
        {
            //- �t�@�C�������݂��Ȃ���ΐ�������
            CreateSaveData();
        }
    }

    /// <summary>
    /// �f�[�^�̃Z�[�u���s��
    /// </summary>
    private void DataSave(int Stage)
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor��ł̃Z�[�u�t�@�C���p�X
        //- �e�L�X�g����������
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- �X�e�[�W�����X�V����
            for(int i = 0; i < STAGE_NUM; i++)
            {
                //Debug.Log(stageflag[i]);
                //- �X�e�[�W�t���O�𕶎���ɕύX���āA�Í����������̂���������
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);

                //--- �Í����̊m�F ---
                //Debug.Log("�Í����F" + EncryptFlag);
            }
        }
    }

    /// <summary>
    /// �Z�[�u�̏�����
    /// </summary>
    private void CreateSaveData()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor��ł̃Z�[�u�t�@�C���p�X
        //- �f�B���N�g���p�X���擾����
        string directoryPath = new FileInfo(FILE_PATH).Directory.FullName;
        //- �f�B���N�g�������݂��Ă��邩
        if(!Directory.Exists(directoryPath))
        {
            //- ���݂��Ă��Ȃ�������쐬����
            Directory.CreateDirectory(directoryPath);
        }
        //- �e�L�X�g����������
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- �X�e�[�W������������
            for(int i = 0; i < STAGE_NUM; i++)
            {
                stageflag[i] = false;
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);
            }
        }
    }

    /// <summary>
    /// �Z�[�u�f�[�^�����Z�b�g����
    /// </summary>
    public void ResetSaveData()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor��ł̃Z�[�u�t�@�C���p�X
        //- �e�L�X�g����������
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.ASCII))
        {
            //- �X�e�[�W������������
            for (int i = 0; i < STAGE_NUM; i++)
            {
                stageflag[i] = false;
                string EncryptFlag = AesExample.EncryptStringToBytes_Aes(stageflag[i].ToString());
                sw.WriteLine(EncryptFlag);
            }
        }
    }

    /*�@���[�[�[�[�[�[�g���R�[�h�[�[�[�[�[�[���@*/
#if UNITY_EDITOR
    //- Inspector�g���N���X
    [CustomEditor(typeof(SaveManager))]
    public class InitSaveData : Editor
    {
        private SaveManager save;
        public override void OnInspectorGUI()
        {
            //- ��b��Inspector�\��
            base.OnInspectorGUI();
            //- �{�^���\��
            if(GUILayout.Button("�Z�[�u�f�[�^���Z�b�g"))
            {
                save = new SaveManager();
                save.ResetSaveData();
                Debug.Log("[UI]DataReset");
                
            }
        }
    }
#endif
}
