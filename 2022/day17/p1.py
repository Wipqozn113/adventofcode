# Inject into python path
import sys
import os
import re 
import math
from file_read_backwards import FileReadBackwards

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates import Coordinate, HorizontalLine, Rhombus

class AirFlow:
    def __init__(self, pattern):
        self.pattern = list(pattern.strip())
        self.n = 0
        self.len = len(self.pattern)
        self.pat = ""

    def Next(self):
        flow = self.pattern[self.n]
        self.pat += flow
        self.n += 1

        # Reset pattern
        if self.n == self.len:
            self.n = 0

        return flow

class Rock:
    def __init__(self, name, height, width, shape_str=None, shape_arr=None):      
        if shape_str is not None:
            shape_arr = self.CreateShapeArray(shape_str)
        self.shape_arr = shape_arr
        self.width = width 
        self.shape = self.CreatePointsArray(shape_arr)
        self.name = name
        self.height = height

    def CreatePointsArray(self, shape_arr):
        shape = []
        for shp in shape_arr:
            row = []
            for s in shp:
                typ = s
                if isinstance(s, Point):
                    typ = s.type
                row.append(Point(typ, self))
            shape.append(row)
        
        return shape

    def CreateShapeArray(self, shape):
        if shape is None:
            raise Exception("Need a string!")

        arr = shape.split("\n")
        shp = []
        for s in arr:
            shp.append(list(s))

        self.shape = shp
        return shp

class Point:
    def __init__(self, type, rock):
        self.rock = rock
        if isinstance(type, Point):
            type = type.type
        self.type = type

    def Blocks(self, other):
        return self.type == "#" and self.rock != other.rock
    
    def IsBlockedBy(self, other):
        return self.type == "#" and other.type == "#" and self.rock != other.rock

class Rocks:
    def __init__(self, rocks):
        self.rocks = rocks
        self.n = 0
        self.len = len(self.rocks)

    def Next(self):
        rock = self.rocks[self.n]
        self.n += 1

        # Reset pattern
        if self.n == self.len:
            self.n = 0
        
        return Rock(rock.name, rock.height, rock.width, shape_arr=rock.shape)

class Cave:
    def __init__(self, airflow, rocks, width=7):
        self.width = width
        self.cave_width = width # Laaaaaazy
        self.cave = None
        self.airflow = airflow
        self.rocks = rocks
        self.tower_height = 0
        self.cave_height = 0

    def FindError(self):
        y = self.cave_height - 1
        with FileReadBackwards("test.out", encoding="utf-8") as frb:
            for line in frb:
                if line[0] == "+":
                    continue
                line = line.strip().strip("|")
                for x in range(self.width):
                    if self.cave[y][x].type != line[x]:
                        print("BUG CITY BAAAABBBYYY")
                        me = ""
                        for x in range(self.width):
                            me += self.cave[y][x].type
                        print(y)
                        print(me)
                        print(line)
                        for x in range(self.width):
                            if self.cave[y][x].rock is not None:
                                print(self.cave[y][x].rock.name)
                        exit()
                y -= 1


    def PrintMe(self, pause=True, debug=False):
        if not debug:
            return
        print("Tower Height: ", self.tower_height, "Cave height: ", self.cave_height)
        for r in self.cave:
            row = ""
            for c in r:
                row += c.type
            print(row)
        if pause:
            input("Any key to continue")

    def ExpandCave(self, rock):
        expand_by = 3 + rock.height
        for row in range(expand_by):
            row = []
            for col in range(self.width):
                row.append(Point(".", None))
            if self.cave is None:
                self.cave = [row]
            else:
                self.cave = [row] + self.cave        
        self.cave_height = len(self.cave)

    def CalculateHeight(self, rocks=2022):
        for n in range(rocks):
            rock = self.rocks.Next()
            self.ExpandCave(rock)    
            self.DropRock(rock)
            self.AdjustHeight()
            self.PrintMe()

        return self.tower_height  

    def DropRock(self, rock):
        # Toss rock in the cave
        r = 0
        for row in rock.shape:
            self.cave[r][2:2+rock.width] = row
            r += 1
        rock_position = Coordinate(2, 0)

        while True:
            if not self.MoveRock(rock, rock_position):
                break

    def AdjustHeight(self):
        stop = False
        self.cave_height = len(self.cave)
        for y in range(self.cave_height):
            for x in range(self.cave_width):
                if self.cave[y][x].type == "#":
                    self.tower_height = self.cave_height - y
                    stop = True
                    break
            if stop:
                break

        if self.tower_height < self.cave_height:
            self.TrimTop()        

    def TrimTop(self):
        trim = self.cave_height - self.tower_height
        self.cave = self.cave[trim:]
        self.cave_height = len(self.cave)
    
    def MoveRock(self, rock, position):
        # First airflow movement

        airflow = self.airflow.Next()
        if airflow == "<":
            self.MoveRockLeft(rock, position)
        elif airflow == ">":
            self.MoveRockRight(rock, position)

        # Then gravity movement
        return self.MoveRockDown(rock, position)

    def MoveRockLeft(self, rock, position):
        cols = rock.width
        rows = rock.height
        x,y = position.x, position.y

        # Rock against wall. Cannot be moved.
        if position.x == 0:
            return False

        self.PrintMe()

        # Check if the rock is blocked by another rock
        for xd in range(cols):
            for yd in range(rows):
                # If any point is blocked, we can't move the rock
                if self.cave[y + yd][x + xd].IsBlockedBy(self.cave[y + yd][x + xd - 1]):
                    return False

        # Move rock to the left
        for xd in range(cols):
            for yd in range(rows):
                #if rock.name == "L":
                #    self.PrintMe(debug=True)
                #    print(x, self.cave[y + yd][x + xd - 1].type, self.cave[y + yd][x + xd].type)
                self.cave[y + yd][x + xd - 1] = self.cave[y + yd][x + xd]

        # Clear out last bit of rock
        for yd in range(rows):
            self.cave[y + yd][x + cols - 1] = Point(".", None)

        self.PrintMe()

        # Update its coordinate
        position.x -= 1
        return True

    def MoveRockRight(self, rock, position):
        cols = rock.width
        rows = rock.height
        x,y = position.x, position.y

        # Rock against wall. Cannot be moved.
        if position.x + rock.width == self.width:
            return False

        self.PrintMe()

        # Check if the rock is blocked by another rock
        for xd in range(cols):
            for yd in range(rows):
                # If any point is blocked, we can't move the rock
                if self.cave[y + yd][x + xd].IsBlockedBy(self.cave[y + yd][x + xd + 1]):
                    return False

        # Move rock to the right
        for xd in range(cols, 0, -1):
            for yd in range(rows):
                self.cave[y + yd][x + xd] = self.cave[y + yd][x + xd - 1]
       
        # Clear out last bit of rock
        for yd in range(rows):
            self.cave[y + yd][x] = Point(".", None)

        self.PrintMe()

        # Update its coordinate
        position.x += 1
        return True


    def MoveRockDown(self, rock, position):
        cols = rock.width
        rows = rock.height
        x,y = position.x, position.y

        # Rock against floor. Cannot be moved.
        if position.y + rock.height == self.cave_height:
            return False

        self.PrintMe()

        # Check if the rock is blocked by another rock
        for xd in range(cols):
            for yd in range(rows, 0, -1):
                # If any point is blocked, we can't move the rock
                if self.cave[y + yd - 1][x + xd].IsBlockedBy(self.cave[y + yd][x + xd]):
                    return False

        # Move rock down
        for xd in range(cols):
            for yd in range(rows, 0, -1):
                self.cave[y + yd][x + xd] = self.cave[y + yd - 1][x + xd]

        # Clear out last bit of rock
        for xd in range(cols):
            self.cave[y][x + xd] = Point(".", None)


        self.PrintMe()

        # Update its coordinate
        position.y += 1
        return True

def CreateRocks():
    rocks = []
    rk = "####"
    rocks.append(Rock("-", 1, 4, shape_str=rk))
    rk = ".#.\n###\n.#."
    rocks.append(Rock("+", 3, 3, shape_str=rk))
    rk = "..#\n..#\n###"
    rocks.append(Rock("L", 3, 3, shape_str=rk))
    rk = "#\n#\n#\n#"
    rocks.append(Rock("|", 4, 1, shape_str=rk))
    rk = "##\n##"
    rocks.append(Rock("[]", 2, 2, shape_str=rk))

    rks = Rocks(rocks)
    return rks


def CreateAirFlow(filename):
    with open(filename) as file:
        for line in file:
            airflow = AirFlow(line)
            return airflow


airflow = CreateAirFlow("test.in")
rocks = CreateRocks()
cave = Cave(airflow, rocks, 7)
print(cave.CalculateHeight(2022))
cave.FindError()





    