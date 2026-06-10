using System;
using Cysharp.Threading.Tasks;
using Game.UI.Hud;
using Game.Utils;
using Game.Windows.General;
using Game.Windows.WinWindow;
using Game.Windows.LoseWindow;
using UnityEngine;

namespace Game.Gameplay
{
    public class GameplaySystem
    {
        public event Action GamePreparedEvent;
        public event Action GameStartedEvent;
        public event Action GameFinishedEvent;
        
        private readonly FieldGenerator _generator;
        private readonly FieldFiller _filler;
        private readonly FieldInputController _inputController;
        private readonly FieldProgressController _progressController;
        private readonly WindowSystem _windowSystem;
        private readonly Ticker _ticker;
        private readonly HudView _hudView;
        
        private readonly InitialFieldData _initialFieldData;
        
        private CellModel[,] _cellDataArray;
        
        private bool _gameStarted;
        
        
        public GameplaySystem(FieldFiller filler, FieldInputController inputController, 
            FieldGenerator generator, FieldProgressController progressController, WindowSystem windowSystem, 
            InitialFieldData data, Ticker ticker, HudView hudView)
        {
            _generator = generator;
            _filler = filler;
            _inputController = inputController;
            _progressController = progressController;
            _windowSystem = windowSystem;
            _initialFieldData = data;
            _ticker = ticker;
            _hudView = hudView;
        }

        public void PrepareGame()
        {
            var fieldSize = _initialFieldData.Size;
            _cellDataArray = _generator.GenerateField(fieldSize);
            _inputController.CellPressedEvent += StartGame;
            _progressController.Prepare(_cellDataArray);
            
            _inputController.SetActive(true);
            
            GamePreparedEvent?.Invoke();;
        }

        private void StartGame(Vector2Int startPosition)
        {
            _inputController.CellPressedEvent -= StartGame;
            
            var fieldSize = _initialFieldData.Size;
            var bombsAmount = _initialFieldData.BombsAmount;
            var data = new FieldGenerationData
            {
                BombsAmount = bombsAmount,
                Size = fieldSize,
                StartPosition = startPosition,
                CellDataArray = _cellDataArray
            };
            
            _filler.GenerateContent(data);
            
            _progressController.NoCellsLeftEvent += HandleWin;
            _progressController.BombPressedEvent += HandleLose;
            
            _progressController.Start(fieldSize, bombsAmount);
            _progressController.HandleCellPressed(startPosition);
            
            _ticker.SetPaused(false);
            _gameStarted = true;
            
            GameStartedEvent?.Invoke();
        }

        public void Pause()
        {
            _inputController.SetActive(false);
            
            if (_gameStarted == false)
                return;
            
            _ticker.SetPaused(true);
        }

        public void Unpause()
        {
            _inputController.SetActive(true);
            
            if (_gameStarted == false)
                return;
            
            _ticker.SetPaused(false);
        }

        private void Finish()
        {
            _inputController.SetActive(false);
            _ticker.SetPaused(true);
            
            GameFinishedEvent?.Invoke();
        }

        public void Restart()
        {
            Debug.Log("Restart");
            
            Reset();
            
            PrepareGame();
        }

        public void Reset()
        {
            Finish();
            
            _hudView.ResetHud();
            _generator.ResetField(_cellDataArray);
            
            _cellDataArray = null;
            _gameStarted = false;
            
            _progressController.Clear();
            _inputController.Clear();
        }

        private void HandleWin()
        {
            Debug.Log("Win");
            
            Finish();
            _windowSystem.Open<WinWindow>().Forget();
        }

        private void HandleLose()
        {
            Debug.Log("Lose");
            
            Finish();
            _windowSystem.Open<LoseWindow>().Forget();
        }
    }
}