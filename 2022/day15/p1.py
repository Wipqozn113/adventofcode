# Inject into python path
import sys
import os
import re 
import math

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates import Coordinate, HorizontalLine, Rhombus

class Lines:
    def __init__(self):
        self.lines = {}

    def Gap(self, y):
        if y not in self.lines:
            return None
        if(len(self.lines[y]) < 2):
            return None

        ln1 = self.lines[y].pop()
        ln2 = self.lines[y].pop()
        if ln1.start.x < ln2.start.x:
            x = ln1.end.x + 1
        else:
            x = ln1.start.x - 1

        return Beacon(x, y)       

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
            if not joined or ln in self.lines[y]:                
                break
        self.lines[y].add(ln)
                    
    #  If line can be joined, creates a new set with the joined lined removed
    def JoinLines(self, line, y):
        lines = set()
        joined_line = line
        joined = False
        while len(self.lines[y]) > 0:
            ln = self.lines[y].pop()
            if ln.CanJoin(line) and not joined:
                ln.JoinLines(line)
                joined = True
                joined_line = ln
            else:
                lines.add(ln)  
        # Can't join line, so just add it  
        if not joined:
            lines.add(line)
        self.lines[y] = lines
        return joined_line, joined 

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
        self.rhombus = Rhombus(self.coord, self.beacon_distance)

    def GetLineAtRow(self, row, limit = None):
        distance_y = row - self.coord.y
        line = self.rhombus.LineFromCentreY(distance_y)
        if line is None:
            return None

        if limit is not None:
            line.Trim(0, limit)

        return line

def CreateSensors(filename):
    sensors = []
    beacons = []
    with open(filename) as file:
        for line in file:
            line = line.strip()
            matches = re.findall(r"-?\d+", line)
            beacon = Beacon(matches[2], matches[3])      
            sensor = Sensor(matches[0], matches[1], beacon)
            sensors.append(sensor)

    return sensors

def FindDistressBeacon(sensors, limit):
    for row in range(limit + 1):
        if row % 100000 == 0:
            print("Row ", row)
        lines = Lines()
        for sensor in sensors:
            line = sensor.GetLineAtRow(row, limit)
            if line is not None:
                lines.AddLine(line)
        gap = lines.Gap(row)
        if gap is not None:
            return gap # gap is beacon      

def PositionsWithoutBeacon(sensors, y):
    lines = Lines()
    for sensor in sensors:
        line = sensor.GetLineAtRow(y)
        if line is not None:
            lines.AddLine(line)
    count = 0
    for ln in lines.lines[y]:
        count += ln.Len()
    
    return count

#filename, y = ("test.in", 10)
filename, y = ("input.in", 2000000)
sensors = CreateSensors(filename)
print(PositionsWithoutBeacon(sensors, y))