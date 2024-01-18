// Klasa MainBuildingMenu 
//
// klasa za izbornik glavne zgrade
//
// MainBuildingMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti

namespace City_Rp3 {
    public class MainBuildingMenu : Menu {
        public event EventHandler<ShowMenuEventArgs>? ShowBuildings;
        public event EventHandler<ShowMenuEventArgs>? ShowWorkers;

        public MainBuildingMenu(Form screen, bool draggable = true) {
            title = "Main Building";
            _screen = screen;
            _Container = new MenuContainer(this, draggable) {
                Size = new Size(300, 270),
            };
            _content = new MainBuildingMenuContent(this);
            MainBuildingMenuContent content =
                (MainBuildingMenuContent)_content;
            content.ShowBuildings +=
                (sender, e) => ShowBuildings?.Invoke(this,
                    new ShowMenuEventArgs(last_location, last_do_not_cover));
            content.ShowWorkers +=
                (sender, e) => ShowWorkers?.Invoke(this,
                    new ShowMenuEventArgs(last_location, last_do_not_cover));
            addContentToContainer();
        }

        protected override void onShow() {
            ((MainBuildingMenuContent)_content).updateResources();
        }

    }
}
