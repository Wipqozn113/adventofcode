# Inject into python path
import sys
import os
import re 
import math

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates import Coordinate

class Zone:
    def __init__(self, centre,  radius):
        self.centre = Coordinate(centre.x, centre.y)
        self.radius = radius
    
    def CoordinatesCovered(self, set, row, limit):
        mn = 0
        mx = limit
        row_centre = Coordinate(self.centre.x, row)
        row_distance = self.centre.ManhattenDistance(row_centre) 
        # Row is too far away, so points are inside this Zone
        if(row_distance > self.radius):
            return set

        # Row is close enough to be contained within zone
        col_length = 2 * (self.radius - row_distance) + 1
        col_radius = math.floor(col_length / 2)
        for x in range(self.centre.x - col_radius, self.centre.x + col_radius + 1):       
            if x >= mn and x <= mx:     
                set.add(Coordinate(x, row))

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

    def CalculateCoverage(self, set, row, limit):
        self.coverage.CoordinatesCovered(set, row, limit)

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

def ImpossiblePositions(sensors, row, set, limit):
    for sensor in sensors:
        sensor.CalculateCoverage(set, row, limit)
    
    return set

def FindDistressBeacon(sensors, limit):
    positions = set()
    for row in range(limit + 1):
        if(row % 10000 == 0):
            print("On row {}".format(row))
        ImpossiblePositions(sensors, row, positions, limit)

    for x in range(limit):
        for y in range(limit):
            coord = Coordinate(x, y)
            if coord not in positions:
                print(coord.x, coord.y)
                return Beacon(coord.x, coord.y)



filename, limit = ("test.in", 20)
#filename, limit = ("input.in", 4000000)
sensors, beacons = CreateSensors(filename)
beacon = FindDistressBeacon(sensors, limit)
print(beacon.TuningFrequency())



            