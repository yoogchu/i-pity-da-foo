using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{
	public EventSound3D eventSound3DPrefab;

	public AudioClip jumpAudio;
	public AudioClip blockCollisionAudio;
	public AudioClip landAudio;
	public AudioClip gruntAudio;
	public AudioClip shootAudio;


	private UnityAction<Vector3> jumpEventListener;
	private UnityAction<Vector3,float> blockCollisionEventListener;
	private UnityAction<Vector3,float> landEventListener;
//	private UnityAction<Vector3,float> shootEventListener;

	void Awake()
	{
		jumpEventListener = new UnityAction<Vector3>(jumpEventHandler);
		blockCollisionEventListener = new UnityAction<Vector3,float>(blockCollisionEventHandler);
		landEventListener = new UnityAction<Vector3,float>(landEventHandler);
//		shootEventListener = new UnityAction<Vector3,float>(shootEventHandler);
	}

	// Use this for initialization
	void Start()
	{

	}
		
	void OnEnable()
	{
		EventManager.StartListening<JumpEvent, Vector3>(jumpEventListener);
		EventManager.StartListening<PlayerLandsEvent, Vector3,float>(landEventListener);
		EventManager.StartListening<BlockCollisionEvent, Vector3,float>(blockCollisionEventListener);

	}

	void OnDisable()
	{
		EventManager.StopListening<JumpEvent, Vector3>(jumpEventListener);
		EventManager.StopListening<PlayerLandsEvent, Vector3,float>(landEventListener);
		EventManager.StopListening<BlockCollisionEvent, Vector3,float>(blockCollisionEventListener);
	}

	void blockCollisionEventHandler(Vector3 worldPos, float impactForce)
	{
		//AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

		const float halfSpeedRange = 0.2f;

		EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

		snd.audioSrc.clip = this.blockCollisionAudio;

		snd.audioSrc.pitch = Random.Range(1f-halfSpeedRange, 1f+halfSpeedRange);

		snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce /200f);
		snd.audioSrc.maxDistance = 100f;

		snd.audioSrc.Play();
	}

	void landEventHandler(Vector3 worldPos, float collisionMagnitude)
	{
		//AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

		if (eventSound3DPrefab)
		{
			if (collisionMagnitude > 300f)
			{

				EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

				snd.audioSrc.clip = this.landAudio;

				snd.audioSrc.minDistance = 5f;
				snd.audioSrc.maxDistance = 100f;

				snd.audioSrc.Play();

				if (collisionMagnitude > 500f)
				{

					EventSound3D snd2 = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

					snd2.audioSrc.clip = this.gruntAudio;

					snd2.audioSrc.minDistance = 5f;
					snd2.audioSrc.maxDistance = 100f;

					snd2.audioSrc.Play();
				}
			}


		}
	}

	void jumpEventHandler(Vector3 worldPos)
	{
		if (eventSound3DPrefab)
		{

			EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

			snd.audioSrc.clip = this.jumpAudio;

			snd.audioSrc.minDistance = 5f;
			snd.audioSrc.maxDistance = 100f;

			snd.audioSrc.Play();
		}
	}

}
