namespace City_Rp3 {

    //klasa sadrži funkcije za provjeru je li se zgrada/resurs može postaviti na odredeno mjesto 
    //sadrzi i pathfind algoritam koji sluzi da radnici, vukovi i vojnici setaju po mapi
    public static class PlacingAndPathFind {
        //vrsta zgrade dodati provjeru!!!!
        //provjerava jel moguće postaviti zgradu na koordinate (x,y)
        public static bool IsPossibleToPlace((int, int) coords, int building, Map map) {
            //provjera ako dohvatim koordinate izvan mape, podizem zastavu
            int flag = 0;

            //dimenzije mape
            int mapWidth = 20;
            int mapHeight = 20;
            int i, j;

            int x = coords.Item1;
            int y = coords.Item2;

            int width = Constants.BuildingSize(building).width;
            int height = Constants.BuildingSize(building).height;

            //gledamo je li zgrada postavljena na povrsini na kojoj treba biti
            //Mine na Iron
            //Clayworks na Water
            //Sve ostalo na travi
            int surface = Constants.Grass;
            if (building == Constants.Clayworks) {
                surface = Constants.Water;
            }

            if (building == Constants.Mine) {
                surface = Constants.Iron;
            }

            //ako postavljanjem zgrade "izletimo" izvan mape onda vrati false
            if (x + width > mapWidth || y + height > mapHeight)
                return false;

            if (x < 0 || y < 0)
                return false;

            if (x > mapWidth || y > mapHeight)
                return false;

            int tile = map.get((x, y));
            int tile2 = map.get((x, y));

            //provjeri jel se zgrada postavlja na dobru površinu
            for (i = 0; i < width; i++) {
                for (j = 0; j < height; j++) {
                    tile = map.get((x + i, y + j));
                    if (tile != surface) {
                        return false;
                    }
                }
            }

            //pocinjemo od koordinata (x - 1, y - 1) 
            x--;
            y--;

            //mora biti razmak izemđu zgrada pa ustvari gledamo duži i širi prostor
            //znaci da je sa svake strane za 1 kockicu zgrada duža odnosno šira
            width += 2;
            height += 2;

            int node1, node2;
            //sad provjeravamo rubove oko zgrade, mora biti jedan tile slobodan okolo svake zgrade/resursa
            //omogucava da se radnici mogu slobodno kretati
            for (i = 0; i < width; i++) {
                if (outOfBounds((x + i, y)) == true)
                    node1 = -1;
                else
                    node1 = map.get((x + i, y));

                if (outOfBounds((x + i, y + height - 1)) == true)
                    node2 = -1;
                else
                    node2 = map.get((x + i, y + height - 1));

                if ((node1 != -1 && node1 != Constants.Grass && node1 != surface && node1 != Constants.Wood &&
                    node1 != Constants.Stone && node1 != Constants.Iron && node1 != Constants.Clay && node1 != Constants.Water)
                    || (node2 != -1 && node2 != Constants.Grass && node2 != surface && node2 != Constants.Wood &&
                    node2 != Constants.Stone && node2 != Constants.Iron && node2 != Constants.Clay && node2 != Constants.Water))
                    return false;
            }

            for (j = 0; j < height; j++) {
                if (outOfBounds((x, y + j)) == true)
                    node1 = -1;
                else
                    node1 = map.get((x, y + j));

                if (outOfBounds((x + width - 1, y + j)) == true)
                    node2 = -1;
                else
                    node2 = map.get((x + width - 1, y + j));

                if ((node1 != -1 && node1 != Constants.Grass && node1 != surface && node1 != Constants.Wood &&
                    node1 != Constants.Stone && node1 != Constants.Iron && node1 != Constants.Clay && node1 != Constants.Water)
                    || (node2 != -1 && node2 != Constants.Grass && node2 != surface && node2 != Constants.Wood &&
                    node2 != Constants.Stone && node2 != Constants.Iron && node2 != Constants.Clay && node2 != Constants.Water))
                    return false;
            }

            return true;
        }

        static bool outOfBounds((int, int) a) {
            int x = a.Item1;
            int y = a.Item2;
            int mapLength = 20;
            int mapWidth = 20;
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapLength)
                return true;

            return false;
        }

        //klasa predstavlja ćeliju
        //drzimo informacije o susjedima, koordinatama, roditelju (od kud smo dosli) i heuristike funkcije
        public class cell {
            //parent ćelija, trebat ćemo da znamo rekonstruirat put
            public cell parent;
            public int x, y;
            bool obstacle;
            //heuristike (L - lokalno f i G - globalni f)
            public double fL, fG;
            public List<cell> neighbours;
            public bool visited;

            public cell(cell p, double _fL, double _fG, int _x, int _y) {
                parent = p;
                fL = _fL;
                fG = _fG;
                x = _x;
                y = _y;
                obstacle = false;
                visited = false;
                neighbours = new List<cell>();
            }

            public cell() { }
        };

        //izracunaj udaljenost -- heuristika koju koristimo
        public static double distance(cell a, cell b) {
            return (Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)));
        }

        public static double heuristic(cell a, cell b) {
            return distance(a, b);
        }

        //sortiraj listu za pretrazivanje po globalnom f
        public static List<cell> sort(List<cell> list) {

            list = list.OrderBy(o => o.fG).ToList();
            return list;
        }

        //A* algoritam za pronalaženje najboljeg puta.
        public static List<(int, int)> FindPath((int, int) start, (int, int) end, Map map) {
            List<(int, int)> path = new List<(int, int)>();

            int startx = start.Item1;
            int starty = start.Item2;

            int endx = end.Item1;
            int endy = end.Item2;

            //ako su točke cilja ili početka invalidne, baci exception
            if (outOfBounds((startx, starty)) == true || outOfBounds((endx, endy)) == true) {
                throw new Exception("krive koordinate starta");
            }

            //ako se na startu nalazi nešto drugo od trave, baci exception jer se tamo nesmijemo kretati
            if (map.get((startx, starty)) != Constants.Grass) {
                throw new Exception("Start koordinate nisu na travi");
            }
            int x, y;
            int i, j;

            i = endx;
            j = endy;

            //razlika između polja na kojoj je zgrada i prvog slobodnog polja za hodanje
            int dx = 0;
            int dy = 0;

            //podigni zastavu ako smo otisli izvan granica
            int flag = 0;
            int tile = map.get((startx, starty));

            //Ako je točka cilja unutar zgrade znači da je kliknuto na neku od zgrada, tada postavi nove koordinate cilja
            //igrač dolazi neposredno pored zgrade, a ne na samu zgradu:

            if (map.get((endx, endy)) != Constants.Grass) {
                //prvo namještamo x koordinatu
                if (startx > endx) {
                    while (true) {
                        if (outOfBounds((i, j))) {
                            flag = 1;
                        }
                        else
                            tile = map.get((i, j));

                        if (tile == Constants.Grass || flag == 1)
                            break;
                        dx++;
                        i++;
                    }
                }
                else if (startx < endx)
                    while (true) {
                        if (outOfBounds((i, j)))
                            flag = 1;
                        else
                            tile = map.get((i, j));

                        if (tile == Constants.Grass || flag == 1)
                            break;
                        dx--;
                        i--;
                    }

                //sad namjestimo y koordinatu

                else if (startx == endx) {
                    i = endx;
                    flag = 0;
                    if (starty > endy)
                        while (true) {
                            if (outOfBounds((i, j)))
                                flag = 1;
                            else
                                tile = map.get((i, j));

                            if (tile == Constants.Grass || flag == 1)
                                break;
                            dy++;
                            j++;
                        }
                    else if (starty < endy)
                        while (true) {
                            if (outOfBounds((i, j)))
                                flag = 1;
                            else
                                tile = map.get((i, j));

                            if (tile == Constants.Grass || flag == 1)
                                break;
                            dy--;
                            j--;
                        }
                }
            }

            //promjeni koordinate cilja na točku najbliže početnoj točki koja je trava pored zgrade
            //ona uvijek postoji jer su zgrade razmaknute uvijek za 1 ploču na mapi
            endx += dx;
            endy += dy;


            //inicijaliziraj polje ćelija
            cell[,] cells = new cell[20, 20];
            for (x = 0; x < 20; x++) {
                for (y = 0; y < 20; y++) {
                    cells[x, y] = new cell(null, double.MaxValue, double.MaxValue, x, y);
                }
            }

            //dodaj susjede za svaku ćeliju
            for (x = 0; x < 20; x++) {
                for (y = 0; y < 20; y++) {
                    //provjeri da nebi izasli iz granica
                    if (x < 19)
                        cells[x, y].neighbours.Add(cells[x + 1, y]);
                    if (x > 0)
                        cells[x, y].neighbours.Add(cells[x - 1, y]);
                    if (y < 19)
                        cells[x, y].neighbours.Add(cells[x, y + 1]);
                    if (y > 0)
                        cells[x, y].neighbours.Add(cells[x, y - 1]);
                }
            }


            cell cellStart = cells[startx, starty];
            cell cellEnd = cells[endx, endy];

            //inicijaliziraj početak
            cell current = cellStart;
            cellStart.fL = 0;
            cellStart.fG = heuristic(cellStart, cellEnd);

            //lista po kojoj pretražujemo...prvo pretražujemo one ćelije s najmanjom fG brojem
            List<cell> searchList = new List<cell>();
            searchList.Add(cellStart);

            //dok nije prazna lista ili dok nismo došli do kraja
            while (searchList.Count > 0 && current != cellEnd) {
                //prvo moramo sortirati listu po heuristici fG
                searchList = sort(searchList);

                while (searchList.Count > 0 && searchList.First().visited == true)
                    searchList.RemoveAt(0);

                if (searchList.Count == 0)
                    break;

                current = searchList.First();
                current.visited = true;

                //za svakog susjeda radimo isto:
                foreach (var neighbourcell in current.neighbours) {
                    if (neighbourcell.visited == false && map.get((neighbourcell.x, neighbourcell.y)) == Constants.Grass) {
                        searchList.Add(neighbourcell);
                    }

                    //izracunaj cost od susjeda:
                    double fNew = current.fL + distance(current, neighbourcell);

                    if (fNew < neighbourcell.fL) {
                        neighbourcell.parent = current;

                        neighbourcell.fL = fNew;

                        neighbourcell.fG = neighbourcell.fL + heuristic(neighbourcell, cellEnd);
                    }

                }
            }

            cell p = cellEnd;
            //sada kada smo našli put idi od ćelije cilja i prati njegove roditelje od kud smo došli
            //dobit ćemo u obrnutom redoslijedu putanju
            while (p.parent != null) {
                path.Add((p.x, p.y));
                p = p.parent;
            }
            path.Add((startx, starty));
            path.Reverse();

            return path;

        }


    }
}
