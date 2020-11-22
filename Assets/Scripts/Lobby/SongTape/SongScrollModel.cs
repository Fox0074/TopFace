using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Lobby.SongTape
{
	public class SongScrollModel
	{
		public string title { get; set; }
		public string author { get; set; }
		public int currentPoints { get; set; }
		public byte progress { get; set; }		// TODO float?
		public Difficulty difficulty { get; set; }	// TODO enum to float?
		public AudioClip audioPreview { get; set; }
	}
}
