// using UnityEngine;

// public class QuestionManager : MonoBehaviour
// {
//     public static QuestionManager Instance;
//     private void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);
//     }
//     [SerializeField] private CSE_Camera_Zoom cameraZoom;

//     public void PlayDeathCutscene()
//     {
//         StartCoroutine(DeathCutsceneRoutine());
//     }

//     private IEnumerator DeathCutsceneRoutine()
//     {
//         cameraZoom.Play();

//         yield return new WaitForSeconds(cameraZoom.duration);
//     }

// }
