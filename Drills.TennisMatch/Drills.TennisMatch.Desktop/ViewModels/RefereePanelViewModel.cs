using System;

using Drills.TennisMatch.Desktop.Commands;

namespace Drills.TennisMatch.Desktop.ViewModels
{
    public class RefereePanelViewModel : BaseViewModel
    {
        public RelayCommand PointToP1Command { get; set; }

        public RelayCommand PointToP2Command { get; set; }

        public RelayCommand StartMatchCommand { get; set; }

        private bool _isLiveMatch;

        public bool IsLiveMatch { get => _isLiveMatch; set => SetProperty(ref _isLiveMatch, value); }
    }
}
