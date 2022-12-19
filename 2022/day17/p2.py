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

    def Next(self, debug=False):
        flow = self.pattern[self.n]
        self.pat += flow
        self.n += 1

        # Reset pattern
        if self.n == self.len:
            self.n = 0

        if debug:
            override = input("Override airflow: ")
            if len(override) > 0:
                flow = override.strip()
            print(flow)
        return flow

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
    rocks.append(Rock("S", 2, 2, shape_str=rk))
    
    rks = Rocks(rocks)
    return rks



class Rock:
    def __init__(self, name, height, width, shape_str=None, shape_arr=None, num=0):      
        if shape_str is not None:
            shape_arr = self.CreateShapeArray(shape_str)
        self.position = Coordinate(2, 0)
        self.shape_arr = shape_arr
        self.width = width 
        self.flat_shape = []
        self.shape = self.CreatePointsArray(shape_arr)
        self.left_movement = self.CreateLeftMovement()
        self.right_movement = self.CreateRightMovement()
        self.down_movement = self.CreateDownMovement()
        self.name = name
        self.height = height
        self.num = num

        left = ""
        for l in self.left_movement:
            left += "({},{}),".format(l.coord.x, l.coord.y)
        #print("left:" ,left)

        left = ""
        for l in self.right_movement:
            left += "({},{}),".format(l.coord.x, l.coord.y)
        #print("right: ", left)
        left = ""
        for l in self.down_movement:
            left += "({},{}),".format(l.coord.x, l.coord.y)
        #print("down:", left)
    def MoveRight(self, cave):
        # Check if any point is blocked
        for point in self.right_movement:
            # If any point is blocked, we can't move the rock
            if point.BlockedRight(cave):
                return False

        # Move all points to the right
        for point in self.right_movement:
            point.MoveRight(cave)

        # Rock was moved
        return True

    def MoveLeft(self, cave):
        # Check if any point is blocked
        for point in self.left_movement:
            # If any point is blocked, we can't move the rock
            if point.BlockedLeft(cave):
                return False

        # Move all points to the left
        for point in self.left_movement:
            point.MoveLeft(cave)

        # Rock was moved
        return True

    def MoveDown(self, cave):
        # Check if any point is blocked
        for point in self.down_movement:
            # If any point is blocked, we can't move the rock
            if point.BlockedDown(cave):
                return False

        # Move all points down
        for point in self.down_movement:
            point.MoveDown(cave)

        # Rock was moved
        return True

    def CreatePointsArray(self, shape_arr):
        shape = []
        flat = []
        x,y = self.position.x, self.position.y
        yd = y 
        for shp in shape_arr:
            row = []
            xd = x
            for s in shp:
                typ = s
                if isinstance(s, Point):
                    typ = s.type
                point = Point(typ, self, xd, yd)
                row.append(point)
                flat.append(point)
                xd += 1
            yd += 1
            shape.append(row)
        
        self.flat_shape = flat
        return shape

    def CreateLeftMovement(self):
        left = []
        while len(left) != len(self.flat_shape):
            leftmost = None
            for point in self.flat_shape:
                # Item already added
                if point in left:
                    continue
                # First Item not already in list
                if leftmost is None:
                    leftmost = point
                    continue

                if point.coord.x < leftmost.coord.x:
                    leftmost = point
            left.append(leftmost)

        return left
  

    
    def CreateRightMovement(self):
        right = []
        while len(right) != len(self.flat_shape):
            rightmost = None
            for point in self.flat_shape:
                # Item already added
                if point in right:
                    continue
                # First Item not already in list
                if rightmost is None:
                    rightmost = point
                    continue

                if point.coord.x > rightmost.coord.x:
                    rightmost = point
            right.append(rightmost)

        return right
    
    def CreateDownMovement(self):
        down = []
        while len(down) != len(self.flat_shape):
            downmost = None
            for point in self.flat_shape:
                # Item already added
                if point in down:
                    continue
                # First Item not already in list
                if downmost is None:
                    downmost = point
                    continue

                if point.coord.y > downmost.coord.y:
                    downmost = point
            down.append(downmost)

        return down

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
    def __init__(self, type, rock, x=0, y=0):
        self.rock = rock
        if isinstance(type, Point):
            type = type.type
        self.type = type
        self.coord = Coordinate(x,y)

    def BlockedRight(self, cave):
        # Air is never blocked
        if self.type == ".":
            return False

        # Against a wall
        if self.coord.x == cave.width - 1:
            return True

        # Against a rock
        return cave.cave[self.coord.y][self.coord.x + 1].type == "#" and self.rock != cave.cave[self.coord.y][self.coord.x + 1].rock

    def MoveRight(self, cave):
        # Never bother moving air
        if self.type == ".":
            return True

        # Move yourself
        cave.cave[self.coord.y][self.coord.x + 1] = self

        # Clear out old spot
        cave.cave[self.coord.y][self.coord.x] = Point(".", self.rock, x=self.coord.x, y=self.coord.y)

        # Update coordinates
        self.coord.x += 1

        return True

    def BlockedLeft(self, cave):
        # Air is never blocked
        if self.type == ".":
            return False

        # Against a wall
        if self.coord.x == 0:
            return True

        # Against a rock
        return cave.cave[self.coord.y][self.coord.x - 1].type == "#" and self.rock != cave.cave[self.coord.y][self.coord.x - 1].rock

    def MoveLeft(self, cave):
        # Never bother moving air
        if self.type == ".":
            return True

        # Move yourself
        cave.cave[self.coord.y][self.coord.x - 1] = self

        # Clear out old spot
        cave.cave[self.coord.y][self.coord.x] = Point(".", self.rock, x=self.coord.x, y=self.coord.y)

        # Update coordinates
        self.coord.x -= 1

        return True

    def BlockedDown(self, cave):
        # Air is never blocked
        if self.type == ".":
            return False

        # Against the floor
        if self.coord.y + 1 == cave.height:
            return True

        # Against a rock
        return cave.cave[self.coord.y + 1][self.coord.x].type == "#" and self.rock != cave.cave[self.coord.y + 1][self.coord.x].rock

    def MoveDown(self, cave):
        # Never bother moving air
        if self.type == ".":
            return True

        # Move yourself
        cave.cave[self.coord.y + 1][self.coord.x] = self

        # Clear out old spot
        cave.cave[self.coord.y][self.coord.x] = Point(".", self.rock, x=self.coord.x, y=self.coord.y)

        # Update coordinates
        self.coord.y += 1

        return True

    def Blocks(self, other):
        return self.type == "#" and self.rock != other.rock
    
    @property
    def PrintText(self):
        if self.type == ".":
            return "."
        
        return self.rock.name

    def IsBlockedBy(self, other):
        return self.type == "#" and other.type == "#" and self.rock != other.rock

    def Replace(self, other):
        if self.rock == other.rock:
            return True

        return other.type == "."

class Rocks:
    def __init__(self, rocks):
        self.rocks = rocks
        self.n = 0
        self.len = len(self.rocks)
        self.rocks_generated = 0

    def Next(self):
        self.rocks_generated += 1
        rock = self.rocks[self.n]
        self.n += 1

        # Reset pattern
        if self.n == self.len:
            self.n = 0
        
        return Rock(rock.name, rock.height, rock.width, shape_arr=rock.shape, num=self.rocks_generated)

class Cave:
    def __init__(self, airflow, rocks, width=7):
        self.width = width
        self.cave_width = width # Laaaaaazy
        self.cave = None
        self.airflow = airflow
        self.rocks = rocks
        self.tower_height = 0
        self.cave_height = 0

    @property
    def height(self):
        return self.cave_height

    def FindError(self):
        y = self.cave_height - 1
        count = 0
        last_line = None
        with FileReadBackwards("fuck.out", encoding="utf-8") as frb:
            for line in frb:
                line = line.strip()
                if count > 0:
                    me = ""
                    for x in range(self.width):
                        me += self.cave[y][x].type   
                    print(line)
                    count += 1
                    if count == 4:
                        exit()
                else:
                    for x in range(self.width):
                        if self.cave[y][x].type != line[x]:
                            count += 1
                            print("BUG CITY BAAAABBBYYY")
                            me = ""
                            for x in range(self.width):
                                me += self.cave[y][x].type
                            print(y)
                            print(me)
                            print(line, last_line)
                            for x in range(self.width):
                                if self.cave[y][x].rock is not None:
                                    print(self.cave[y][x].rock.name, self.cave[y][x].rock.num)
                            break
                last_line = line

                y -= 1


    def PrintMe(self, pause=True, debug=False, top_y = 9999999999):
        if not debug:
            return
        print("Tower Height: ", self.tower_height, "Cave height: ", self.cave_height)
        y = 0
        for r in self.cave:
            row = ""
            for c in r:
                row += c.PrintText
            print(row)
            y += 1
            if y == top_y:
                break
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
            if n % 10000 == 0:
                print(n)
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

        #self.PrintMe(debug=True, top_y=15)
        while True:
            #self.PrintMe(debug=True)
            if not self.MoveRock(rock):
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
    
    def MoveRock(self, rock):
        # First airflow movement
        debug = False
        if self.rocks.rocks_generated > 81:
            debug = False

        #self.PrintMe(debug=True, top_y=15)
        airflow = self.airflow.Next(False)
        if airflow == "<":
            rock.MoveLeft(self)
        elif airflow == ">":
            rock.MoveRight(self)
        self.PrintMe(debug=debug, top_y=15)
        # Then gravity movement
        return rock.MoveDown(self)    

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
    rocks.append(Rock("S", 2, 2, shape_str=rk))
    
    rks = Rocks(rocks)
    return rks


def CreateAirFlow(filename):
    with open(filename) as file:
        for line in file:
            airflow = AirFlow(line)
            #print(airflow.pattern)
            return airflow

airflow = CreateAirFlow("test.in")
rocks = CreateRocks()
cave = Cave(airflow, rocks, 7)
print(cave.CalculateHeight(1000000000000))
#cave.PrintMe(debug=True)
#cave.FindError()
#print(cave.PrintMe(debug=True))
#cave.PrintMe(debug=True)# 






    