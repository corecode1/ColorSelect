using Zenject;

namespace com.ColorSelect
{
    public class MatchModel
    {
        public ColorDescription[] Colors { get; }
        public int WinningColorIndex { get; set; }
        public int Attempts { get; private set; }
        public ColorDescription WinningColor => Colors[WinningColorIndex];

        public MatchModel(Config config)
        {
            Colors = new ColorDescription[config.ColorDescriptions.Length];
        }

        public void AddAttempt()
        {
            Attempts++;
        }

        public class Pool : MemoryPool<MatchModel>
        {
        }
    }
}