using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinUI.Dashboard.Extention;
using XamarinUI.Dashboard.Models;

namespace XamarinUI.Dashboard
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string TitleBack { get { return "Back"; } }

        public MainViewModel()
        {
            BackgroundClicked = new Command(() => IsExpanded = false);

            LoadUserMenu();

            LoadBodyMenu();

            CurrentFeed = new ObservableCollection<Feed>
            {
                new Feed
                {
                    Title = "Xamarin.Forms Event",
                    Subtitle = "Save the date!",
                    Description = "Mussum Ipsum, cacilds vidis litro abertis. Paisis, filhis, espiritis santis. Si u mundo tá muito paradis? Toma um mé que o mundo vai girarzis! Nullam volutpat risus nec leo commodo, ut interdum diam laoreet. Sed non consequat odio. Casamentiss faiz malandris se pirulitá.",
                    ImageUri = "https://i0.wp.com/oraculoti.com.br/wp-content/uploads/2018/04/Curso-Xamarin-Forms-2018-Apps-para-Android-iOS-e-UWP-8-Apps.jpg",
                    AuthorImage = "https://avatars1.githubusercontent.com/u/5353685?v=4"
                },
                new Feed
                {
                    Title = "DotNet Event",
                    Subtitle = "Save the date!",
                    Description = "Mussum Ipsum, cacilds vidis litro abertis. Paisis, filhis, espiritis santis. Si u mundo tá muito paradis? Toma um mé que o mundo vai girarzis! Nullam volutpat risus nec leo commodo, ut interdum diam laoreet. Sed non consequat odio. Casamentiss faiz malandris se pirulitá.",
                    ImageUri = "https://baltaio.blob.core.windows.net/blog/dotnetconf-dotnetcore-resumo.jpg",
                    AuthorImage = "https://avatars1.githubusercontent.com/u/5353685?v=4"
                },
                new Feed
                {
                    Title = "Microsoft Event",
                    Subtitle = "Save the date!",
                    Description = "Mussum Ipsum, cacilds vidis litro abertis. Paisis, filhis, espiritis santis. Si u mundo tá muito paradis? Toma um mé que o mundo vai girarzis! Nullam volutpat risus nec leo commodo, ut interdum diam laoreet. Sed non consequat odio. Casamentiss faiz malandris se pirulitá.",
                    ImageUri = "https://images.techhive.com/images/article/2015/08/microsoft-logo-redwest-a-100611028-large.jpeg",
                    AuthorImage = "https://avatars1.githubusercontent.com/u/5353685?v=4"
                }
            };
        }

        private void LoadUserMenu()
        {
            UserList = new ObservableCollection<MenuBody>
            {
                new MenuBody
                {
                    Text = "Address",
                    Icon = IconFont.MapMarkedAlt,
                },
                new MenuBody
                {
                    Text = "Credit Cards",
                    Icon = IconFont.CreditCard,
                },
                new MenuBody
                {
                    Text = "Favorites",
                    Icon = IconFont.Star
                }.AddChild(new List<MenuBody>
                    {
                        new MenuBody
                        {
                            Text = "Products",
                            Icon = IconFont.Guitar
                        },
                        new MenuBody
                        {
                            Text = "Services",
                            Icon = IconFont.Taxi
                        },
                    }),
                new MenuBody
                {
                    Text = "Wallets",
                    Icon = IconFont.Wallet,
                },
                new MenuBody
                {
                    Text = "Logout",
                    Icon = IconFont.SignOutAlt,
                },
            };
        }

        private void LoadBodyMenu()
        {
            var bodyMenu = new List<MenuBody>
            {
                new MenuBody
                {
                    Text = "Categories",
                    Icon = IconFont.ShoppingBag
                }.AddChild(new List<MenuBody>
                    {
                        new MenuBody
                        {
                            Text = "Japonese",
                            //Icon = IconFont.List
                        },
                        new MenuBody
                        {
                            Text = "Italian",
                            //Icon = IconFont.List
                        },
                        new MenuBody
                        {
                            Text = "Brazilian",
                            //Icon = IconFont.List
                        }
                    }),
                new MenuBody
                {
                    Text = "Near",
                    Icon = IconFont.MapMarkedAlt,
                },
                new MenuBody
                {
                    Text = "Famous",
                    Icon = IconFont.Star,
                },
                new MenuBody
                {
                    Text = "Promotion",
                    Icon = IconFont.Heart,
                },
                new MenuBody
                {
                    Text = "Subscription",
                    Icon = IconFont.FileSignature,
                },
                new MenuBody
                {
                    Text = "About App",
                    Icon = IconFont.Code,
                },
            }.SetColorInMenuList();

            BodyList = bodyMenu.ToObservableCollection();
        }

        public Command ChangeSelectedItemCommand => new Command(() =>
        {
            if (!string.IsNullOrEmpty(SelectedMenu?.Text) && SelectedMenu.Text.Equals(TitleBack))
            {
                BodyList = OldBodyList.ToObservableCollection();
                return;
            }

            if (!SelectedMenu.Child.Count.Equals(0))
            {
                OldBodyList = BodyList.ToList();
                BodyList = SelectedMenu.Child.ToObservableCollection();
                SelectedMenu = new MenuBody();
            }
        });

        public Command ChangeSelectedUserItemCommand => new Command(() =>
        {
            if (!string.IsNullOrEmpty(SelectedMenuUser?.Text) && SelectedMenuUser.Text.Equals(TitleBack))
            {
                UserList = OldUserList.ToObservableCollection();
                return;
            }

            if (!SelectedMenuUser.Child.Count.Equals(0))
            {
                OldUserList = UserList.ToList();
                UserList = SelectedMenuUser.Child.ToObservableCollection();
            }
        });

        #region Properties

        public ICommand BackgroundClicked { get; private set; }

        private const double MaxOpacity = 1;
        private double _ExpandedPercentage;
        public double ExpandedPercentage
        {
            get => _ExpandedPercentage;
            set
            {
                SetProperty(ref _ExpandedPercentage, value);
                OverlayOpacity = MaxOpacity < value ? MaxOpacity : value;
            }
        }

        //private double[] _LockStates = new double[] { 0, .50, 5 };
        //public double[] LockStates
        //{
        //    get => _LockStates;
        //    set => SetProperty(ref _LockStates, value);
        //}

        private bool _IsExpanded;
        public bool IsExpanded
        {
            get => _IsExpanded;
            set => SetProperty(ref _IsExpanded, value);
        }

        private bool _IsVisible = true;
        public bool IsVisible
        {
            get => _IsVisible;
            set => SetProperty(ref _IsVisible, value);
        }

        private double _OverlayOpacity;
        public double OverlayOpacity
        {
            get => _OverlayOpacity;
            set => SetProperty(ref _OverlayOpacity, value);
        }

        public List<MenuBody> OldBodyList { get; set; }

        private ObservableCollection<MenuBody> _bodyList;
        public ObservableCollection<MenuBody> BodyList
        {
            set { SetProperty(ref _bodyList, value); }
            get { return _bodyList; }
        }

        private ObservableCollection<MenuBody> _userList;
        public ObservableCollection<MenuBody> UserList
        {
            set { SetProperty(ref _userList, value); }
            get { return _userList; }
        }

        public List<MenuBody> OldUserList { get; set; }

        private ObservableCollection<Feed> _currentFeed;
        public ObservableCollection<Feed> CurrentFeed
        {
            set { SetProperty(ref _currentFeed, value); }
            get { return _currentFeed; }
        }

        private MenuBody _selectedMenu;
        public MenuBody SelectedMenu
        {
            set { SetProperty(ref _selectedMenu, value); }
            get { return _selectedMenu; }
        }

        private MenuBody _selectedMenuUser;
        public MenuBody SelectedMenuUser
        {
            set { SetProperty(ref _selectedMenuUser, value); }
            get { return _selectedMenuUser; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
