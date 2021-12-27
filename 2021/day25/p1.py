class Coordinates:
    def __init__(self, x, y):
        self.x = x
        self.y = y

class Ocean:
    def __init__(self):
        self.floor = []
        self.eastsc = []
        self.southsc = []

    def AddRow(self, row):
        newrow = []
        y = len(self.floor)
        x = 0
        for r in row:
            if r == ">":
                sc = SeaCummber(Coordinates(x, y), ">", self.floor)
                newrow.append(sc)
                self.eastsc.append(sc)
            elif r == "v":
                sc = SeaCummber(Coordinates(x, y), "v", self.floor)
                newrow.append(sc)
                self.southsc.append(sc)
            else:
                newrow.append(".")
            x += 1
        self.floor.append(newrow)

    def FindSafeSpot(self):
        step = 1
        while True:
            moved = self.MoveSeaCucumbers()
            if moved:
                step += 1
            else: 
                break

        print(step)
    
    def MoveSeaCucumbers(self):
        emove = []
        for sc in self.eastsc:
            if sc.CanMove():
                emove.append(sc)

        for sc in emove:
            sc.Move()

        smove = []
        for sc in self.southsc:
            if sc.CanMove():
                smove.append(sc)

        for sc in smove:
            sc.Move()

        return (len(emove) > 0 or len(smove) > 0)



class SeaCummber:
    def __init__(self, coordinates, direction, floor):
        self.x = coordinates.x
        self.y = coordinates.y
        self.dir = direction
        self.floor = floor

    def GetNewCoord(self):
        if self.dir == ">":
            y = self.y
            x = self.x + 1 if self.x + 1 < len(self.floor[y]) else 0

        elif self.dir == "v":
            x = self.x
            y = self.y + 1 if self.y + 1 < len(self.floor) else 0

        return x, y

    def CanMove(self):
        x, y = self.GetNewCoord()

        return not isinstance(self.floor[y][x], SeaCummber)

    def Move(self):
        x, y = self.GetNewCoord()
        self.floor[y][x] = self
        self.floor[self.y][self.x] = "."
        self.x = x
        self.y = y

ocean = Ocean()
with open('p1.in') as file:
    for line in file:
        ocean.AddRow(line.strip())

ocean.FindSafeSpot()