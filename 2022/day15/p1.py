# Inject into python path
import sys
import os
import re 
m = re.search(r"\d+", s)

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.coordinates import Coordinate

class Beacon:
    def __init__(self, x, y):
        self.coord = Coordinate(x, y)

    def __eq__(self, other):
        return self.coord == other.coord

class Sensor:
    def __init__(self, x, y, beacon):
        self.coord = Coordinate(x, y)
        self.closest_beacon = beacon

def CreateSensors(filename):
    sensors = []
    beacons = set()
    with open(filename) as file:
        for line in file:
            line = line.strip()
            matches = re.findall(r"-?\d+", line)
            beacon = Beacon(matches[2], matches[3])      
            if beacon in beacons:
                pass # get the beacon somehow?
            sensor = Sensor(matches[0], matches[1], beacon)
            sensors.append(sensor)
            beacons.add(beacon)

            