//Klasa Soldiers 
//
//klasa koja čuva podatke o ratnicima
//
//Soldiers() - običan crt, samo stvara objekt
//Soldiers(Soldiers e) - copy ctr
//Soldiers(string loadString, Map map) - load ctr, prima string i stvara objekt, treba map jer se path ne zapisuje u string nego računa tjeko loada
//bool Cmp(Soldiers e) - uspoređuje 2 objekta ove klase
//string dump() - vraća string sa spremanje klase u datoteku, on se daje load ctru
//void remove(int id) - briše ratnika id
//int[] getAllIds() - vraća ideve svih ratnika
//int? getByCoords((int x, int y) coords) - vraća id ratnika koji je na koordinatama coords, ako ih je više vraća jednog, ako nema nikog vraća null
//(int, int) getPos(int id) - vraća poziciju ratnika id na mapi
//void setPos((int x, int y) coords, int id) - postavlja poziciju ratnika id na coords
//(int?, int?) getHomePos(int id) - vraća koordinate doma ratnika id, dom je tamo gdje vraća resurse, ako nema dom vraća (null, null) , ne koristi se
//void setHomePos((int? x, int? y) coords, int id) - postavlja koordinate doma ratnika id, ne koristi se
//bool isHome(int id) - vraća true ako je ratnik doma, ne koristi se
//void goHome(int id) - šalje ratnika doma (postavlja mu destinaciju na doma) , ne koristi se
//int getHealth(int id) - vraća zdravlje ratnika id
//void setHealth(int id, int hp) - postavlja zdravlje ratnika id na hp, briše ga ako je <=0
//void updatePath(int id, Map map) - poziva pathfinding za ratnika id, treba objekt sa mapom
//void step(int id) - pomiče ratnika id jedan korak po pathu koji je dao updatePath
//int add((int x, int y) coords, int hp) - dodaje novog ratnika na poziciji coords, sa zdravljem h; vraća njegov id
//(int? x, int? y) getDes(int id) - varaća koordinate kamo ratnik ide
//void setDes((int? x, int? y) coords, int id) - postavlja kamo ratnik ide
//(int?, int?) getNextStep(int id)- vraća slijedeće korak ratnika, ako ga nema vraća (null, null)
//bool hasID (int id) - vraća true ako ratnik sa id postoji
//int? getTargetId(int id) - vraća id vuka kojega ratnik ganja, ili vraća null
//void setTargetId(int id, int? target) - postavlja id vuka kojeg ratnik ganja

public class Soldiers : Entities {
    private class Soldier : Entity {
        public int? target_id;
    }
    private Dictionary<int, Soldier> entList;
    public Soldiers() {
        entList = new Dictionary<int, Soldier>();

    }
    public Soldiers(Soldiers e) {
        entList = new Dictionary<int, Soldier>();
        foreach (int i in e.entList.Keys) {
            entList.Add(i, new Soldier());
            entList[i].pos_x = e.entList[i].pos_x;
            entList[i].pos_y = e.entList[i].pos_y;
            entList[i].des_x = e.entList[i].des_x;
            entList[i].des_y = e.entList[i].des_y;
            entList[i].home_x = e.entList[i].home_x;
            entList[i].home_y = e.entList[i].home_y;
            entList[i].health = e.entList[i].health;
            foreach ((int, int) coords in e.entList[i].path) entList[i].path.Add(coords);
        }
        freeID = e.freeID;

    }
    public Soldiers(string loadString, Map map) {
        entList = new Dictionary<int, Soldier>();
        string[] words = loadString.Split(',');
        for (int i = 1; i < words.Length - 1; i += 8) {
            int id = Int32.Parse(words[i]);
            entList.Add(id, new Soldier());
            entList[id].pos_x = Int32.Parse(words[i + 1]);
            entList[id].pos_y = Int32.Parse(words[i + 2]);
            if (words[i + 3] != "") entList[id].des_x = Int32.Parse(words[i + 3]);
            if (words[i + 4] != "") entList[id].des_y = Int32.Parse(words[i + 4]);
            if (words[i + 5] != "") entList[id].home_x = Int32.Parse(words[i + 5]);
            if (words[i + 6] != "") entList[id].home_y = Int32.Parse(words[i + 6]);
            entList[id].health = Int32.Parse(words[i + 7]);
            if (this.getDes(id) != (null, null)) this.updatePath(id, map);
        }
        freeID = Int32.Parse(words.Last());

    }
    public bool Cmp(Soldiers e) {
        int[] ids1 = getAllIds();
        int[] ids2 = e.getAllIds();
        if (ids1.Length != ids2.Length) return false;
        foreach (int i in ids2) {
            if (entList[i].pos_x != e.entList[i].pos_x) return false;
            if (entList[i].pos_y != e.entList[i].pos_y) return false;
            if (entList[i].des_x != e.entList[i].des_x) return false;
            if (entList[i].des_y != e.entList[i].des_y) return false;
            if (entList[i].home_x != e.entList[i].home_x) return false;
            if (entList[i].home_y != e.entList[i].home_y) return false;
            if (entList[i].health != e.entList[i].health) return false;
            for (int j = 0; j < entList[i].path.Count(); j++)
                if (entList[i].path[j] != e.entList[i].path[j]) return false;
        }
        return true;
    }
    public string dump() {
        string result = "SOLDIERS,";
        foreach (int key in entList.Keys) result +=
                key + ","
                + entList[key].pos_x + "," + entList[key].pos_y + ","
                + entList[key].des_x + "," + entList[key].des_y + ","
                + entList[key].home_x + "," + entList[key].home_y + ","
                + entList[key].health + ",";
        result += freeID;
        return result;
    }
    public void remove(int id) { //ne recikliram :(
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        entList.Remove(id);

    }
    public int[] getAllIds() {
        return entList.Keys.ToArray();
    }
    public int? getByCoords((int x, int y) coords) {
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        int? e_id = null;
        foreach (int id in getAllIds())
            if (getPos(id) == coords) e_id = id;
        return e_id;
    }
    public (int, int) getPos(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        return (entList[id].pos_x, entList[id].pos_y);
    }
    public void setPos((int x, int y) coords, int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        entList[id].pos_x = coords.x;
        entList[id].pos_y = coords.y;

    }
    public (int?, int?) getHomePos(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        return (entList[id].home_x, entList[id].home_y);
    }
    public void setHomePos((int? x, int? y) coords, int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        entList[id].home_x = coords.x;
        entList[id].home_y = coords.y;

    }
    public bool isHome(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (entList[id].home_x == null || entList[id].home_y == null) throw new Exception("home position not set");
        return (entList[id].des_x == entList[id].home_x) && (entList[id].des_y == entList[id].home_y) && !entList[id].path.Any();
    }
    public void goHome(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (entList[id].home_x == null || entList[id].home_y == null) throw new Exception("work position not set");
        entList[id].des_x = entList[id].home_x;
        entList[id].des_y = entList[id].home_y;

    }
    public int getHealth(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        return entList[id].health;
    }
    public void setHealth(int id, int hp) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (hp <= 0) remove(id);
        else entList[id].health = hp;

    }
    public void updatePath(int id, Map map) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (entList[id].des_x == null || entList[id].des_y == null) throw new Exception("destinacija nije postavljena");
        entList[id].path = City_Rp3.PlacingAndPathFind.FindPath((entList[id].pos_x, entList[id].pos_y), ((int)entList[id].des_x, (int)entList[id].des_y), map);
        entList[id].path.RemoveAt(0);

    }
    public void step(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (entList[id].path == null) throw new Exception("path not set");
        if (!entList[id].path.Any()) return;
        (int, int) move = entList[id].path.First();
        entList[id].path.RemoveAt(0);
        entList[id].pos_x = move.Item1;
        entList[id].pos_y = move.Item2;

    }
    public int add((int x, int y) coords, int hp) {
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        entList.Add(freeID, new Soldier());
        entList[freeID].pos_x = coords.x;
        entList[freeID].pos_y = coords.y;
        entList[freeID].health = hp;
        freeID++;

        return freeID - 1;
    }
    public (int? x, int? y) getDes(int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        return (entList[id].des_x, entList[id].des_y);
    }
    public void setDes((int? x, int? y) coords, int id) {
        if (!entList.ContainsKey(id)) throw new ArgumentException("non-existing id");
        if (coords.x < 0 || coords.x > 19 || coords.y < 0 || coords.y > 19) throw new ArgumentException("out of bounds");
        entList[id].des_x = coords.x;
        entList[id].des_y = coords.y;

    }
    public (int?, int?) getNextStep(int id) {
        if (!entList[id].path.Any()) return (null, null);
        return entList[id].path.First();
    }
    public int? getTargetId(int id) {
        return entList[id].target_id;
    }
    public void setTargetId(int id, int? target) {
        entList[id].target_id = target;

    }
    public bool hasID(int id) {
        return entList.ContainsKey(id);
    }

}
