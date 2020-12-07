using System.Collections.Generic;
using System.Linq;

namespace FizerFox.Meta
{
	public class LobbyData
	{
		public int CurrentStage;
		public List<SongData> SongDatas = new List<SongData>();

		public SongData GetSongData(SongId id) => SongDatas.FirstOrDefault((data) => data.Id == id);
	}
}
