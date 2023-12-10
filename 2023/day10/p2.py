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
        self.part_of_loop = False
        self.inside_loop = False

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
        self.loop = []

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
            loop = []
            self.start.UpdatePipe(symbol)
            steps = 1
            directions = self.start.Exit()
            while steps < self.max_loop_size:                
                if(directions[0] < 0 or directions[1] < 0) or (directions[0] >= max_x or directions[1] >= max_y):
                    break

                pipe = self.pipes[directions[1]][directions[0]]
                loop.append(pipe)
                if pipe == self.start:
                    for p in loop:
                        self.loop.append(p)
                        self.start.symbol = symbol
                        p.part_of_loop = True
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

    def find_adj_nonlooped_tiles(self, loop_tile, tile_adj_loop_wall):
        x, y = loop_tile.x, loop_tile.y

        try:
            if not self.pipes[y][x + 1].part_of_loop:
                tile_adj_loop_wall.add(self.pipes[y][x + 1])
        except IndexError:
            pass

        try:
            if not self.pipes[y][x - 1].part_of_loop:
                tile_adj_loop_wall.add(self.pipes[y][x - 1])
        except IndexError:
            pass

        try:
            if not self.pipes[y + 1][x].part_of_loop:
                tile_adj_loop_wall.add(self.pipes[y + 1][x + 1])
        except IndexError:
            pass

        try:
            if not self.pipes[y - 1][x].part_of_loop:
                tile_adj_loop_wall.add(self.pipes[y - 1][x - 1])
        except IndexError:
            pass

    def clearJunk(self):
        for row in self.pipes:
            r = ""
            for tile in row:
                if tile.part_of_loop == False:
                    tile.symbol = "."
                r += tile.symbol

    def flag_if_inside_loop(self, tile):
        # We already determined this is nested inside the loop
        if tile.inside_loop
            # Ensure tils in the same row and column 
            # are properly flagged as inside the loop
            flag_row_column(tile)
            return 

        # Attempt to trace a path from this tile back to itself
        # If we can trace such a  path, then the tile is inside the loop
        if can_trace_path(tile):
            tile.inside_loop = True
            # Any tiles in the same row and column are also
            # inside the loop, so flag them accordingly
            flag_row_column(tile)

    def flag_row_column(self, tile):
        x = tile.x
        y = tile.y
        for (i in range(x, 0, -1)):
            if(self.pipes[y][i].part_of_loop):
                break
            else:
                self.pipes[y][i].inside_loop = True
        for (i in range(x, len(self.pipes[0]))):
            if(self.pipes[y][i].part_of_loop):
                break
            else:
                self.pipes[y][i].inside_loop = True
        for (i in range(y, 0, -1)):
            if(self.pipes[i][x].part_of_loop):
                break
            else:
                self.pipes[i][x]].inside_loop = True
        for (i in range(x, len(self.pipes))):
            if(self.pipes[i][x].part_of_loop):
                break
            else:
                self.pipes[i][x].inside_loop = True



    def can_trace_path(self, tile):
        pass

    def findNestedTiles(self):
        # Clear out anything not part of the loop. 
        # Not needed, but makes my life easier.
        # Also makes output more readable when debugging.
        self.clearJunk() 
        
        # Collect all ground tiles adjacent to a pipe from the loop
        tile_adj_to_loop = set()
        for loop_tile in self.loop:
            self.find_adj_nonlooped_tiles(loop_tile, tile_adj_to_loop)
                
        # Flag all adj. tiles that are nested inside the loop
        for tile in tile_adj_to_loop:
            flag_if_inside_loop(tile)

        # 



grid = Grid()
filename = "test3.in"
with open(filename) as file:
    for line in file:
        grid.parseline(line)

grid.findloop()
grid.findNestedTiles()
            


        
