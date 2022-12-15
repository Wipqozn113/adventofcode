from matplotlib import pyplot as plt
from matplotlib.colors import LinearSegmentedColormap
import time
from matplotlib.animation import FuncAnimation

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
        self.cave = [[0]*800 for i in range(800)]
        self.width = 800
        self.height = 800
        self.rested_sand = 0



    def PrintMe(self, sand=None):
        print("oh")
        if sand is not None:
            temp = self.cave[sand.y][sand.x]
            self.cave[sand.y][sand.x] = 2
        self.plot.set_data([x[400:600] for x in self.cave])
        if sand is not None:
            self.cave[sand.y][sand.x] = temp
        return self.plot
        #self.fig.canvas.flush_events()
                
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
        self.cave.append([1] * self.width)
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
        
    def AnimateSand(self, drop_coord=None):
        plt.ion()
        plt.register_cmap(cmap=LinearSegmentedColormap.from_list(name='sand',colors=[[0.0,0.0,0.0,1.0],[1.0,0.0,0.0,1.0],[194.0,178.0,128.0,1.0]]))
        plt.axis("off")
        self.fig, self.ax = plt.subplots()
        plt.figure(figsize = (10,10))
        self.plot = plt.imshow([x for x in self.cave], interpolation='nearest',cmap="sand",vmin=0,vmax=2)
        self.ln = self.ax.plot([],[])

        if drop_coord is None:
            drop_coord = Coordinate(500, 0)


        ani = FuncAnimation(self.fig, self.Animate, 
        interval=20, frames=1000, blit=True)
        plt.show()

    def Animate(self, w):
        print("hwat")
        self.CreateSand(Coordinate(500, 0))
        return self.PrintMe()
        

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
        if self.cave[curr.y][curr.x] == 2:
            return None
        while True:
            # print(curr.x, curr.y)
            # Don't bother checking X, since we're just going to force the Cave to be large enough
            if curr.y + 1 >= self.height:
                # The abyss!
                return None
            if  self.cave[curr.y + 1][curr.x] == 0:
                curr.y += 1
            elif self.cave[curr.y + 1][curr.x - 1] == 0:
                curr.y += 1
                curr.x -= 1
            elif self.cave[curr.y + 1][curr.x + 1] == 0:
                curr.y += 1
                curr.x += 1
            else:                
                rest_coord = curr
                self.cave[curr.y][curr.x] = 2
                self.PrintMe()
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
                self.cave[y][x] = 1
                y += 1
        else:
            while x <= c2.x:
                self.cave[y][x] = 1
                x += 1

def CalculateSandFill(cave, filename):
    cave.CreateCave(filename)
    cave.PrintMe()
    return cave.FillWithSand()

cave = Cave()
cave.CreateCave("input.in")
cave.AnimateSand()




'''
#importing libraries
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import numpy as np

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
'''
