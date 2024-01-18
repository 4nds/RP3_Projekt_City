//Klasa Map
//
//čuva stanje mape u svakom trenutku, pruža mogućnost da se mapa loada iz stringa i dumpa u string
//
//Map(string loadString) - konstruktor koji prima dump i konstruira map objekt iz toga dumpa. U slučaju lošeg load stringa throwa exception.
//string dump() - vraća string koji sadrži stanje mape za spremanje igre
//int get((int x, int y)) - vraća šifru zgrade/resursa na koordinatama x,y
//void set((int x, int y), int code) - postavlja polje x,y na prirodni resurs code. Baca ArgumentException ako nije uspješno. Bacit će ArgumenExeption ako je code zgrada ili kod za banku.
//      (u obzir dolaze šifre 0-5), za zgrade koristi donju metodu
//void build((int x, int y), int building) - gradi zgradu iz building (po šifri) na x,y. Za zgrade većih dimenzija x,y je gornji lijevi rub.
//     Baca ArgumentException ako nije uspješno. Ne provjerava je li mjesto gradnje validno.
//

public class Map {
    private int[,] fields;
    public Map() {
        fields = new int[20, 20];
    }
    public Map(string loadString) {
        fields = new int[20, 20];
        string[] exploded = loadString.Split(',');

        if (exploded.Length != 400) throw new ArgumentException("Invalid load string");
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++) {
                int number;
                if (!int.TryParse(exploded[i * 20 + j], out number)) throw new ArgumentException("Invalid load string");
                fields[i, j] = number;
            }
    }
    public Map(Map e) {
        fields = new int[20, 20];
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
                fields[i, j] = e.fields[i, j];

    }
    public bool Cmp(Map e) {
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
                if (fields[i, j] != e.fields[i, j]) return false;
        return true;
    }
    public string dump() {
        string res = "";
        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++) {
                res += fields[i, j];
                if (i < 19 || j < 19) res += ",";
            }
        return res;
    }
    public int get((int? x, int? y) coords) {
        if (coords.x == null || coords.y == null) return -1;
        if (coords.x >= 0 && coords.y >= 0 && coords.x < 20 && coords.y < 20) {
            return fields[(int)coords.x, (int)coords.y];
        }
        else {
            throw new ArgumentException("out of bounds");
        }
    }
    public void set((int x, int y) coords, int code) {
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        if (code >= 0 && code < 6 && code != 2) {
            fields[coords.x, coords.y] = code;

        }
        else throw new ArgumentException("wrong field code");
    }
    public void build((int x, int y) coords, int building) {
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        switch (building) {
            case Constants.MainBuilding:
                fields[coords.x, coords.y] = Constants.MainBuilding11;
                fields[coords.x + 1, coords.y] = Constants.MainBuilding12;
                fields[coords.x, coords.y + 1] = Constants.MainBuilding21;
                fields[coords.x + 1, coords.y + 1] = Constants.MainBuilding22;
                break;
            case Constants.Farm:
                fields[coords.x, coords.y] = Constants.Farm11;
                fields[coords.x + 1, coords.y] = Constants.Farm12;
                fields[coords.x + 2, coords.y] = Constants.Farm13;
                fields[coords.x, coords.y + 1] = Constants.Farm21;
                fields[coords.x + 1, coords.y + 1] = Constants.Farm22;
                fields[coords.x + 2, coords.y + 1] = Constants.Farm23;
                fields[coords.x, coords.y + 2] = Constants.Farm31;
                fields[coords.x + 1, coords.y + 2] = Constants.Farm32;
                fields[coords.x + 2, coords.y + 2] = Constants.Farm33;
                break;
            case Constants.Mine:
                fields[coords.x, coords.y] = Constants.Mine11;
                fields[coords.x + 1, coords.y] = Constants.Mine12;
                fields[coords.x, coords.y + 1] = Constants.Mine21;
                fields[coords.x + 1, coords.y + 1] = Constants.Mine22;
                break;
            case Constants.Clayworks:
                fields[coords.x, coords.y] = Constants.Clayworks11;
                fields[coords.x + 1, coords.y] = Constants.Clayworks12;
                break;
            case Constants.Wonder:
                fields[coords.x, coords.y] = Constants.Wonder11;
                fields[coords.x + 1, coords.y] = Constants.Wonder12;
                fields[coords.x + 2, coords.y] = Constants.Wonder13;
                fields[coords.x, coords.y + 1] = Constants.Wonder21;
                fields[coords.x + 1, coords.y + 1] = Constants.Wonder22;
                fields[coords.x + 2, coords.y + 1] = Constants.Wonder23;
                fields[coords.x, coords.y + 2] = Constants.Wonder31;
                fields[coords.x + 1, coords.y + 2] = Constants.Wonder32;
                fields[coords.x + 2, coords.y + 2] = Constants.Wonder33;
                break;
            case Constants.Stockpile:
                fields[coords.x, coords.y] = Constants.Stockpile;
                break;
            case Constants.Smithy:
                fields[coords.x, coords.y] = Constants.Smithy11;
                fields[coords.x + 1, coords.y] = Constants.Smithy12;
                break;
            case Constants.Armory:
                fields[coords.x, coords.y] = Constants.Armory11;
                fields[coords.x + 1, coords.y] = Constants.Armory12;
                break;
            default:
                throw new ArgumentException("not a valid building id");
        }

    }
}
