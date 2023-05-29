using Library.Commands;
using Library.Models;
using Library.Repositories;
using System.Collections.ObjectModel;

namespace Library.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand CheckAllCommand { get; set; }
        public RelayCommand SelectionChanged { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand AddAuthorCommand { get; set; }

        public Repo AuthorsRepo { get; set; }

        private ObservableCollection<Author> authors;

        public ObservableCollection<Author> Authors
        {
            get { return authors; }
            set { authors = value; OnPropertyChanged();}
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }

        private int ID;

        public int SelectedId
        {
            get { return ID; }
            set { ID = value; OnPropertyChanged(); }
        }


        public MainWindowViewModel()
        {
            AuthorsRepo=new Repo();

            Authors = AuthorsRepo.Getall();

            AddAuthorCommand = new RelayCommand((obj) =>
            {
                AuthorsRepo.AddAuthor(Id, FirstName, LastName);
            });

            SelectionChanged = new RelayCommand((obj) =>
            {
                AuthorsRepo.DeleteAuthor(SelectedId);
            });
        }
    }
}