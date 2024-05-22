﻿using PillMe.Models;
using PillMe.Views;
using Plugin.LocalNotification;

namespace PillMe
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "pills.db";
        public static Repository? database;
        public static Repository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            
            MainPage = new Shell
            {
                CurrentItem = new MainPage()
            };
        }
    }
}
