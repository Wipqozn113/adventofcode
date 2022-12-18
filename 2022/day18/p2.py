# Inject into python path
import sys
import os
import re 
import math
import numpy as np
import queue

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates3d import Coordinate3D
class Graph:
    def __init__(self, root=None):
        self.root = root
        self.nodes = set()

    def BFS(self, root=None):
            # Default to Graph Root
            if root is None:
                root = self.root

            # Setup queue with root
            q = queue.Queue()
            root.explored = True
            q.put(root)

            while not q.empty():
                node = q.get()
                
                for child in node.neighbours:
                    if child.VisitMe():
                        child.explored = True
                        child.parent = node
                        q.put(child)

class Cave:
    def __init__(self, size):
        self.dict = {}
        self.cave = np.full((size+2,size+2,size+2), Nothing()) # Make a 10 by 20 by 30 array
        self.cubes = []
        self.size = size+2

    def AddCube(self, cube):
        self.cave[cube.coord.x,cube.coord.y,cube.coord.z] = cube
        self.cubes.append(cube)

    def PopulateCave(self):
        for x in range(self.size):
            for y in range(self.size):
                for z in range(self.size):
                    if isinstance(self.cave[x,y,z], Nothing):
                        self.cave[x,y,z] = AirPocket(x,y,z)

        for x in range(self.size):
            for y in range(self.size):
                for z in range(self.size):
                    self.cave[x,y,z].SetNeighbours(self)

class AirPocket:
    def __init__(self, x, y, z):
        self.coord = Coordinate3D(x, y, z)
        self.neighbours = []
        self.key = self.coord.__hash__()
        self.parent = None
        self.explored = False
        self.wet = False

    def SetNeighbours(self, cave):
        adj = self.coord.AdjCoordinates()
        for coord in adj:
            try:
                if coord.x >= 0 and coord.y >= 0 and coord.z >= 0:
                    self.neighbours.append(cave.cave[coord.x, coord.y, coord.z])
            except IndexError:
                continue

    def VisitMe(self):  
        self.wet = True
        return not self.explored

class Nothing:
    def __init__(self):
        pass

class Steam:
    def __init__(self, x, y, z):
        self.coord = Coordinate3D(x, y, z)        
        self.neighbours = []
        self.key = self.coord.__hash__()
        self.parent = None
        self.explored = False
        self.wet = False

    def VisitMe(self):
        return self.explored


class Cube:
    def __init__(self, x, y, z, name):
        self.coord = Coordinate3D(x, y, z)
        self.neighbours = []
        self.adjcubes = []
        self.key = self.coord.__hash__()
        self.wet = False
        self.parent = None
        self.explored = False
        self.name = name

    def VisitMe(self):
        self.wet = True
        return False

    @property
    def ReachableExposedSides(self):
        if self.wet:
            return 6 - self.BlockedSide
        else:
            return 0

    @property
    def BlockedSide(self):
        count = 0
        for side in self.neighbours:
            if isinstance(side, Cube):
                count += 1
            elif isinstance(side, AirPocket) and not side.wet:
                count += 1
        return count

    @property
    def ExposedSides(self):
        return 6 - len(self.adjcubes)

    def SetNeighbours(self, cave):
        adj = self.coord.AdjCoordinates()
        for coord in adj:
            try:
                if coord.x >= 0 and coord.y >= 0 and coord.z >= 0:
                    self.neighbours.append(cave.cave[coord.x, coord.y, coord.z])
                    if isinstance(cave.cave[coord.x, coord.y, coord.z], Cube):
                        self.adjcubes.append(cave.cave[coord.x, coord.y, coord.z])
            except IndexError:
                continue

def CreateCubes(filename):
    max_coord = 0
    cubes = []
    with open(filename) as file:
        for line in file:
            line = line.strip()
            name = line
            ln = line.split(",")
            ln[0] = 2 + int(ln[0])
            ln[1] = 2 + int(ln[1])
            ln[2] = 2 + int(ln[2])
            cube = Cube(ln[0], ln[1], ln[2], name)
            cubes.append(cube)
            max_coord = max(max_coord, cube.coord.LargestCoord)
    
    return max_coord, cubes

def FindExposedSides(cubes):
    exposed = 0
    for cube in cubes:
        exposed += cube.ExposedSides

    return exposed

def FindReachableExposedSides(cubes):
    exposed = 0
    for cube in cubes:
        exposed += cube.ReachableExposedSides

    return exposed

def CreateCave(filename):
    max_coord, cubes = CreateCubes(filename)
    cave = Cave(max_coord)
    for cube in cubes:
        cave.AddCube(cube)
    cave.PopulateCave()
    graph = Graph(cave.cave[0,0,0])
    graph.BFS()

    return cave


cave = CreateCave("input.in")
print(FindExposedSides(cave.cubes))
print(FindReachableExposedSides(cave.cubes))