class Coordinates:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z
        self.coord = [x, y, z]
    
    def __add__(self, other):
        return Coordinates(self.x + other.x, self.y + other.y, self.z + other.z)

    def __eq__(self, other):
        return (self.x == other.x) and (self.y == other.y) and (self.z == other.z)

class Rotation:
    def __init__(self, coordinates):
        x = coordinates.x
        y = coordinates.y
        z = coordinates.z
        self.rotations = [
            # x is facing x
            Coordinates(x, y, z),
            Coordinates(x, -z, y),
            Coordinates(x, -y, -z),
            Coordinates(x, z, -y),
            # x is facing -x
            Coordinates(-x, -y, z),
            Coordinates(-x, -z, -y),
            Coordinates(-x, y, -z),
            Coordinates(-x, z, y),
            # x is facing y
            Coordinates(-z, x, -y),
            Coordinates(y, x, -z),
            Coordinates(z, x, y),
            Coordinates(-y, x, z),
            # x is facing -y
            Coordinates(z, -x, -y),
            Coordinates(y, -x, z),
            Coordinates(-z, -x, y),
            Coordinates(-y, -x, -z),
            # x is facing z
            Coordinates(-y, -z, x),
            Coordinates(z, -y, x),
            Coordinates(y, z, x),
            Coordinates(-z, y, x),
            # x is facing -z
            Coordinates(z, y, -x),
            Coordinates(-y, z, -x),
            Coordinates(-z, -y, -x),
            Coordinates(y, -z, -x)
        ]
    
    def Print(self):
        facings = ["x", "-x", "y", "-y", "z", "-z"]
        i = 0
        n = 0
        for rotation in self.rotations:
            if i % 4 == 0:
                print(f"X is facing {facings[n]}")
                n += 1
                
            i += 1
            print(rotation)

class Beacon:
    def __init__(self, coordinates):
        self.coord = coordinates
        self.rotations = Rotation(coordinates)

    def Rotation(self, rot):
        return self.rotations.rotations[rot]

class Scanner:
    def __init__(self, id, beacons=[], coordinates=None):
        self.id = id
        self.beacons = []
        self.coord = coordinates
     
    def SetCoordinates(self, x, y, z):
        self.coord = Coordinates(x, y, z)

    def AddBeacon(self, beacon):
        self.beacons.append(beacon)

    def Overlapping(self, other):
        for beacon_s in self.beacons:
            for beacon_o in other.beacons:
                for rot in range(24):
                    position_o = beacon_s.coord + beacon_o.Rotation(rot)       
                    if position_o.x == 68 and position_o.y == -1246 and position_o.z == -43:
                        print("ok") 
                    matches = 0
                    matching_beacons = []
                    for bs in self.beacons:
                        for bo in other.beacons:
                            if position_o == bs.coord + bo.Rotation(rot):
                                matches += 1
                                matching_beacons.append(bs.coord.coord)

                    if matches == 12:
                        print(matching_beacons)
                        return True
                    
        return False



scanners = []
rotations = []
with open('s2.in') as file:
    for line in file:
        if line[0:2] == "--":
            scanner = Scanner(len(scanners))
            scanners.append(scanner)
            if len(scanners) == 1:
                scanner.SetCoordinates(0, 0, 0)
        elif line.strip() == "":
            continue
        else:
            l = line.strip().split(",")
            beacon = Beacon(Coordinates(int(l[0]), int(l[1]), int(l[2])))
            scanner.AddBeacon(beacon)

scanners[0].Overlapping(scanners[1])

