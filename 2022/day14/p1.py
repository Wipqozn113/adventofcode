class Coordinate:
    def __init__(self, x, y):
        self.x = int(x)
        self.y = int(y)

    def IsVertical(self, other):
        if self == other:
            return None 
        return self.x == other.x

    def __eq__(self, other):
        return self.x == other.x and self.y == other.y

    def __gt__(self, other):
        return self.x > other.x or self.y > other.y

class Cave:
    def __init__(self):
        # Laaaaaaazy
        self.cave = [ ["."]*800 for i in range(800)]
        self.width = 800
        self.height = 800

    def PrintMe(self):
        start = False
        for row in range(20):
            line = ""
            for col in range(480, 520):
                line += self.cave[row][col]
            print(line)
                

    def AddRocks(self, path):
        coordinates = path.split('->')
        last_coord = None
        for coordinate in coordinates:
            coord = Coordinate(*(coordinate.strip().split(',')))
            print(coord.y, coord.x)
            if last_coord is not None:
                self.DrawPath(coord, last_coord)
            last_coord = coord
        self.PrintMe()

    def FillWithSand(self, drop_coord=None):
        if drop_coord is None:
            drop_coord = Coordinate(500, 0)

        rested_sand = 0
        while True:
            coord = self.CreateSand(drop_coord)
            if coord is None:
                break
            rested_sand += 1

        return rested_sand

    def CreateSand(self, drop_coord):
        rest_coord = None
        curr = drop_coord
        while True:
            # print(curr.x, curr.y)
            # Don't bother checking X, since we're just going to force the Cave to be large enough
            if curr.y + 1 >= self.height:
                # The abyss!
                return None
            if  self.cave[curr.y + 1][curr.x] == ".":
                curr.y += 1
            elif self.cave[curr.x + 1][curr.x - 1] == ".":
                curr.y += 1
                curr.x -= 1
            elif self.cave[curr.y + 1][curr.x + 1] == ".":
                curr.y += 1
                curr.x += 1
            else:
                rest_coord = curr
                self.cave[curr.y][curr.x] = "O"
                break

        return rest_coord

    def DrawPath(self, coord1, coord2):
        c1 = coord1
        c2 = coord2
        if coord1 > coord2:
            c1 = coord2
            c2 = coord1

        x = c1.x
        y = c1.y
        if c1.IsVertical(c2):
            while y <= c2.y:
                self.cave[y][x] = "#"
                y += 1
        else:
            while x <= c2.x:
                self.cave[y][x] = "#"
                x += 1

def CalculateSandFill(cave, filename):
    with open(filename) as file:
        for line in file:
            line = line.strip()
            cave.AddRocks(line)
    return cave.FillWithSand()

cave = Cave()
print(CalculateSandFill(cave, "test.in"))
cave.PrintMe()