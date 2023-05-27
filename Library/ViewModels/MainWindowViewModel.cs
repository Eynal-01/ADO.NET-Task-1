using Library.Commands;
using Library.Helpers;
using Library.Models;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Library.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand CheckAllCommand { get; set; }
        public RelayCommand DeleteCommmand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand AddAuthorCommand { get; set; }

        public WrapPanel AuthorsWrapPanel { get; internal set; }

        bool checkAll = true;

        public MainWindowViewModel()
        {
            AddAuthorCommand = new RelayCommand((a) =>
            {
                var addWindow = new AddAuthorWindow();
                var addWindowViewModel = new AddAuthorViewModel();
                addWindow.DataContext = addWindowViewModel;
                App.ChildWindow = addWindow;
                addWindow.Show();
            });

            RefreshCommand = new RelayCommand((r) =>
            {
                AddAuthorsToView(DatabaseHelper.GetAuthorsFromDb());
                MessageBox.Show(App.Current.MainWindow, "Successfully refreshed!", "Success!");
            });

            DeleteCommmand = new RelayCommand((d) =>
            {
                //if (AuthorsWrapPanel.Children[0] is NoResultFoundUC)
                //    return;

                var IDs = new List<string>();
                foreach (var view in AuthorsWrapPanel.Children)
                {
                    var viewModel = (view as AuthorUC).DataContext as AuthorUCViewModel;
                    if (viewModel.IsChecked == true)
                    {
                        IDs.Add(viewModel.Author.Id);
                    }
                }
                if (IDs.Count == 0)
                {
                    MessageBox.Show(App.Current.MainWindow, "Please, check author!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                    return;
                }
                DatabaseHelper.DeleteAuthorsByID(IDs);
                AddAuthorsToView(DatabaseHelper.GetAuthorsFromDb());
                MessageBox.Show(App.Current.MainWindow, $"{IDs.Count} author(s) deleted successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
            });

            CheckAllCommand = new RelayCommand((c) =>
            {
                //if (AuthorsWrapPanel.Children[0] is NoResultFoundUC)
                //    return;

                foreach (var view in AuthorsWrapPanel.Children)
                {
                    ((view as AuthorUC).DataContext as AuthorUCViewModel).IsChecked = checkAll;
                }
                checkAll = !checkAll;
            });
        }

        public void AddAuthorsToView(List<Author> authors)
        {
            AuthorsWrapPanel.Children.Clear();

            if (authors.Count == 0)
            {
               // var noResultUC = new NoResultFoundUC();
                //AuthorsWrapPanel.Children.Add(noResultUC);
                return;
            }
            foreach (var author in authors)
            {
                var authorUC = new AuthorUC();
                var authorUCViewModel = new AuthorUCViewModel(author);
                authorUC.DataContext = authorUCViewModel;
                AuthorsWrapPanel.Children.Add(authorUC);
            }
        }
    }
}