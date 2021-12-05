class Coord:
    def __init__(self, x, y):
        self.x = x
        self.y = y

class Line:
    def __init__(self, start, end):
        st = start.split(",")
        self.start = Coord(int(st[0].strip()), int(st[1].strip()))
        en = end.split(",")
        self.end = Coord(int(en[0].strip()), int(en[1].strip()))

    def Is90Degrees(self):
        return ((self.start.x == self.end.x) or (self.start.y == self.end.y))

    def MaxX(self):
        return self.start.x if self.start.x > self.end.x else self.end.x

    def MaxY(self):
        return self.start.y if self.start.y > self.end.y else self.end.y

class OceanFloor:
    def __init__(self, height, width, lines):
        self.height = height
        self.width = width
        self.floor = [[0]*height for _ in range(width)]
        self.lines = lines

    def DrawAllLines(self):
        for line in self.lines:
            self.DrawLine(line)

    def Draw90DegreeLines(self):
        for line in self.lines:
            if(line.Is90Degrees()):
                self.DrawLine(line)

    def DrawLine(self, line):
        if(line.Is90Degrees()):
            self.Draw90DegreeLine(line)
        else:
            self.Draw45DegreeLine(line)

    def Draw45DegreeLine(self, line):
        # Sanity check
        if not line.Is90Degrees():
            x = line.start.x
            y = line.start.y
            endx = line.end.x
            xinc = 1 if line.start.x < line.end.x else -1
            yinc = 1 if line.start.y < line.end.y else -1
            while True:                
                self.floor[x][y] += 1
                x += xinc
                y += yinc
                if(x == endx):
                    self.floor[x][y] += 1
                    break

    def Draw90DegreeLine(self, line):
        # Sanity check
        if(line.Is90Degrees()):
            if(line.start.x == line.end.x):
                x = line.start.x
                start, end = (line.start.y, line.end.y) if line.start.y < line.end.y else (line.end.y, line.start.y)
                for y in range(start, end + 1):
                    self.floor[x][y] += 1
            if(line.start.y == line.end.y):
                y = line.start.y
                start, end = (line.start.x, line.end.x) if line.start.x < line.end.x else (line.end.x, line.start.x)
                for x in range(start, end + 1):
                    self.floor[x][y] += 1
        
    def ReadableFloor(self):
        floor = ""
        for x in range(self.height):
            for y in range(self.width):
                floor += str(self.floor[x][y]) + " "

            floor += "\n"

        return floor

    def CalcPoints(self):
        points = 0
        for x in range(self.height):
            for y in range(self.width):
                if(self.floor[x][y] > 1):
                    points += 1

        return points

        
lines = []
height = 0
width = 0
with open('p1.in') as file:
    for fline in file:
        l = fline.strip().split("->")
        line = Line(l[0], l[1])
        height = line.MaxX() if line.MaxX() > height else height
        width = line.MaxY() if line.MaxY() > width else width
        lines.append(line)

hw = height if height > width else width
hw += 1
floor = OceanFloor(hw, hw, lines)
floor.DrawAllLines()
#print(floor.ReadableFloor())
print(floor.CalcPoints())