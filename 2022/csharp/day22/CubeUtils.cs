﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day22
{
    public static partial class CubeUtils
    {
        public static Cube GetCube(bool test=false)
        {
            var cube = test ? CreateTestCube() : CreateCube();
            var path = test ? GetTestPath(cube) : GetPath(cube);
            cube.Path = path;
            return cube;

        }

        public static char[,] CreateMap(string mapStr, long width, long height, 
            long colStart = 0, long rowStart = 0, bool colUp = true, bool rowUp = true, bool flipped = true)
        {
            var map = new char[height, width];
            var col = colStart;
            var row = rowStart;
            var lines = mapStr.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                if (line.Count() == 0)
                    continue;
                var l = line.Trim();
                foreach (char c in l)
                {
                    map[row, col] = c;

                    col = colUp ? col + 1 : col - 1;
                }

                col = colStart;
                row = rowUp ? row + 1 : row - 1;
            }

            return map;
        }


        public static List<CubeCommand> GetTestPath(Cube cube)
        {
            return CreatePath("10R5L5R10L4R5L5", cube);
        }

        public static List<CubeCommand> GetPath(Cube cube)
        {
            return CreatePath("20R6R32L32L33R46L38R16R20R35L46R18L41R35R6L45R23R33R17R35L37R36R30L12R42R45R31L23L1R17R37L27R27L47L33R15R20R46L24L26R35L2R4R7L12L20L23L12L36L48R18R34R25L6L17R10L32L46L41L14R50L44R10R44R41L26L35L29L33L33L45R15L34R29R19L21R26R36L1R6L19L17L18L37R16R50L20R32R30L17L12L47L13L40R41L17R29R6L20R31L50R25R7R6L27L13L31R29R4R26R44R16R34R31L6L46L19L47R13L47R29R38L35R6R11L25L50L7R38L42R37R30L21L31L22L15R28L29L38R29L46R2L39L39R43L49R7R23L22R6R35L48L43R45R37R30R43L28L13R45R21R37R15L10L13L31R8R16R20L43R35R12R8R36L36R11R4R13R45R13L3L3R48L22L28R3R33R45R18L37L48L12L39R17R31L47R27L27R1L21L25R34R19R44R40R9R32L29R19L24L23R24L39R39L4L39R8R28L26L45L2L2R8L23L16L5L47R7R15L46L26R18L43R33L41L9L37L19L26L1L2R29L11R27L41R3L23R48R21R35R27R39L23R3L40L30R13L24L5R43R17R18L34R30R1R13R44L11L1R11R32L50R5L27L50L12L42L47L37R34R25L40L39R20L40L34L8L33R7R49R39L12L10R6R50R25R16R11L38R46L21R12L5R36R8R26L22R9R1R31R42L44L12R26L48L32R28R42R19R8R43R30R44R49R17L3L36R50R17L41L11L29L6R30L33R34L21L24R21R21R22L22R21L21L45L24L29L8R20R37R26L40R40R24R13L10R29L42L12R18R33R35R33R38L38L18R34R30L50L48R49L19L31L11L17R1L44R4L19R22L14R7R32L31R8L38L33L23R41R18R44L13R41R39R36R17L38R45R25R28R23L2R17R2R16L42L29R20L48L29R42R47R45R2R10L6L42R39R43R50L49R19R41L7L32R25R1L49R37R38L44R21R31L33R10L17R36R50R16L41R28L22L8R27L14L24R37R42R5R23L8R20R13R27L17R37L28L14L4R28L18L29R43L17R31R5R19L43R29R13L15R37R19L47L17R22R36L36L39R4L49L47R2L10R26L24L12L48R27R8L37R10R16R16L38L38R9R43R27L35L3R21R19R37R15L48L28R45R43R29R49R49R26R25R5L3R50L13L33R43L44R45L22R13R29R40L22L27L7L26L14L32L14L2L21R36R22R6L41L34R8L4L29L34R31R35L14R3L17L5R47R3R30R3R9L33R48R22R43R14R16R9R23L32L30L11L16L21R29L3R40L18L22L13L48L46R38R37L14L8R35L13R47R15L43L26R18R32R42L10L43L49L26L43L38R20R44L32L23R50L32L5L49L11R45L17L32L35R18R8L12L2R13L11R20R9L32L12R7R22L9L45L9L49L42R19L26L16L2R49R9R4L36R50R2R2R7R5R22R21R5R39L33L6R44L9L38L18L44R5R44R25L42R33R39R49R11R13L38L35R46L18L37R44R5R6L25L23R30R21L41R49L31R36R31L44R6L32R4R39R27R10L47R38R2R31L16R27L9L6R23R3L16R7L15R35L29R35R25R22R11L13R29L23R20L45R12R19L21R20R13R12R10L17L30L18L8R3L47L31L1R6L13R46R20L3L19R30L9R23L4L46R16R35L37L36L50L35L16R2R25L35R27R16R37R32L21L17R36L24L18R27R46R37L6L32L9L27L17L17R46L23R20R45L37R15R38L26L23L38L14R35L8L35R7R14L19L31L31L44L35R8L4R2R33R45L24L31L21R7R16R31L49R9R24R26R9R40L43L37L49R12R19R44R8R5L19L27L42L14L48L28L50R49L1R37L16L4R27R32R34L21L15R24L3R41L50R37L49L46L38R44L40L8L8L19L21L7R36R10L46L34L31L2R14R14L7R37R18L48L42R33R33L3L20L48R46R47L26L43L41L22L26L41L19R2L5R49L27R35R19L41L6R29L28R41R44R10L33L23R45L9L1R31R37L3R27L9L16L12L37R32L1R36R37R43R46R43R27R30L22R41R47R26R36L22R46L23R9R6L7R42L15L16R47L32L44L18R15R18R7R37R9L23L44R6R47L45L44R6L48R21L17R14R21L17L49L30L42R41L1R21R12R36L47R9R2R24R33R31L44L23L33R50R5R43R16R2L13R4R1R16R16L32L1L31R21L36R32L38L22R36L28L25R15R19L11R18R37L6L28R13L32L34R11R38R27R8R35R47L15R20R27R30R39L3L5L13R30R21L32R35R3L40R14R50R24L30L49L49L21L37L32R43R27L28R4R42R39L27L5R37L18L41L25L29L34L13L50R1R23L27R4R12R39R28L8R13R1L29L44R37R20R44L15R28R2L4L27L5L45L13L32R20L37L25R22L28R32R40R31R32R11R42R25R22L8R42L16R6R37R27L20R6L14R39L18L1R10L9L14L44L33R1L3L44L25R14L20R46R7L43L40R44R6L16R18L1L13R12L34L44L46R7L33R2R24R9L22L18L2R20R47R28L36L44L38L8R16R23L2R36L50R46L29R30L41L2L38L9L41L8L3L28L10L27L46R24R4L6R24L27R5L42R2R17L43L2R40R32R34L40R3R34R28L43R49L11R10R10R22R24L45R37L20R19R48R24R1L33R43L8L29R43R13L18R6R21R36L12L17L41L12L40L43R13L32L13R49L20L11R23L49L4R23L19L13L26L39R29R9L7R33R37L6R42L48L2R47R14R35R5R16R2R23R9R26R5L10L46L34L49L14R25R17L7R25L7L34R3L16R4R5L25R42L49L3L8L17R43R15L14R47R46L15L35R32L34L45R32R39R31R39L7R28R41L26R34L13R40R1R17L11L36R10L3L13L19L25R4R4R34L37L24R42R33L32L32R47R50R8R2L35L12R14R41R41L44R33L17R34L41R23L15L26L38R10L43L7L9R28R48R50L17L35L37R31L7R44L34L42L7L14R16R3L48R20L23L16L17L7R40R12L36L16L17L5L26R46R50R28R35L20R2R18R12L47L5L12R27R16L25L40L42L9R38R11L43L41L45L3R36L9L3R42L37R46R13R24R44L17R5L46L49L29L24R4L46L3R13R22L4L2L43R10L20L35R29R28L35L8L24R15R6R25L15R11R33L27L28R44L13L42R8R37L36R33L38L18R6L19R28R11L3L47L40R20L30R7L37R16L46R31R32R9L18R17L36L14L42R40L41R24R36R40R24L41L23L49R6R28L41R7R47R19L4R6R22L14R5R47L13L43L50R48R22R39L16L35R42R47L33R2R28R47R14R19L2R23L49R40L23R13R43R36L16R23L23L32L35R37R38R25L21L5R48L36R28R13L20R40L46L6R6L8L9L11L34L30L47R1R48R28R15R31R26L17R10R41R1L12R25R39L31L24R30R6L5R33R25L21L26L42L33L50R31R31R10L28L8R28R11R43R45R25R10R47R26R47R27R3R2L34R4R26L34L6L1R10R7R24R49L16R49R48L12L25L6R35R39R42R32L5L44R13L15L25L11L29L7L9L37R30L26L46L35R34R17R50R50R48L23R37L31L18L48L25L21L50L24L4R22L1L5R23R7L38L1L7L38L50L41L46L3R29R29R48R20L8R42R42R14R42L32L5R44R19R49L23L4R21R3R24R1R50L10L23L47R40R2R50L32L4L34R39L7R49R13R30R8R5L48R42L38L35R11L38R11L12R41R14L45R21R29L40R2R30R7L36L40R23L15L15L17L40R29R50L48R1R25R29L17R43R9L48R14L42R41R7R30L23R24L39R16R45R11R16R2L44R45L11R39L28L42R35R33R49R14R26R39L38L15L5L10R26L37L33R45R8L16R44R2R11R27L21R40L39L47R10R23R30R25L7R35R48R17R18L21R24L44L20R27R47R13L16L38R28L17L40L11L31L27L2R34R30L41L37L10R49R7R18L27L7L33L43L41R5R32R15L28R23R27R21R25L9L13R14L3L34R39L42R49R45L45L24R44L1L3R8L29L33R24R16R29L18R37R12L24R44L38R48L23L50R20L45R49L18L25L7R17R3R49L12R6R17L26R33R33L49L5L47R3R37R49L31L24L3R43R11R7R32L1L3L24R50L39L41L25L6L34L23R40R33L16R7L39L7L25L28L20R48R5L22R8R33R18R14R22L19L12L32R37R27L22R30L38R9R33L11R6R46R24L43L20R50R1R13L38L40L24R43R39L40L13L25L7R17R35L28R48L29R41L8L6L5R22R15R14R25L16R38L2R1R14L49L50R46R50L5L8R39L20R20R46R49R24R16R38L24L11L47L5R9L13L37R43L1L38L5R9R35L17R33R17L44L10R26L36L45R45R19L15R25R28R46L42L23R27L18L16L1R22R25R48R9R47L1R14R9R29R49L14R10R39L34R12R43R13R38R27L16L11R33R42L25L44L50L7R31L18R29R34L46L25L15L20R28L20L10R37L3R48L8L46R7L29L29L12L3L42R2L13L11R48L16L8R40L24R10L46R25R31R48L48L9R38L12R4L4L50L6", cube);
        }

        public static List<CubeCommand> CreatePath(string path, Cube cube)
        {
            var commands = new List<CubeCommand>();

            var comm = "";
            foreach (char p in path)
            {
                if (Char.IsNumber(p))
                {
                    comm += p;
                }
                else if (Char.IsLetter(p))
                {
                    var stepsCommand = new CubeCommand(long.Parse(comm), cube);
                    comm = "";
                    commands.Add(stepsCommand);
                    var rotateCommand = new CubeCommand(p, cube);
                    commands.Add(rotateCommand);
                }
            }

            if (comm != "")
            {
                var stepsCommand = new CubeCommand(long.Parse(comm), cube);
                comm = "";
                commands.Add(stepsCommand);
            }

            return commands;
        }
        
        // Test Cube
        public static Cube CreateTestCube()
        {
            long height = 4;
            long width = 4;
            var topCube = CreateTestSide1(height, width);
            var northCube = CreateTestSide2(height, width);
            var westCube = CreateTestSide3(height, width);
            var southCube = CreateTestSide4(height, width);
            var bottomCube = CreateTestSide5(height, width);
            var eastCube = CreateTestSide6(height, width);

            var cube = new Cube()
            {
                StartingSide = topCube,
                Sides = new List<CubeSide>() { topCube, northCube, westCube, southCube, bottomCube, eastCube },
                CurrentSide = topCube
            };

            // construct sides
            topCube.PopulateSides(northCube, southCube, eastCube, westCube);
            bottomCube.PopulateSides(southCube, northCube, eastCube, westCube);
            northCube.PopulateSides(topCube, bottomCube, westCube, eastCube);
            southCube.PopulateSides(topCube, bottomCube, eastCube, westCube);
            eastCube.PopulateSides(topCube, bottomCube, southCube, northCube);
            westCube.PopulateSides(topCube, bottomCube, northCube, southCube);

            return cube;
        }

        public static CubeSide CreateTestSide1(long height, long width)
        {
            var inp =
@"...#
.#..
#...
....";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.Top,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 0,
                ColDisplacement = width * 2
            };

            return side;
        }

        public static CubeSide CreateTestSide2(long height, long width)
        {
            var inp =
@"...#
....
..#.
....";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.North,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 1,
                ColDisplacement = width * 0
            };

            return side;
        }

        public static CubeSide CreateTestSide3(long height, long width)
        {
            var inp =
@"....
....
...#
....";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.West,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 1,
                ColDisplacement = width * 1
            };

            return side;
        }

        public static CubeSide CreateTestSide4(long height, long width)
        {
            var inp =
@"...#
#...
....
..#.";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.South,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 1,
                ColDisplacement = width * 2
            };

            return side;
        }

        public static CubeSide CreateTestSide5(long height, long width)
        {
            var inp =
@"...#
....
.#..
....";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.Bottom,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 2,
                ColDisplacement = width * 2
            };

            return side;
        }

        public static CubeSide CreateTestSide6(long height, long width)
        {
            var inp =
@"....
.#..
....
..#.";
            var map = CreateMap(inp, height, width);
            var side = new CubeSide()
            {
                Side = CubeFace.East,
                Map = map,
                Height = height,
                Width = width,
                RowDisplacement = height * 2,
                ColDisplacement = width * 3
            };

            return side;
        }   
    }
}