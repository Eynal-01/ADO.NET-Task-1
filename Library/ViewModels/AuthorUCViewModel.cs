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
        }
    }
}
