using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace com.ColorSelect
{
    public class ColorSelectApp
    {
        private readonly ILogger _logger;
        private readonly DefaultGameMode _gameMode;
        private readonly MatchModel.Pool _matchModelPool;

        private readonly List<MatchModel> _completedMatches;

        public ColorSelectApp(
            ILogger logger,
            DefaultGameMode gameMode,
            MatchModel.Pool matchModelPool,
            Config config)
        {
            _matchModelPool = matchModelPool;
            _gameMode = gameMode;
            _logger = logger;

            _completedMatches = new List<MatchModel>(config.ExpectedMatchesPoolSize);
        }

        public void Initialize()
        {
            _gameMode.Initialize();
        }

        public void StartGame()
        {
            _logger.LogRegular("Starting game");
            RunMatchesIndefinitely().Forget();
        }

        private async UniTaskVoid RunMatchesIndefinitely()
        {
            while (true)
            {
                _gameMode.PrepareMatch(_matchModelPool.Spawn());
                MatchModel matchModel = await _gameMode.RunMatch();
                _completedMatches.Add(matchModel);
                
                _logger.Log(
                    "Completed match. Attempts: {0}, total matches completed: {1}",
                    LogLevel.Log,
                    matchModel.Attempts,
                    _completedMatches.Count);
            }
        }
    }
}