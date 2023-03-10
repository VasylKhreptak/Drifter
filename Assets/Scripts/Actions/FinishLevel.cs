using CBA.Actions.Core;
using Zenject;

namespace Actions
{
    public class FinishLevel : Action
    {
        private LevelFinishCommand _levelFinishCommand;

        [Inject]
        private void Construct(LevelFinishCommand levelFinishCommand)
        {
            _levelFinishCommand = levelFinishCommand;
        }

        public override void Do()
        {
            _levelFinishCommand.Do();
        }
    }
}
