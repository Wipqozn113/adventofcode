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
        for row in range(self.height):
            line = ""
            for col in range(400, 600):
                line += self.cave[row][col]
            print(line)
                
    def CreateCave(self, filename):
        highest_y = 0
        with open(filename) as file:
            for line in file:
                line = line.strip()
                y = self.AddRocks(line)
                if y > highest_y:
                    highest_y = y

        # Slice off extra parts of cave
        self.cave = self.cave[:highest_y + 2]
        self.cave.append(["#"] * self.width)
        self.height = len(self.cave)       

    def AddRocks(self, path):
        coordinates = path.split('->')
        last_coord = None
        highest_y = 0
        for coordinate in coordinates:
            coord = Coordinate(*(coordinate.strip().split(',')))            
            if coord.y > highest_y:
                highest_y = coord.y
            if last_coord is not None:
                self.DrawPath(coord, last_coord)
            last_coord = coord

        return highest_y
        

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
        curr = Coordinate(drop_coord.x, drop_coord.y)
        if self.cave[curr.y][curr.x] == "O":
            return None
        while True:
            # print(curr.x, curr.y)
            # Don't bother checking X, since we're just going to force the Cave to be large enough
            if curr.y + 1 >= self.height:
                # The abyss!
                return None
            if  self.cave[curr.y + 1][curr.x] == ".":
                curr.y += 1
            elif self.cave[curr.y + 1][curr.x - 1] == ".":
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
    cave.CreateCave(filename)
    return cave.FillWithSand()

cave = Cave()

#print(CalculateSandFill(cave, "input.in"))


#importing libraries
import matplotlib.pyplot as plt
import matplotlib.animation as animation


fig = plt.figure()
#creating a subplot 
ax1 = fig.add_subplot(1,1,1)

def animate(i):
    cave = Cave()
    cave.CreateCave("test.in")
    xs = []
    ys = []
   
    for y in range(cave.height):
        for x in range(cave.width):
            if cave.cave[y][x] == "#":
                xs.append(y)
                ys.append(x)
    
    
    ax1.clear()
    ax1.plot(xs, ys)

    plt.xlabel('Date')
    plt.ylabel('Price')
    plt.title('Live graph with matplotlib')	
	
    
ani = animation.FuncAnimation(fig, animate, interval=1000) 
plt.show()
