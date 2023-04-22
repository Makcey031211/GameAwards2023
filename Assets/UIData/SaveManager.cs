using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

//- �X�e�[�W�N���A�󋵂̕ۑ��ǂݍ��݂��s��
public class SaveManager : MonoBehaviour
{
    private const int STAGE_NUM = 50;    //���X�e�[�W��
    private string FILE_PATH;            //�Z�[�u�f�[�^�p�X
    private bool[] stageflag = new bool[STAGE_NUM];//�X�e�[�W�����t���O�z����쐬

    private void Start()
    {
        //- �f�[�^�ǂݍ���
        DataLoad();
    }

    /// <summary>
    /// �X�e�[�W�N���A��������ݒ肷��
    /// </summary>
    /// <param name="stageNum"></param>
    public void SetStageClear(int stageNum)
    {
        if(stageNum >= 1 && stageNum <= STAGE_NUM)
        {
            //- �z��ɃN���A�󋵂�ۑ�
            stageflag[stageNum - 1] = true;
            //- ��������
            DataSave();
        }
    }

    /// <summary>
    /// �X�e�[�W���N���A����Ă��邩��Ԃ�
    /// </summary>
    /// <param name="stageNum"></param>
    /// <returns></returns>
    public bool GetStageClear(int stageNum)
    {
        //- �X�e�[�W�����͈͓���
        if(stageNum >= 1 && stageNum <= STAGE_NUM)
        {
            //- �X�e�[�W�̃N���A�󋵃t���O��Ԃ�
            return stageflag[stageNum - 1];
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
            using (StreamReader sr = new StreamReader(FILE_PATH,Encoding.UTF8)) //using:�����I�ɃN���[�Y����
            {
                string line;    //1�s���̕������ǂݍ���
                int i = 0;      
                //- �ǂݍ���ł���s��null����Ȃ����s�������[�v����
                while((line = sr.ReadLine()) != null && i < STAGE_NUM)
                {
                    bool.TryParse(line, out stageflag[i]);  //Line�������bool�^�ɕϊ����A�t���O�z��ɐݒ�
                    i++;
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
    private void DataSave()
    {
        FILE_PATH = Path.Combine(Application.dataPath, "Save", "Save.csv");    //UnityEditor��ł̃Z�[�u�t�@�C���p�X
        //- �e�L�X�g����������
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.UTF8))
        {
            //- �X�e�[�W�����X�V����
            for(int i = 0; i < STAGE_NUM; i++)
            {   
                //- �X�e�[�W�t���O�𕶎���ɂ��ď�������
                sw.WriteLine(stageflag[i].ToString());
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
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.UTF8))
        {
            //- �X�e�[�W������������
            for(int i = 0; i < STAGE_NUM; i++)
            {
                sw.WriteLine(false);
                stageflag[i] = false;
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
        using (StreamWriter sw = new StreamWriter(FILE_PATH, false, Encoding.UTF8))
        {
            //- �X�e�[�W������������
            for (int i = 0; i < STAGE_NUM; i++)
            {
                sw.WriteLine(false);
                stageflag[i] = false;
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
