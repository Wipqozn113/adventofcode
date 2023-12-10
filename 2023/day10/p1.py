import math

class Tile:
    def __init__(self, symbol, x, y):
        self.x = x
        self.y = y 
        self.symbol = symbol
        self.isstart = True if symbol == "S" else False
        self.North = symbol in ['|', 'L', 'J', '.']
        self.East  = symbol in ['-', 'L', 'F', '.']
        self.South = symbol in ['|', 'F', '7', '.']
        self.West  = symbol in ['-', 'J', '7', '.']

    def Exit(self):
        x = self.x
        y = self.y

        # Can't enter from the north
        if self.North:
            return (x, y - 1, 'N')

        if self.South:
            return (x, y + 1, 'S')
        
        if self.East:
            return (x + 1, y, 'E')
        
        if self.West:
            return (x - 1, y, 'W')


    def UpdatePipe(self, symbol):
        self.North = symbol in ['|', 'L', 'J', '.']
        self.East  = symbol in ['-', 'L', 'F', '.']
        self.South = symbol in ['|', 'F', '7', '.']
        self.West  = symbol in ['-', 'J', '7', '.']

    def EnterNorth(self):
        x = self.x
        y = self.y

        # Can't enter from the north
        if not self.North:
            return (x, y - 1, 'N')

        if self.South:
            return (x, y + 1, 'S')
        
        if self.East:
            return (x + 1, y, 'E')
        
        if self.West:
            return (x - 1, y, 'W')

        # Blocked
        return (x, y - 1)

    def EnterEast(self):
        x = self.x
        y = self.y

        # Can't enter from the north
        if not self.East:
            return (x + 1, y, 'E')

        if self.West:
            return (x - 1, y, 'W')
        
        if self.North:
            return (x, y - 1, 'N')
        
        if self.South:
            return (x, y + 1, 'S')

        # Blocked
        return (x + 1, y)        

    def EnterSouth(self):
        x = self.x
        y = self.y

        # Can't enter from the north
        if not self.South:
            return (x, y + 1, 'S')

        if self.North:
            return (x, y - 1, 'N')
        
        if self.East:
            return (x + 1, y, 'E')
        
        if self.West:
            return (x - 1, y, 'W')

        # Blocked
        return (x, y + 1)

    def EnterWest(self):
        x = self.x
        y = self.y

        # Can't enter from the north
        if not self.West:
            return (x - 1, y, 'W')

        if self.East:
            return (x + 1, y, 'E')
        
        if self.North:
            return (x, y - 1, 'N')
        
        if self.South:
            return (x, y + 1, 'S')

        # Blocked
        return (x - 1, y)    

class Grid:
    def __init__(self):
        self.start = None
        self.pipes  = []

    def parseline(self, line):
        line = line.strip()
        pipes = []
        x = 0
        y = len(self.pipes)

        for pipe in line:
            p = Tile(pipe, x, y)
            pipes.append(p)
            if pipe == "S":
                self.start = p
            x += 1

        self.pipes.append(pipes)

    def findloop(self):
        symbols = ['|', '-', 'L', 'J', '7', 'F']
        max_y = len(self.pipes)
        max_x = len(self.pipes[1])
        self.max_loop_size = max_x * max_y 

        for symbol in symbols:
            self.start.UpdatePipe(symbol)
            steps = 1
            directions = self.start.Exit()
            while steps < self.max_loop_size:
                if(directions[0] < 0 or directions[1] < 0) or (directions[0] >= max_x or directions[1] >= max_y):
                    break

                pipe = self.pipes[directions[1]][directions[0]]
                if pipe == self.start:
                    return math.ceil(steps / 2)

                if directions[2] == "N":
                    newdir = pipe.EnterSouth()
                elif directions[2] == "S":
                    newdir = pipe.EnterNorth()
                elif directions[2] == "E":
                    newdir = pipe.EnterWest()
                elif directions[2] == "W":
                    newdir = pipe.EnterEast()
                
                if newdir[0] == directions[0] and newdir[1] == directions[1]:
                    break

                steps += 1
                directions = newdir



grid = Grid()
filename = "input.in"
with open(filename) as file:
    for line in file:
        grid.parseline(line)

print(grid.findloop())
            


        
