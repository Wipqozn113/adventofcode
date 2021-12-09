class LowPoint:
    def __init__(self, height, x, y):
        self.height = height
        self.x = x
        self.y = y

    @property
    def Risk(self):
        return self.height + 1


class HeightMap:
    def __init__(self):
        self.map = []
        self.lowpoints = []

    def AddRow(self, row):
        self.map.append([int(x) for x in list(row.strip())])

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

    def CalcLowPoints(self):
        # Don't do this twice
        if(len(self.lowpoints) > 0):
            return

        for x in range(self.Length):
            for y in range(self.Width):
                if self.IsLowPoint(x, y):
                    self.lowpoints.append(LowPoint(self.map[x][y], x, y))

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

print(map.TotalRisk)