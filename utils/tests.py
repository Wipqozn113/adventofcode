from graphs import Node, Graph

def TestShortestPath():
    width = 8
    height = 5
    nodes = [[Node() for i in range(width)] for j in range(height)]
    row = 0
    col = 0
    graph = Graph()
    with open("shortestpath.in") as file:
        for line in file:
            line = line.strip()
            for l in line:
                nodes[row][col].obj = Hill(row, col, l)
                if nodes[row][col].obj.root:
                    root = nodes[row][col]
                if nodes[row][col].obj.goal:
                    goal = nodes[row][col]
                col += 1
            row += 1
            col = 0

    row = 0
    col = 0
    for r in nodes:
        for c in r:
            if row + 1 < height:
                if nodes[row][col].obj.Reachable(nodes[row + 1][col]):
                    nodes[row][col].children.append(nodes[row + 1][col])
            if row - 1 >= 0:
                if nodes[row][col].obj.Reachable(nodes[row - 1][col]):
                    nodes[row][col].children.append(nodes[row - 1][col])
            if col + 1 < width:
                if nodes[row][col].obj.Reachable(nodes[row][col + 1]):
                    nodes[row][col].children.append(nodes[row][col + 1])
            if col - 1 >= 0:
                if nodes[row][col].obj.Reachable(nodes[row][col - 1]):
                    nodes[row][col].children.append(nodes[row][col - 1])
            col += 1
        row += 1
        col = 0

    dist = graph.UnweightedShortestPath(goal, root)
    print(dist)

    

class Hill:
    def __init__(self, x, y, height):
        self.x = x
        self.y = y
        self.height = self.GetHeight(height)
        self.root = False
        self.goal = False
        if height == "S":
            self.root = True
        if height == "E":
            self.goal = True

    def Reachable(self, node):
        return node.obj.height - self.height <= 1

    def GetHeight(self, height):
        # Convert to number
        if height == 'S':
            self.start = True
            height = 'a'
        elif height == 'E':
            self.goal = True
            height = 'z'
        return ord(height)  

#TestShortestPath()

from coordinates import Coordinate, HorizontalLine, Rhombus

def TestLineJoin():    
    start = Coordinate(0, 9)
    end = Coordinate(20, 9)
    ln1 =  HorizontalLine(start, end)
    st = Coordinate(13, 9)
    en = Coordinate(19, 9)
    ln2 =  HorizontalLine(st, en)
    ln1.PrintMe()
    print(ln2.CanJoin(ln1))
    ln1.JoinLines(ln2)
    ln1.PrintMe()

TestLineJoin()
    