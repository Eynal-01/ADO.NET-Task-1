using Library.Commands;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class AuthorUCViewModel : BaseViewModel
    {
        public RelayCommand EditCommand { get; set; }

        public Author Author { get; set; }

        private bool isChecked = false;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged(); }
        }

        public AuthorUCViewModel(Author author)
        {
            Author = author;

            //EditCommand = new RelayCommand((e) =>
            //{
            //    var editWindow = new EditWindow();
            //    var editWindowViewModel = new EditWindowViewModel(author);
            //    editWindow.DataContext = editWindowViewModel;

            //    editWindow.Title = $"Edit Author {author.FirstName.Trim()} {author.LastName.Trim()}";
            //    App.ChildWindow = editWindow;
            //    editWindow.ShowDialog();
            //});
        }
    }
}
