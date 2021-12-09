class LowPoint:
    def __init__(self, height, x, y):
        self.height = height
        self.x = x
        self.y = y

    @property
    def Risk(self):
        return self.height + 1        

class Point:
    def __init__(self, height):
        self.height = height
        self.filled = False

    def __ge__(self, other):
        if isinstance(other, Point):
            return self.height >= other.height

        return NotImplementedError

class Basin:
    def __init__(self, lowpoint, map):
        self.lowpoint = lowpoint
        self.map = map
        self.size = 0

    def FloodFill(self, x, y):
        try:
            if self.map[x][y].height == 9 or x < 0 or y < 0 or self.map[x][y].filled:
                return
        except IndexError:
            return

        self.map[x][y].filled = True
        self.size += 1
        
        self.FloodFill(x + 1, y)
        self.FloodFill(x - 1, y)
        self.FloodFill(x, y + 1)
        self.FloodFill(x, y - 1)

        return

class HeightMap:
    def __init__(self):
        self.map = []
        self.lowpoints = []
        self.top3 = []

    def AddRow(self, row):
        self.map.append([Point(int(x)) for x in list(row.strip())])

    @property
    def Length(self):
        return len(self.map)

    @property
    def Width(self):
        return len(self.map[0])

    @property
    def TotalRisk(self):
        self.CalcLowPoints()
        risk = 0
        for lp in self.lowpoints:
            risk += lp.Risk

        return risk

    @property 
    def Top3Mult(self):
        self.CalcLowPoints()
        mult = self.top3[0] * self.top3[1] * self.top3[2]
        return mult

    def CalcLowPoints(self):
        # Don't do this twice
        if(len(self.lowpoints) > 0):
            return

        for x in range(self.Length):
            for y in range(self.Width):
                if self.IsLowPoint(x, y):
                    lp = LowPoint(self.map[x][y].height, x, y)
                    self.lowpoints.append(lp)
                    basin = Basin(lp, self.map)
                    basin.FloodFill(x, y)
                    self.top3.append(basin.size)
                    self.top3 = sorted(self.top3, reverse=True)[:3]

    def IsLowPoint(self, x, y):
        if((x != 0) and (self.map[x][y] >= self.map[x-1][y])):
            return False

        if((x != self.Length - 1) and (self.map[x][y] >= self.map[x+1][y])):
            return False

        if((y != 0) and (self.map[x][y] >= self.map[x][y-1])):
            return False

        if((y != self.Width - 1) and (self.map[x][y] >= self.map[x][y+1])):
            return False

        return True

map = HeightMap()
with open('p1.in') as file:
    for line in file:
        map.AddRow(line)

print(map.Top3Mult)