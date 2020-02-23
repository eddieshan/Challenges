using System;
using System.Linq;
using System.Collections.ObjectModel;

using Drills.TennisMatch.Component;

namespace Drills.TennisMatch.Desktop.ViewModels
{
    public class ScoreBoardViewModel : BaseViewModel
    {
        public ObservableCollection<ItemPairModel<int>> Sets { get; set; }

        public ItemPairModel<string> Game { get; }

        public ItemPairModel<string> PlayersPair { get; }

        private string _winner;

        public string Winner { get => _winner; private set => SetProperty(ref _winner, value); }

        private bool _isLiveMatch;

        public bool IsLiveMatch { get => _isLiveMatch; private set => SetProperty(ref _isLiveMatch, value); }

        public ScoreBoardViewModel()
        {
            Sets = new ObservableCollection<ItemPairModel<int>>();            
            Game = new ItemPairModel<string>(title: "GAME");

            PlayersPair = new ItemPairModel<string>(title: String.Empty);
            PlayersPair.Update(TennisLiterals.P1, TennisLiterals.P2);
        }

        public void Start()
        {
            Winner = String.Empty;
            Sets.Clear();
            Sets.Add(NewSet());
        }

        private ItemPairModel<int> NewSet() => new ItemPairModel<int>(title: $"S{Sets.Count + 1}");

        public void UpdateSet(NumericScore score) => Sets.Last().Update(score.P1, score.P2, UpdateSetWinner(score));

        public void UpdateSets(LiveMatch match)
        {
            if (Sets.Count == match.Scores.Trail.Count())
            {
                var previous = match.Scores.Trail.LastOrDefault();
                UpdateSet(previous.Total);
                Sets.Add(NewSet());
            }

            UpdateSet(match.Set.Scores.Total);
        }

        public void UpdateWinner(bool p1Wins) => Winner = p1Wins? TennisLiterals.P1Wins : TennisLiterals.P2Wins;

        private string UpdateSetWinner(NumericScore score)
        {
            if (score.P1 == score.P2)
            {
                const int tieBreak = 6;
                if (score.P1 == 0)
                {
                    return TennisLiterals.EqualGames;
                }
                else
                {
                    return score.P1 == tieBreak ? TennisLiterals.Tiebreak : TennisLiterals.EqualGames;
                }
            }
            else
            {
                return score.P1Wins() ? TennisLiterals.P1 : TennisLiterals.P2;
            }
        }
    }
}
