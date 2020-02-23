using System;
using System.Linq;

using Drills.TennisMatch.Component;
using Drills.TennisMatch.Desktop.Commands;
using Drills.TennisMatch.Desktop.Services;

namespace Drills.TennisMatch.Desktop.ViewModels
{
    public class TennisMatchViewModel : BaseViewModel
    {
        public ScoreBoardViewModel ScoreBoard { get; }

        public RefereePanelViewModel RefereePanel { get; }

        private readonly IMatchService _service;

        private Match Current { get; set; }

        private void StartMatch()
        {
            var newMatch = _service.Start();
            RefereePanel.IsLiveMatch = true;

            ScoreBoard.Start();

            Update(newMatch);
        }

        private void PointTo(bool isP1) =>
            Current.Match(
                    live => Update(live.PointTo(isP1)),
                    won => throw new InvalidOperationException("A WonMatch cannot be advanced."));

        public TennisMatchViewModel(IMatchService matchService)
        {
            _service = matchService;

            RefereePanel = new RefereePanelViewModel()
            {
                PointToP1Command = new RelayCommand(() => PointTo(true)),
                PointToP2Command = new RelayCommand(() => PointTo(false)),
                StartMatchCommand = new RelayCommand(() => StartMatch())
            };

            ScoreBoard = new ScoreBoardViewModel();

            StartMatch();
        }

        private void MatchFinished(WonMatch won)
        {
            RefereePanel.IsLiveMatch = false;
            ScoreBoard.UpdateWinner(won.Total.P1Wins());
            ScoreBoard.UpdateSet(won.Scores.Last().Total);
        }

        private void UpdateLive(LiveMatch match)
        {
            ScoreBoard.UpdateSets(match);

            match.Set.Game.Points
                 .Match(
                 loveAll => ScoreBoard.Game.Update(TennisLiterals.Love, TennisLiterals.Love),
                 pair => {
                     var pointsP1 = TennisLiterals.GetPointLiteral(pair.P1.Value);
                     var pointsP2 = TennisLiterals.GetPointLiteral(pair.P2.Value);
                     ScoreBoard.Game.Update(pointsP1, pointsP2);
                 },
                 deuce => ScoreBoard.Game.Update(TennisLiterals.Deuce, TennisLiterals.Deuce),
                 advantage =>
                 {
                     var p1 = advantage.P1Wins ? TennisLiterals.Advantage : TennisLiterals.Disadvantage;
                     var p2 = advantage.P1Wins ? TennisLiterals.Disadvantage : TennisLiterals.Advantage;
                     ScoreBoard.Game.Update(p1, p2);
                 }
                 ,
                 winner => ScoreBoard.Game.Update("Win", "Lose"));
        }

        private void Update(Match match)
        {
            Current = match;
            match.Match(
                  live => UpdateLive(live),
                  won => MatchFinished(won));
        }
    }
}