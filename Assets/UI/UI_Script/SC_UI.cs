using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_UI : MonoBehaviour
{

    public void LordScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}



public class RandomSoundPlayer : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip[] soundClips; // �����§��� 3 ���§
    [SerializeField] private AudioSource audioSource;
    
    [Header("Timing Settings")]
    [SerializeField] private float checkInterval = 30f; // ��Ǩ�ͺ�ء 30 �Թҷ�
    [SerializeField] [Range(0f, 1f)] private float soundProbability = 0.5f; // �͡�ʷ���������§ (50%)
    
    private float timer;

    void Start()
    {
        // ��Ǩ�ͺ����� AudioSource �������
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        timer = checkInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            CheckAndPlaySound();
            timer = checkInterval; // ���絵�ǨѺ����
        }
    }

    void CheckAndPlaySound()
    {
        // ��Ǩ�ͺ��������§����������������
        if (soundClips == null || soundClips.Length == 0)
        {
            Debug.LogWarning("��������§���������!");
            return;
        }
        
        // ������Ҩ�������§�������
        float randomValue = Random.value;
        
        if (randomValue < soundProbability)
        {
            // �������͡���§ 1 � 3
            int randomIndex = Random.Range(0, soundClips.Length);
            AudioClip selectedClip = soundClips[randomIndex];
            
            if (selectedClip != null)
            {
                audioSource.PlayOneShot(selectedClip);
                Debug.Log($"������§: {selectedClip.name} (Index: {randomIndex})");
            }
        }
        else
        {
            Debug.Log("���������§��ͺ���");
        }
    }
}