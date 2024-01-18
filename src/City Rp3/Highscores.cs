class Highscores {
    Dictionary<string, int> data;

    public Highscores() {
        data = new Dictionary<string, int>();
    }

    //vraca highscore korisnika s imenom username
    public int get(string username) {
        int result;
        if (data.TryGetValue(username, out result)) return result;
        else return -1;             //neka vrijednost koju score ne moze poprimiti
    }

    //postavlja par username-highscore u rjecnik
    public void set(string username, int highscore) {
        data[username] = highscore; //prepise staru vrijednost (.Add ako se to zeli izbjeci)
    }

    //vraca rjecnik(tablicu) sa svim zabiljezenim rezultatima
    public Dictionary<string, int> getAll() {
        return data;
    }

    //cijeli rjecnik vraca u string (csv forma) 
    public string dump() {
        string result = "";
        foreach (KeyValuePair<string, int> entry in data) {
            result = result + entry.Key + "," + entry.Value + ",";
        }
        return result;
    }

    //ucitava sve podatke rjecnika iz stringa (csv forma)
    public void load(string entry) {
        bool first = true;
        string username = "";
        string highscore_string = "";
        int begin_substr = 0;
        int size = 0;
        for (int i = 0; i < entry.Length; i++) {
            if (size == 0) begin_substr = i;
            size++;
            if (entry[i] == ',' && first == true) {
                username = entry.Substring(begin_substr, size - 1);
                first = false;
                size = 0;
                continue;
            }
            if (entry[i] == ',' && first == false) {
                highscore_string = entry.Substring(begin_substr, size - 1);
                first = true;
                size = 0;
                int x = Int32.Parse(highscore_string);
                data[username] = x;
            }

        }
    }
}
