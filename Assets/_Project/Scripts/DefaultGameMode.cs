using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace com.ColorSelect
{
    public class DefaultGameMode
    {
        public enum State
        {
            Idle,
            MatchInProgress,
            MatchFinished,
            PlayingFx,
        }

        private readonly Config _config;
        private readonly ColorSelectable.Pool _pool;
        private readonly ColorsService _colorsService;
        private readonly ILogger _logger;
        private readonly MatchView _view;

        private readonly ColorSelectable[] _colorSelectables;
        private MatchModel _matchModel;
        private State _state;

        public DefaultGameMode(
            ILogger logger,
            Config config,
            ColorSelectable.Pool pool,
            ColorsService colorsService,
            MatchView view)
        {
            _logger = logger;
            _pool = pool;
            _config = config;
            _colorsService = colorsService;
            _view = view;
            _colorSelectables = new ColorSelectable[_config.SelectablesCount];
        }

        public void Initialize()
        {
            _colorsService.Initialize();
            _state = State.Idle;
        }

        public void PrepareMatch(MatchModel matchModel)
        {
            _matchModel = matchModel;
            int count = _config.SelectablesCount;
            _colorsService.GetRandomColors(_matchModel.Colors);
            _matchModel.WinningColorIndex = Random.Range(0, count);
            _view.SetColor(_matchModel.WinningColor);
            // add half total width offset to center selectables
            float offset = -(count - 1) * 0.5f * _config.SpacePerSelectable;

            for (int i = 0; i < _config.SelectablesCount; i++)
            {
                Vector3 spawnPosition = new(i * _config.SpacePerSelectable + offset, 0);
                ColorSelectable item = _pool.Spawn(spawnPosition, _matchModel.Colors[i]);
                item.OnSelected += HandleColorSelected;
                _colorSelectables[i] = item;
            }

            _view.Activate(true);
        }

        public async UniTask<MatchModel> RunMatch()
        {
            _logger.LogRegular("Running match");
            _state = State.MatchInProgress;

            while (_state != State.MatchFinished)
            {
                await UniTask.DelayFrame(_config.AsyncWaitsFrameSkips);
            }

            MatchModel model = _matchModel;
            _matchModel = null;
            return model;
        }

        private void HandleColorSelected(ColorSelectable selectable, ColorDescription color)
        {
            if (_state != State.MatchInProgress)
            {
                return;
            }
            
            _matchModel.AddAttempt();

            if (color == _matchModel.WinningColor)
            {
                FinishMatch();
                return;
            }

            HandleWrongSelection(selectable, color).Forget();
        }

        private void FinishMatch()
        {
            for (int i = 0; i < _colorSelectables.Length; i++)
            {
                ColorSelectable selectable = _colorSelectables[i];
                _colorSelectables[i] = null;
                selectable.OnSelected -= HandleColorSelected;
                // despawning and re-spawning isn't really needed for our use case
                // but is almost free because of pooling
                // so I've added it for flexibility
                _pool.Despawn(selectable);
            }

            _state = State.MatchFinished;
        }

        private async UniTask HandleWrongSelection(ColorSelectable selectable, ColorDescription originalColor)
        {
            _state = State.PlayingFx;
            selectable.SetColor(_config.WrongSelectionFxColor);
            await UniTask.Delay(_config.WrongSelectionFxDurationMs);
            selectable.SetColor(originalColor);
            _state = State.MatchInProgress;
        }
    }
}