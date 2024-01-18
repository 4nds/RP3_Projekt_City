// Klasa Window
//
// forma koja prikazuje ostale forme (ekrane) i izbornike
//
// Dictionary<string, Form> Screens - riječnik ekrana (ključevi su imena ekrana)
// Dictionary<string, Menu> Menus - riječnik izbornika (ključevi su imena izbornika)
// Window() - konstruktor
// void addScreen(string screen_name, Form screen) - metoda dodaje formu screen kao ekran pod imenom screen_name
// public void showScreen(string screen_name) - metoda koja pokazuje ekran identificiran s njegovim imenom
// void addMenu(string menu_name, Menu menu) - moteda dodaje izbornik menu pod imenom menu_name
// void showMenu(string menu_name, Point location) - metoda koja pokazuje izbornik na lokaciji location
//     identificiran s njegovim imenom
// void showMenu(string menu_name, Point location, (Point, Point) do_not_cover) - metoda koja pokazuje
//     izbornik na lokaciji location identificiran s njegovim imenom,
//     do_not_cover predstavlja pravokutnik kojeg meni neće prekrit
// void hideMenu(string menu_name) - metoda koja skriva izbornik identificiran s njegovim imenom
// bool isMenuVisible(string menu_name) - metoda koja vraće je li vidljiv izbornik identificiran s njegovim imenom

namespace City_Rp3 {
    public partial class Window : Form {
        private Form _current_screen;
        private Menu _last_shown_menu;
        private readonly Dictionary<string, Form> _screens;
        private readonly Dictionary<string, Menu> _menus;

        public event EventHandler<string> ScreenShown;
        public event EventHandler<string> MenuShown;
        public event EventHandler<string> MenuHidden;

        //public event EventHandler<ShowMenuEventArgs>? ScreenShown;

        public Dictionary<string, Form> Screens {
            get => _screens;
        }

        public Dictionary<string, Menu> Menus {
            get => _menus;
        }

        public Window() {
            InitializeComponent();

            _current_screen = new();
            _last_shown_menu = new();
            _screens = new();
            _menus = new();

            //_globals = globlas;
        }

        public void addScreen(string screen_name, Form screen) {
            _screens[screen_name] = screen;
            //_screens.Add(screen_name, screen);
            screen.MdiParent = this;
            screen.FormBorderStyle = FormBorderStyle.None;
            screen.StartPosition = FormStartPosition.CenterScreen;
        }

        public void showScreen(string screen_name) {
            hideCurrentScreen();
            _current_screen = _screens[screen_name];
            showCurrentScreen();
            ScreenShown?.Invoke(this, screen_name);
        }

        public void addMenu(string menu_name, Menu menu) {
            _menus[menu_name] = menu;
            //_menus.Add(menu_name, menu);
        }

        public void showMenu(string menu_name, Point location) {
            _menus[menu_name].show(location);
            _last_shown_menu = _menus[menu_name];
            MenuShown?.Invoke(this, menu_name);
        }

        public void showMenu(string menu_name, Point location,
            (Point, Point) do_not_cover) {
            _menus[menu_name].show(location, do_not_cover);
            _last_shown_menu = _menus[menu_name];
            MenuShown?.Invoke(this, menu_name);
        }

        public void hideMenu(string menu_name) {
            _menus[menu_name].hide();
            MenuHidden?.Invoke(this, menu_name);
        }

        public bool isMenuVisible(string menu_name) {
            return _menus[menu_name].Visible;
        }

        private void showCurrentScreen() {
            _current_screen.BringToFront();
            _current_screen.Show();
        }

        private void hideCurrentScreen() {
            _current_screen.Hide();
        }

        private void window_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = !onClose();
        }

        private bool onClose() {
            if (_current_screen == _screens["game"]) {
                if (isMenuVisible("game")) {
                    return false;
                }
                Size screen_size = _screens["game"].ClientSize;
                Size game_menu_size = _menus["game"].Size;
                int left = (screen_size.Width - game_menu_size.Width) / 2;
                int top = (screen_size.Height - game_menu_size.Height) / 2;
                foreach (Menu menu in _menus.Values) {
                    menu.hide();
                }
                showMenu("game", new Point(left, top));
                return false;
            }
            else {
                foreach (Form screen in _screens.Values) {
                    screen.Close();
                }
                return true;
            }
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '') {
                if (_last_shown_menu.Visible) {
                    _last_shown_menu.hide();
                }
                else {
                    onClose();
                    Close();
                }
                e.Handled = true;
            }


        }

    }
}
