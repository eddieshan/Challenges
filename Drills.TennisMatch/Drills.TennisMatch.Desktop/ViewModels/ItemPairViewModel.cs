using System;

namespace Drills.TennisMatch.Desktop.ViewModels
{
    public class ItemPairModel<T>: BaseViewModel
    {
        private string _title;

        public string Title { get => _title; }

        private T _player1;

        public T Player1 { get => _player1; set => SetProperty(ref _player1, value); }

        private T _player2;

        public T Player2 { get => _player2; set => SetProperty(ref _player2, value); }

        private string _winner;

        public string Winner { get => _winner; set => SetProperty(ref _winner, value); }

        public ItemPairModel(string title) => _title = title;

        public void Update(T p1, T p2)
        {
            Player1 = p1;
            Player2 = p2;
        }

        public void Update(T p1, T p2, string winner)
        {
            Player1 = p1;
            Player2 = p2;
            Winner = winner;
        }
    }
}
