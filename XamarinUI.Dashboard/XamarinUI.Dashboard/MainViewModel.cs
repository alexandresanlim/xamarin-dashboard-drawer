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

namespace XamarinUI.Dashboard
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string TitleBack { get { return "Back"; } }

        public MainViewModel()
        {
            BackgroundClicked = new Command(() => IsExpanded = false);

            UserList = new ObservableCollection<Menu>
            {
                new Menu
                {
                    Title = "Address",
                    Icon = IconFont.MapMarkedAlt,
                },
                new Menu
                {
                    Title = "Credit Cards",
                    Icon = IconFont.CreditCard,
                },
                new Menu
                {
                    Title = "Favorites",
                    Icon = IconFont.Star
                }.AddChild(new ObservableCollection<Menu>
                    {
                        new Menu
                        {
                            Title = "Products",
                            Icon = IconFont.Guitar
                        },
                        new Menu
                        {
                            Title = "Services",
                            Icon = IconFont.Taxi
                        },
                    }),
                new Menu
                {
                    Title = "Wallets",
                    Icon = IconFont.Wallet,
                },
                new Menu
                {
                    Title = "Logout",
                    Icon = IconFont.SignOutAlt,
                },
            };

            PlanList = new ObservableCollection<Menu>
            {
                new Menu
                {
                    Title = "Categories",
                    Icon = IconFont.ShoppingBag
                }.AddChild(new ObservableCollection<Menu>
                    {
                        new Menu
                        {
                            Title = "Japonese",
                            //Icon = IconFont.List
                        },
                        new Menu
                        {
                            Title = "Italian",
                            //Icon = IconFont.List
                        },
                        new Menu
                        {
                            Title = "Brazilian",
                            //Icon = IconFont.List
                        }
                    }.SetColorInMenuList()),
                new Menu
                {
                    Title = "Near",
                    Icon = IconFont.MapMarkedAlt,
                },
                new Menu
                {
                    Title = "Famous",
                    Icon = IconFont.Star,
                },
                new Menu
                {
                    Title = "Promotion",
                    Icon = IconFont.Heart,
                },
                new Menu
                {
                    Title = "Subscription",
                    Icon = IconFont.FileSignature,
                },
                new Menu
                {
                    Title = "About App",
                    Icon = IconFont.Code,
                },
            }.SetColorInMenuList();

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

        public Command ChangeSelectedItemCommand => new Command(() =>
        {
            if (!string.IsNullOrEmpty(SelectedMenu?.Title) && SelectedMenu.Title.Equals(TitleBack))
            {
                PlanList = OldPlanList;
                return;
            }

            if (!SelectedMenu.Child.Count.Equals(0))
            {
                OldPlanList = PlanList;
                PlanList = SelectedMenu.Child;
                SelectedMenu = new Menu();
            }
        });

        public Command ChangeSelectedUserItemCommand => new Command(() =>
        {
            if (!string.IsNullOrEmpty(SelectedMenuUser?.Title) && SelectedMenuUser.Title.Equals(TitleBack))
            {
                UserList = OldUserList;
                return;
            }

            if (!SelectedMenuUser.Child.Count.Equals(0))
            {
                OldUserList = UserList;
                UserList = SelectedMenuUser.Child;
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

        private ObservableCollection<Menu> _OldPlanList;
        public ObservableCollection<Menu> OldPlanList
        {
            set { SetProperty(ref _OldPlanList, value); }
            get { return _OldPlanList; }
        }

        private ObservableCollection<Menu> _planList;
        public ObservableCollection<Menu> PlanList
        {
            set { SetProperty(ref _planList, value); }
            get { return _planList; }
        }

        private ObservableCollection<Menu> _userList;
        public ObservableCollection<Menu> UserList
        {
            set { SetProperty(ref _userList, value); }
            get { return _userList; }
        }

        private ObservableCollection<Menu> _oldUserList;
        public ObservableCollection<Menu> OldUserList
        {
            set { SetProperty(ref _oldUserList, value); }
            get { return _oldUserList; }
        }

        private ObservableCollection<Feed> _currentFeed;
        public ObservableCollection<Feed> CurrentFeed
        {
            set { SetProperty(ref _currentFeed, value); }
            get { return _currentFeed; }
        }

        private Menu _selectedMenu;
        public Menu SelectedMenu
        {
            set { SetProperty(ref _selectedMenu, value); }
            get { return _selectedMenu; }
        }

        private Menu _selectedMenuUser;
        public Menu SelectedMenuUser
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

    public class Menu : View
    {
        public Menu()
        {
            Child = new ObservableCollection<Menu>();
        }

        public string Title { get; set; }

        public string Icon { get; set; }

        public ObservableCollection<Menu> Child { get; private set; }

        public Menu AddChild(ObservableCollection<Menu> childrens)
        {
            var back = new Menu
            {
                Title = "Back",
                Icon = IconFont.AngleLeft,
                BackgroundColor = Color.FromHex("#bdc3c7")
            };

            childrens.Insert(0, back);

            this.Child = childrens.ToObservableCollection();

            return this;
        }
    }

    public class Feed
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUri { get; set; }

        public string AuthorImage { get; set; }

        public string Description { get; set; }
    }
}
