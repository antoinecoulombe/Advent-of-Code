using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dev.adventCalendar._2020
{
    class Day20 : Day
    {
        private class Tile
        {
            private static class Transform
            {
                public enum ROTATE { d90 = 1, d180 = 2, d270 = 3 }
                private static List<List<bool>> Transpose(List<List<bool>> tile)
                {
                    var trans = tile;
                    for (int i = 0; i < tile.Count; ++i)
                        for (int j = 0; j < tile[i].Count; ++j)
                            trans[i][j] = tile[j][i];
                    return trans;
                }

                private static List<List<bool>> ReverseRow(List<List<bool>> tile)
                {
                    var trans = tile;
                    for (int i = 0; i < tile.Count; ++i)
                        for (int j = 0; j < tile[i].Count; ++j)
                            trans[i][j] = tile[i][^(j + 1)];
                    return trans;
                }

                private static List<List<bool>> ReverseColumn(List<List<bool>> tile)
                {
                    var trans = tile;
                    for (int i = 0; i < tile.Count; ++i)
                        for (int j = 0; j < tile[i].Count; ++j)
                            trans[i][j] = tile[^(i + 1)][j];
                    return trans;
                }

                private static List<List<bool>> Rotate90(List<List<bool>> tile)
                    => ReverseRow(Transpose(tile));
                private static List<List<bool>> RotateMinus90(List<List<bool>> tile)
                    => Transpose(ReverseRow(tile));
                private static List<List<bool>> Rotate180(List<List<bool>> tile)
                    => ReverseColumn(ReverseRow(tile));

                public static List<List<bool>> Rotate(List<List<bool>> tile, ROTATE rd)
                {
                    return rd switch
                    {
                        ROTATE.d90 => ReverseRow(Transpose(tile)),
                        ROTATE.d180 => ReverseColumn(ReverseRow(tile)),
                        ROTATE.d270 => Transpose(ReverseRow(tile)),
                        _ => throw new Exception("Invalid rotation."),
                    };
                }
            }

            public enum SIDE { up = 1, right = 2, down = 3, left = 4 };

            public int id;
            public List<List<bool>> pixels;
            public Dictionary<SIDE, int> sides;

            public Tile(string input)
            {
                Parse(input);
                InitSides();
            }

            public void Rotate(int rotation)
                => pixels = Transform.Rotate(pixels, (Transform.ROTATE)rotation);

            public bool IsSurrounded()
                => !sides.ContainsValue(-1);

            private void Parse(string t)
            {
                var parts = t.Split(":\n");
                id = int.Parse(parts[0].Split(' ')[1]);
                pixels = parts[1].Split("\n").ToList().Select(x =>
                    x.Select(y => y == '#').ToList()).ToList();
            }

            private void InitSides()
            {
                sides = new();
                foreach (SIDE d in Enum.GetValues(typeof(SIDE)))
                    sides.Add(d, -1);
            }

            public List<bool> GetSide(SIDE d)
            {
                return d switch
                {
                    SIDE.up => pixels[0],
                    SIDE.down => pixels[^1],
                    SIDE.left => pixels.Select(x => x.First()).ToList(),
                    SIDE.right => pixels.Select(x => x.Last()).ToList(),
                    _ => throw new Exception("Invalid direction."),
                };
            }

            private bool AreEquals(List<bool> l1, List<bool> l2)
            {
                for (int i = 0; i < l1.Count; ++i)
                    if (l1[i] != l2[i])
                        return false;
                return true;
            }

            public int GetMatchingSide(List<bool> other)
            {
                foreach (SIDE s in Enum.GetValues(typeof(SIDE)))
                    if (AreEquals(other, GetSide(s)))
                        return (int)s;
                return -1;
            }
        }

        private enum DIR { up = 1, right = 2, down = 3, left = 4 };
        List<Tile> tiles;

        private void GetTiles()
        {
            tiles = new();
            var tileStrings = GetFileText(20).Split("\n\n").ToList();
            tileStrings.ForEach(t =>
            {
                if (!string.IsNullOrEmpty(t))
                    tiles.Add(new Tile(t));
            });
        }

        #region To Refactor

        private void PlaceTiles()
        {
            foreach (Tile t in tiles)
            {
                foreach (Tile t2 in tiles)
                {
                    foreach (Tile.SIDE s in Enum.GetValues(typeof(Tile.SIDE)))
                    {
                        if (t == t2)
                            continue;

                        int match = t.GetMatchingSide(t2.GetSide(s));
                        if (match != -1)
                        {
                            int rot = (4 - match + (int)s + 2) % 4;
                            if (rot != 0)
                                t2.Rotate(rot);
                            t.sides[(Tile.SIDE)match] = t2.id;
                        }
                    }
                }
            }
        }

        private List<List<Tile>> CreatePicture()
        {
            List<List<Tile>> picture = new();
            return picture;
        }

        #endregion

        public override string ExecuteFirst()
        {
            GetTiles();
            PlaceTiles();
            var picture = CreatePicture();

            Console.WriteLine("4 sides: " + tiles.Count(x => x.sides.Count(y => y.Value != -1) == 4) + "/100");
            Console.WriteLine("3 sides: " + tiles.Count(x => x.sides.Count(y => y.Value != -1) == 3) + "/40");
            Console.WriteLine("2 sides: " + tiles.Count(x => x.sides.Count(y => y.Value != -1) == 2) + "/4");
            Console.WriteLine("1 sides (invalid): " + tiles.Count(x => x.sides.Count(y => y.Value != -1) == 1));
            Console.WriteLine("0 sides (invalid): " + tiles.Count(x => x.sides.Count(y => y.Value != -1) == 0));

            return "";
            //return (picture[0][0].id * picture[0][^1].id * picture[^1][0].id * picture[^1][^1].id).ToString();
        }

        public override string ExecuteSecond()
        {
            return "";
        }
    }
}
