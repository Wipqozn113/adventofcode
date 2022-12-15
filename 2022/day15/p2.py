# Inject into python path
import sys
import os
import re 
import math

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates import Coordinate, HorizontalLine

class Lines:
    def __init__(self):
        self.lines = {}

    def AddLine(self, line):
        y = line.start.y
        # Create set for row if doens't exist
        if y not in self.lines:
            self.lines[y] = set()

        if line in self.lines[y]:
            return
        
        # Keep joining lines until all lines are joiend
        ln = line
        while True:
            ln, joined = self.JoinLines(ln, y)
            if not joined:                
                break
        self.lines[y].add(ln)
                    
    #  If line can be joined, creates a new set with the joined lined removed
    def JoinLines(self, line, y):
        lines = set()
        ln = line
        joined = False
        while len(self.lines[y]) > 0:
            ln = self.lines[y].pop()
            if ln.CanJoin(line) and not joined:
                print("LINE 1: ", line)
                ln.JoinLines(line)
                joined = True
            else:
                lines.add(ln)    
        self.lines[y] = lines
        return ln, joined 


class Zone:
    def __init__(self, centre,  radius):
        self.centre = Coordinate(centre.x, centre.y)
        self.radius = radius
    
    def CoordinatesCovered(self, set, row, limit, lines):
        mn = 0
        mx = limit
        row_centre = Coordinate(self.centre.x, row)
        row_distance = self.centre.ManhattenDistance(row_centre) 
        # Row is too far away, so points are not inside this Zone
       
        if(row_distance > self.radius):
            return set
        
        # Row is close enough to be contained within zone
        col_length = 2 * (self.radius - row_distance) + 1
        col_radius = math.floor(col_length / 2)
        if (self.centre.x - col_radius < mn) and (self.centre.x + col_radius > mx):
            return set
        
        start = Coordinate(max((mn,self.centre.x - col_radius)), row)
        end = Coordinate(min((mx,self.centre.x + col_radius)), row)
        line = HorizontalLine(start, end)
        lines.AddLine(line)

        return set        

    def ContainsCoordinate(self, coordinate):
        pass

class Beacon:
    def __init__(self, x, y):
        self.coord = Coordinate(x, y)
        self.sensors = []

    def TuningFrequency(self):
        return (self.coord.x * 4000000) + self.coord.y

    def __eq__(self, other):
        return self.coord == other.coord

class Sensor:
    def __init__(self, x, y, beacon):
        self.coord = Coordinate(x, y)
        self.beacon = beacon
        self.closest_beacon = beacon
        self.beacon_distance = self.coord.ManhattenDistance(beacon.coord)
        self.beacon.sensors.append(self)
        self.coverage = Zone(self.coord, self.beacon_distance)        

    def CalculateCoverage(self, set, row, limit, lines):
        self.coverage.CoordinatesCovered(set, row, limit, lines)

    def InsideImpossibleZone(self, coordinate):
        return self.coverage.ContainsCoordinate(coordinate)

def CreateSensors(filename):
    sensors = []
    beacons = []
    with open(filename) as file:
        for line in file:
            line = line.strip()
            matches = re.findall(r"-?\d+", line)
            beacon = Beacon(matches[2], matches[3])      
            #if beacon in beacons:
            #    pass # get the beacon somehow?
            sensor = Sensor(matches[0], matches[1], beacon)
            sensors.append(sensor)
            beacons.append(beacon)

    return (sensors, beacons)

def ImpossiblePositions(sensors, row, set, limit, lines):
    for sensor in sensors:
        sensor.CalculateCoverage(set, row, limit, lines)
    
    return set

def FindDistressBeacon(sensors, limit, lines):
    positions = set()
    for row in range(limit + 1):
        print("On row {}".format(row))
        ImpossiblePositions(sensors, row, positions, limit, lines)

    for y in range(limit):
        # Two lines means there's a possible space
        ln = lines.lines[y].pop()
        print(ln.start.x, ln.start.y, ln.end.x, ln.end.y)
        if len(lines.lines[y]) > 1:
            print("um")
            ln1 = lines.lines[y].pop()
            ln2 = lines.lines[y].pop()
            if ln1.start.x < ln2.start.x:
                x = ln1.end.x + 1
            else:
                x = ln1.start.x - 1
    return Beacon(x, y)

        


lines = Lines()
filename, limit = ("test.in", 20)
#filename, limit = ("input.in", 4000000)
sensors, beacons = CreateSensors(filename)
beacon = FindDistressBeacon(sensors, limit, lines)
print(beacon.TuningFrequency())



            