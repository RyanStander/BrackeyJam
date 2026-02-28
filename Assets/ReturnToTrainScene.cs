using AudioManagement;
using PersistentManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class ReturnToTrainScene : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera _cinemachineCamera;
    public Vector3 targetPosition = new Vector3(50f, 0f, 0f);
    public Transform FollowFront;
    private float speed;
    public bool Interact(Interaction Interaction)
    {
        _cinemachineCamera.Follow = FollowFront;
        StartCoroutine(MoveToTrain());
        AudioManager.StopMusic(true);
        AudioManager.StopAmbience(true);
        //SceneManager.LoadScene("TrainTravelScene");
        return true;
    }

    public void TrainMovement()
    {

        speed += Time.deltaTime * 4f;
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, 
            targetPosition, 
            speed * Time.deltaTime);
    }

    public IEnumerator MoveToTrain()
    {
        while (Vector3.Distance(transform.parent.position, targetPosition) > 0.1f)
        {
            TrainMovement();
            yield return null;
        }
        SceneManager.LoadScene("TrainTravelScene");
    }

    private void Awake()
    {
        _cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }
}
