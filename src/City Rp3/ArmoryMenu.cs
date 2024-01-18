// Klasa ArmoryMenu 
//
// klasa za izbornik oružarnice
//
// ArmoryMenu(Form screen, bool draggable = true) - konstruktor koji uzima formu screen na kojoj se prikazuje,
//     argumnet draggable govori je li se izbornik može povlačiti



//Hello world!
//Ivona

namespace City_Rp3 {
    public class ArmoryMenu : Menu {
        public ArmoryMenu(Form screen, bool draggable = true) {
            title = "Aarmory";
            _screen = screen;
            _content = new ArmoryMenuContent(this);
            _Container = new MenuContainer(this, draggable) {
                //Size = new Size(225, 450),
                Size = new Size(_content.Size.Width, _content.Size.Height + 40),
            };
            ((ArmoryMenuContent)_content).PropertyChanged +=
                (sender, e) => receiveChange(sender, e);
            /*
            ((AarmoryMenuContent)_content).onUpgradeAttack +=
                (sender, e) => resources.upgradeAttack();
            ((AarmoryMenuContent)_content).onUpgradeDefense +=
                (sender, e) => resources.UpgradeDefense();
            */
            addContentToContainer();
        }

        protected override void onShow() {
            ((ArmoryMenuContent)_content).updateLabelsAndButton();
        }
    }
}
