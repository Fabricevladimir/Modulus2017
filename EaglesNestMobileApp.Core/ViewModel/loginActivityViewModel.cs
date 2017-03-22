﻿/*****************************************************************************/
/*                          LoginActivityViewModel                           */
/* This ViewModel handles all the logic for the login activity. Event        */
/* handlers and data from that activity are also in here.                    */
/*                                                                           */
/*****************************************************************************/
using EaglesNestMobileApp.Core.Contracts;
using EaglesNestMobileApp.Core.Model;
using EaglesNestMobileApp.Core.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
namespace EaglesNestMobileApp.Core.ViewModel
{
    public class LoginActivityViewModel : ViewModelBase
    {
        /* This makes sure that the login button is disabled so that the user  */
        /* does not make multiple calls to the database.                       */
        private bool enableButton = true;
        public bool EnableButton
        {
            get { return enableButton; }
            set
            {
                Set(() => EnableButton, ref enableButton, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        /* This command handles the login event                                */
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand => _loginCommand ??
            (_loginCommand = new RelayCommand(
                async () => await AttemptLoginAsync(), () => EnableButton));

        /* This could be stored in the database and be used for determining    */
        /* whether the user logged out on startup.                             */
        public LocalToken CurrentUser { get; set; } = new LocalToken();


        private AzureToken _remote;
        public AzureToken Remote
        {
            get { return _remote; }
            set { Set(() => Remote, ref _remote, value); }
        }

        /* Singleton instance of the database                    */
        private readonly IAzureService Database;

        public LoginActivityViewModel(IAzureService database)
        {
            Database = database;
        }

        /* This function allows the user to login providing he has the         */
        /* correct credentials                                                 */
        private async Task AttemptLoginAsync()
        {
            /* Disable the login button                                         */
            EnableButton = false;
            Debug.WriteLine($"\n\n\n\n\n\n{CurrentUser.Id}, {CurrentUser.Password}");
            Debug.WriteLine($"\n\n\n\n\n\n{CurrentUser.Id}, {CurrentUser.Password}");
            Debug.WriteLine($"\n\n\n\n\n\n{CurrentUser.Id}, {CurrentUser.Password}");

            /* REMEMBER TO REMOVE BACKDOOR                                      */
            if (CurrentUser.Id == "123")
                NavigateToMainPage();
            else
            {
                /* This will take a while depending on the Internet connection   */
                /* speed. Consider giving the user some indication.              */
                try
                {
                    await Database.InitLocalStore();
                    LocalToken _temporaryToken = await Database.GetLocalTokenAsync();

                    Debug.WriteLine($"\n\n\n\n\n\n{_temporaryToken.Id}, {_temporaryToken.Password}");
                    Debug.WriteLine($"\n\n\n\n\n\n{_temporaryToken.Id}, {_temporaryToken.Password}");
                    Debug.WriteLine($"\n\n\n\n\n\n{_temporaryToken.Id}, {_temporaryToken.Password}");
                    if (_temporaryToken != null)
                    {
                        /* Allow access to the application main page             */
                        App.Locator.User = _temporaryToken;
                        NavigateToMainPage();
                    }
                    else
                    {
                        Remote = await Database.GetAzureTokenAsync(CurrentUser);

                        Debug.WriteLine(Remote.Id);
                        /* Compare the given credentials with the one gotten from     */
                        /* Azure and navigate to the mainpage. The plan is to save    */
                        /* CurrentUser in the database as a TOKEN so that we can      */
                        /* query using the id number whenever we need to get          */
                        /* information related to that student.                       */
                        if (Authenticator.VerifyPassword(CurrentUser.Password,
                               Remote.HashedPassword, Remote.Salt))
                        {
                            CurrentUser.LoggedIn = EnableButton = true;

                            /* Set the password to empty so that no sensitive          */
                            /* information is actually stored on the phone. Then, add  */
                            /* the token to the database.                              */
                            CurrentUser.Password = string.Empty;

                            /* Add the user to the database for future use and also add */
                            /* a reference to the user for the application lifecycle    */
                            App.Locator.User = CurrentUser;
                            await Database.InsertLocalTokenAsync(CurrentUser);
                            await Database.SyncAsync();
                            /* Allow access to the application main page             */
                            NavigateToMainPage();
                        }
                    }
                }
                /* How are we going to signal to the user the different errors?  */
                /* NO INTERNET ACCESS, BAD CREDENTIALS!                          */
                /* Should we check for Internet access once the application      */
                /* loads and warn them there?                                    */
                catch (Exception NoConnection)
                {

                    Debug.WriteLine(Remote.Id);
                    Debug.WriteLine(Remote.Id);
                    Debug.WriteLine($"{CurrentUser.Id}, {CurrentUser.Password}, {NoConnection.ToString()}");
                    CurrentUser.LoggedIn = EnableButton = true;
                }
            }
        }

        /* Starts the mainActivity                                             */
        private void NavigateToMainPage()
        {
            App.Locator.Navigator.NavigateTo(App.PageKeys.MainPageKey);
        }
    }
}