using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollsViewMediator : Mediator<SongScrollsView>
	{
		[Inject] private InitializeScrollCommand _command;

		public override void OnRegister()
		{
			_command.Execute();
		}
	}
}
