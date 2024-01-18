// Klasa MenuContainer 
//
// klasa za okvir menija, ima naslovnu traku i gumb za gašenje menija
//
// MenuContainer(Menu menu, bool draggable) - konstruktor koji uzima meni kao roditelja,
//     argumnet draggable govori je li se izbornik može povlačiti
// void show(Point location, (Point, Point) do_not_cover) - metoda za pokazivanje okvira menija na zadanoj lokaciji,
//     do_not_cover predstavlja pravokutnik kojeg okvir menija neće prekrit
// void hide() - metoda koja skriva okvir menija

namespace City_Rp3 {
    public interface IMenuContainer {
        public int Title_bar_panel_height { get; }

        public void show(Point location, (Point, Point) do_not_cover);

        public void hide();
    }

    internal partial class MenuContainer : UserControl, IMenuContainer {
        private const int MARGIN_WIDTH = 2;

        private readonly Menu _menu;
        private readonly bool _draggable;
        private bool _dragging;
        private Point _drag_start;

        private Form _Parent {
            get => _menu._Screen;
        }

        public int Title_bar_panel_height {
            get => title_bar_panel.Height;
        }

        public MenuContainer(Menu menu, bool draggable) {
            InitializeComponent();

            _menu = menu;
            _draggable = draggable;
            _dragging = false;
            if (_draggable) {
                title_bar_panel.Cursor = Cursors.Hand;
                close_button.Cursor = Cursors.Default;
            }
            title_label.Text = _menu.title;
        }

        public void show(Point location, (Point, Point) do_not_cover) {
            int parent_top = MARGIN_WIDTH;
            int parent_left = MARGIN_WIDTH;
            int parent_bottom = _Parent.ClientSize.Height - MARGIN_WIDTH;
            int parent_right = _Parent.ClientSize.Width - MARGIN_WIDTH;
            (Point dnc_top_left, Point dnc_bottom_right) = do_not_cover;
            int dnc_top = Math.Clamp(dnc_top_left.Y, parent_top, parent_bottom);
            int dnc_left = Math.Clamp(dnc_top_left.X, parent_left, parent_right);
            int dnc_bottom = Math.Clamp(dnc_bottom_right.Y, parent_top, parent_bottom);
            int dnc_right = Math.Clamp(dnc_bottom_right.X, parent_left, parent_right);

            int left = Math.Clamp(location.X, parent_left, parent_right - Width);
            if (dnc_right > left && dnc_left < left + Width) {
                left = parent_right - dnc_right > Width ?
                    dnc_right : dnc_left - Width - MARGIN_WIDTH;
            }

            int top = Math.Clamp(location.Y, parent_top, parent_bottom - Height);
            if (dnc_bottom > top && dnc_top < top + Height) {
                top = parent_top - dnc_top > Height ?
                    dnc_bottom : dnc_top - Height - MARGIN_WIDTH;
            }

            Left = Math.Clamp(left, parent_left, parent_right - Width);
            Top = Math.Clamp(top, parent_top, parent_bottom - Height);

            if (!_Parent.Controls.Contains(this)) {
                _Parent.Controls.Add(this);
            }
            BringToFront();
            //_parent.Controls.SetChildIndex(this, 0);
        }

        public void hide() {
            if (_Parent.Controls.Contains(this)) {
                _Parent.Controls.Remove(this);
            }
        }

        private void dragStart(Point location) {
            _dragging = true;
            _drag_start = new Point(location.X, location.Y);
            //((Control) sender).Capture = true;
        }

        private void drag(Point location) {
            if (_dragging) {
                Left = Math.Clamp(location.X + Left - _drag_start.X, MARGIN_WIDTH,
                    _Parent.ClientSize.Width - Width - MARGIN_WIDTH);
                Top = Math.Clamp(location.Y + Top - _drag_start.Y, MARGIN_WIDTH,
                    _Parent.ClientSize.Height - Height - MARGIN_WIDTH);
            }
        }

        private void dragEnd() {
            _dragging = false;
            //((Control) sender).Capture = false;
        }

        private void title_bar_panel_MouseDown(object sender, MouseEventArgs e) {
            if (_draggable) {
                dragStart(new Point(e.X, e.Y));
            }
        }

        private void title_label_MouseDown(object sender, MouseEventArgs e) {
            if (_draggable) {
                dragStart(new Point(e.X, e.Y));
            }
        }

        private void title_bar_panel_MouseMove(object sender, MouseEventArgs e) {
            if (_draggable) {
                drag(new Point(e.X, e.Y));
            }
        }

        private void title_label_MouseMove(object sender, MouseEventArgs e) {
            if (_draggable) {
                drag(new Point(e.X, e.Y));
            }
        }

        private void title_bar_panel_MouseUp(object sender, MouseEventArgs e) {
            if (_draggable) {
                dragEnd();
            }
        }

        private void title_label_MouseUp(object sender, MouseEventArgs e) {
            if (_draggable) {
                dragEnd();
            }
        }

        private void close_button_MouseHover(object sender, EventArgs e) {
            close_button.ForeColor = Color.Red;
        }

        private void close_button_MouseLeave(object sender, EventArgs e) {
            close_button.ForeColor = SystemColors.ControlText;
        }

        private void close_button_Click(object sender, EventArgs e) {
            _menu.hide();
        }
    }
}
