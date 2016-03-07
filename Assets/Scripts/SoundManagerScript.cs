using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManagerScript : MonoBehaviour {

	public AudioSource audioPlayer;
	public AudioClip[] audioClips;
	
	/*
	void Update(){
		if (!audioPlayer.isPlaying) {
			int randomIndex = Random.Range(0, audioClips.Length);
			AudioClip clip = audioClips[randomIndex];
			audioPlayer.clip = clip;
			audioPlayer.Play();
		}
	}
	*/

	//Loads and concatenates samples

	void Start(){
		
		float[] joinedSamples = new float[0];
		
		foreach(AudioClip clip in audioClips){
			Debug.Log("Adding: " + clip.name);
			//Create samples array with correct length to hold clip samples
			float[] samples = new float[clip.samples * clip.channels];
			//Load the current clip data into that array
			clip.GetData(samples, 0);
			//Create a new array with length of master plus new clip samples
			float[] tempSamples = new float[joinedSamples.Length + samples.Length];
			//Copy master in first
			joinedSamples.CopyTo(tempSamples, 0);
			//Cope new clip sample at end
			samples.CopyTo(tempSamples, joinedSamples.Length);
			//Save the new array to the master
			joinedSamples = tempSamples;
		}

		//Loads clip into audio player.  Needs player to have existing clip.
		//Should probably be creating a new clip and loading the samples in.
		audioPlayer.clip.SetData (joinedSamples, 0);
		audioPlayer.Play ();
	}
}
