using Library.Models;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<Author> CurrentAuthors { get; internal set; }
        public static Window ChildWindow { get; internal set; }
    }
}
