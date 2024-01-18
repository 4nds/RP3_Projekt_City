// Klasa GameMenu 
//
// klasa za izbornik igre
//
// GameMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti

namespace City_Rp3 {
    public class GameMenu : Menu {
        public event EventHandler<EventArgs>? Quit;
        public event EventHandler<EventArgs>? SaveGame;
        public event EventHandler<EventArgs>? ResumeGame;

        public GameMenu(Form screen, bool draggable = true) {
            _screen = screen;
            _Container = new MenuContainer(this, draggable);
            _content = new GameMenuContent(this);
            ((GameMenuContent)_content).Quit +=
                (sender, e) => Quit?.Invoke(this, EventArgs.Empty);
            ((GameMenuContent)_content).SaveGame +=
                (sender, e) => SaveGame?.Invoke(this, EventArgs.Empty);
            addContentToContainer();
        }

        protected override void onHide() {
            ResumeGame?.Invoke(this, EventArgs.Empty);
        }



    }
}
