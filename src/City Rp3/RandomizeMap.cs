public static class Randomize {

    //metoda za randomizaciju mape
    public static string RandomizeMap() {
        int[,] map = new int[20, 20];

        //koordinate glavne zgrade
        map[9, 9] = Constants.MainBuilding11;
        map[9, 10] = Constants.MainBuilding12;
        map[10, 9] = Constants.MainBuilding21;
        map[10, 10] = Constants.MainBuilding22;

        int x_coord, y_coord;
        int x_zeljezo, y_zeljezo;
        int x_voda, y_voda;
        int random_number;
        int i, j, p, k, l;
        Random r = new Random();


        //nuzan uvjet zeljeza 2x2 ploca
        x_coord = r.Next(1, 6);
        y_coord = r.Next(1, 16);
        x_zeljezo = x_coord;
        y_zeljezo = y_coord;
        map[x_coord, y_coord] = Constants.Iron;
        map[x_coord + 1, y_coord] = Constants.Iron;
        map[x_coord, y_coord + 1] = Constants.Iron;
        map[x_coord + 1, y_coord + 1] = Constants.Iron;

        map[x_coord - 1, y_coord - 1] = -1;
        map[x_coord - 1, y_coord] = -1;
        map[x_coord - 1, y_coord + 1] = -1;
        map[x_coord - 1, y_coord + 2] = -1;
        map[x_coord, y_coord - 1] = -1;
        map[x_coord, y_coord + 2] = -1;
        map[x_coord + 1, y_coord - 1] = -1;
        map[x_coord + 1, y_coord + 2] = -1;
        map[x_coord + 2, y_coord - 1] = -1;
        map[x_coord + 2, y_coord] = -1;
        map[x_coord + 2, y_coord + 1] = -1;
        map[x_coord + 2, y_coord + 2] = -1;


        //nuzan uvjet vode 2x1 ploca
        x_coord = r.Next(12, 17);
        y_coord = r.Next(1, 17);
        x_voda = x_coord;
        y_voda = y_coord;
        map[x_coord, y_coord] = Constants.Water;
        map[x_coord + 1, y_coord] = Constants.Water;
        map[x_coord, y_coord + 1] = Constants.Water;

        map[x_coord - 1, y_coord - 1] = -1;
        map[x_coord - 1, y_coord] = -1;
        map[x_coord - 1, y_coord + 1] = -1;
        map[x_coord - 1, y_coord + 2] = -1;
        map[x_coord, y_coord - 1] = -1;
        map[x_coord, y_coord + 2] = -1;
        map[x_coord + 1, y_coord - 1] = -1;
        map[x_coord + 1, y_coord + 1] = -1;
        map[x_coord + 1, y_coord + 2] = -1;
        map[x_coord + 2, y_coord - 1] = -1;
        map[x_coord + 2, y_coord] = -1;
        map[x_coord + 2, y_coord + 1] = -1;


        //za svaki resurs nasumicno se odabiru plocice na koje se stavlja
        //pazeci da se ne "pregazi" glavna zgrada i da ne budu preblizu 2x2 ploce zeljeza i 2x1 ploce vode
        for (i = 1; i < 6; i++) {
            if (i == 2) continue;

            int pom = r.Next(3, 5) * 2;

            for (j = 0; j < pom; j++) {
                for (p = 1; p <= 5; p++) {
                    x_coord = r.Next(19);
                    y_coord = r.Next(19);
                    if (map[x_coord, y_coord] == 0 && x_coord + 1 != 9 && x_coord + 1 != 10 && x_coord - 1 != 9 && x_coord - 1 != 10
                                && y_coord + 1 != 9 && y_coord + 1 != 10 && y_coord - 1 != 9 && y_coord - 1 != 10) {
                        map[x_coord, y_coord] = i;
                        random_number = r.Next(1, 5);
                        if (random_number != 1) {
                            if (x_coord + 1 <= 19 && x_coord - 1 >= 0 && y_coord + 1 <= 19 && y_coord - 1 >= 0
                                && x_coord + 1 != 9 && x_coord + 1 != 10 && x_coord - 1 != 9 && x_coord - 1 != 10
                                && y_coord + 1 != 9 && y_coord + 1 != 10 && y_coord - 1 != 9 && y_coord - 1 != 10
                                && map[x_coord + 1, y_coord + 1] != -1 && map[x_coord + 1, y_coord - 1] != -1
                                && map[x_coord - 1, y_coord + 1] != -1 && map[x_coord - 1, y_coord - 1] != -1) {
                                random_number = r.Next(1, 8);
                                switch (random_number) {
                                    case 1:
                                        map[x_coord - 1, y_coord - 1] = i;
                                        j++;
                                        break;
                                    case 2:
                                        map[x_coord - 1, y_coord] = i;
                                        j++;
                                        break;
                                    case 3:
                                        map[x_coord - 1, y_coord + 1] = i;
                                        j++;
                                        break;
                                    case 4:
                                        map[x_coord, y_coord - 1] = i;
                                        j++;
                                        break;
                                    case 5:
                                        map[x_coord, y_coord + 1] = i;
                                        j++;
                                        break;
                                    case 6:
                                        map[x_coord + 1, y_coord - 1] = i;
                                        j++;
                                        break;
                                    case 7:
                                        map[x_coord + 1, y_coord] = i;
                                        j++;
                                        break;
                                    case 8:
                                        map[x_coord + 1, y_coord + 1] = i;
                                        j++;
                                        break;
                                }
                            }
                        }

                        break;
                    }
                }

                //ako se nakon 5 pokusaja ne odabere dopustiva plocica
                //trazi se prva slobodna (po redu)
                if (p == 6) {
                    for (k = 0; k < 20; k++) {
                        for (l = 0; l < 20; l++) {
                            if (map[k, l] == 0) {
                                map[k, l] = i;
                                break;
                            }
                        }
                        if (l != 20) break;
                    }

                }


            }
        }

        map[x_zeljezo - 1, y_zeljezo - 1] = 0;
        map[x_zeljezo - 1, y_zeljezo] = 0;
        map[x_zeljezo - 1, y_zeljezo + 1] = 0;
        map[x_zeljezo - 1, y_zeljezo + 2] = 0;
        map[x_zeljezo, y_zeljezo - 1] = 0;
        map[x_zeljezo, y_zeljezo + 2] = 0;
        map[x_zeljezo + 1, y_zeljezo - 1] = 0;
        map[x_zeljezo + 1, y_zeljezo + 2] = 0;
        map[x_zeljezo + 2, y_zeljezo - 1] = 0;
        map[x_zeljezo + 2, y_zeljezo] = 0;
        map[x_zeljezo + 2, y_zeljezo + 1] = 0;
        map[x_zeljezo + 2, y_zeljezo + 2] = 0;

        map[x_voda - 1, y_voda - 1] = 0;
        map[x_voda - 1, y_voda] = 0;
        map[x_voda - 1, y_voda + 1] = 0;
        map[x_voda - 1, y_voda + 2] = 0;
        map[x_voda, y_voda - 1] = 0;
        map[x_voda, y_voda + 2] = 0;
        map[x_voda + 1, y_voda - 1] = 0;
        map[x_voda + 1, y_voda + 1] = 0;
        map[x_voda + 1, y_voda + 2] = 0;
        map[x_voda + 2, y_voda - 1] = 0;
        map[x_voda + 2, y_voda] = 0;
        map[x_voda + 2, y_voda + 1] = 0;


        //matrica intova koja predstavlja mapu pretvara se u string csv formata
        string s = "";
        for (k = 0; k < 20; k++) {
            for (l = 0; l < 20; l++) {
                s = s + map[l, k] + ",";
            }

        }
        string s_ = s.Remove(s.Length - 1, 1);
        return s_;
    }

}

