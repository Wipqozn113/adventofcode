# Inject into python path
import sys
import os
import re 
import math

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates3d import Coordinate3D
from utils.graphs import Graph, Node

class Cave:
    def __init__(self, airpockets):
        self.dict = {}
        self.airpockets = airpockets

    def AddAirPocket(self, coord):
        pocket = None
        if coord.Key not in self.dict:
            pocket = AirPocket(coord.x, coord.y, coord.z)
            self.dict[coord.Key] = pocket
        elif isinstance(self.dict[coord.Key], AirPocket):
            pocket = self.dict[coord.Key]
        
        return pocket

class AirPocket:
    def __init__(self, x, y, z):
        self.coord = Coordinate3D(x, y, z)
        self.neighbours = []
        self.key = self.coord.__hash__()
    

class Steam:
    def __init__(self, x, y, z):
        self.coord = Coordinate3D(x, y, z)        
        self.neighbours = []
        self.key = self.coord.__hash__()

class Cube:
    def __init__(self, x, y, z):
        self.coord = Coordinate3D(x, y, z)
        self.neighbours = []
        self.adjcubes = []
        self.key = self.coord.__hash__()

    @property
    def ExposedSides(self):
        return 6 - len(self.neighbours)

    def AddIfNeighbour(self, other):
        if self.coord.AreAdj(other.coord):
            self.neighbours.append(other)
            self.adjcubes.append(other)
            return True

        return False

    def FindNeighbours(self, cubes):
        for cube in cubes:
            if self.AddIfNeighbour(cube):
                if self.ExposedSides == 0:
                    return 0

        return self.ExposedSides
    
    def CreateAirPockets(self, cave):
        if self.ExposedSides == 0:
            return
        
        adjcoords = self.coord.AdjCoordinates()
        for coord in adjcoords:
            pocket = cave.AddAirPocket(coord)
            if pocket is not None:
                self.neighbours.append(pocket)



def CreateCubes(filename):
    cubes = []
    with open(filename) as file:
        for line in file:
            line = line.strip()
            ln = line.split(",")
            cube = Cube(*ln)
            cubes.append(cube)
    
    return cubes

def FindExposedSides(cubes):
    exposed = 0
    for cube in cubes:
        exposed += cube.FindNeighbours(cubes)

    return exposed


cubes = CreateCubes("input.in")
print(FindExposedSides(cubes))
