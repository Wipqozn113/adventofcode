class Coordinates:
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z
        self.coord = [x, y, z]
    
    def __add__(self, other):
        return Coordinates(self.x + other.x, self.y + other.y, self.z + other.z)

    def __sub__(self, other):
        return Coordinates(self.x - other.x, self.y - other.y, self.z - other.z)

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
            Coordinates(x, y, -z),
            Coordinates(x, -y, z),
            Coordinates(x, -y, -z),

            Coordinates(x, z, y),
            Coordinates(x, z, -y),
            Coordinates(x, -z, y),
            Coordinates(x, -z, -y),

              # x is facing -x
            Coordinates(-x, y, z),
            Coordinates(-x, y, -z),
            Coordinates(-x, -y, z),
            Coordinates(-x, -y, -z),

            Coordinates(-x, z, y),
            Coordinates(-x, z, -y),
            Coordinates(-x, -z, y),
            Coordinates(-x, -z, -y),

            # x is facing y
            Coordinates(z, x, y),
            Coordinates(z, x, -y),
            Coordinates(-z, x, y),
            Coordinates(-z, x, -y),
        
            Coordinates(y, x, z),
            Coordinates(y, x, -z),
            Coordinates(-y, x, z),
            Coordinates(-y, x, -z),

            # x is facing -y
            Coordinates(z, -x, y),
            Coordinates(z, -x, -y),
            Coordinates(-z, -x, y),
            Coordinates(-z, -x, -y),
        
            Coordinates(y, -x, z),
            Coordinates(y, -x, -z),
            Coordinates(-y, -x, z),
            Coordinates(-y, -x, -z),
            
            # x is facing z
            Coordinates(y, z, x),
            Coordinates(y, -z, x),
            Coordinates(-y, z, x),
            Coordinates(-y, -z, x),
           
            Coordinates(z, y, x),
            Coordinates(z, -y, x),
            Coordinates(-z, y, x),
            Coordinates(-z, -y, x),
  
            # x is facing -z
            Coordinates(y, z, -x),
            Coordinates(y, -z, -x),
            Coordinates(-y, z, -x),
            Coordinates(-y, -z, -x),
           
            Coordinates(z, y, -x),
            Coordinates(z, -y, -x),
            Coordinates(-z, y, -x),
            Coordinates(-z, -y, -x),
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

    def __eq__(self, other):
        return self.coord == other.coord

class CompositeScanner:
    def __init__(self, scanner):
        self.scanner0 = scanner
        self.scanners = [scanner]

    def FindOverlapping(self, other):
        for scanner in self.scanners:
            print(scanner.id, other.id)
            matches, beacons, coord = scanner.Overlapping(other)
            if matches:
                print(coord.x, coord.y, coord.z)
                self.scanners.append(Scanner(other.id, beacons, coord))  

                return True
        return False
            
    @property
    def TotalBeacons(self):
        beacons = []
        for scanner in self.scanners:
            for beacon in scanner.beacons:
                if beacon not in beacons:
                    beacons.append(beacon)

        return len(beacons)

       

class Scanner:
    def __init__(self, id, beacons=None, coordinates=None):
        self.id = id
        self.beacons = beacons if beacons is not None else []
        self.coord = coordinates
     
    def SetCoordinates(self, x, y, z):
        self.coord = Coordinates(x, y, z)

    def AddBeacon(self, beacon):
        self.beacons.append(beacon)

    def Overlapping(self, other):
        for beacon_s in self.beacons:
            for beacon_o in other.beacons:
                for rot in range(48):
                    position_o = beacon_s.coord + beacon_o.Rotation(rot)       
                    
                    matches = 0
                    matching_beacons = []
                    for bs in self.beacons:
                        for bo in other.beacons:
                            if position_o == bs.coord + bo.Rotation(rot):
                                matches += 1
                                matching_beacons.append(bs)

                    if matches == 12:
                        beacons = []                         
                        for beacon in other.beacons:
                            coord = position_o - beacon.Rotation(rot) 
                            beacons.append(Beacon(coord))
                        return (True, beacons, position_o)
                    
        return (False, None, None)


scanner0 = None
scanners = []
rotations = []
with open('p1.in') as file:
    for line in file:
        if line[0:2] == "--":
            scanner = Scanner(len(scanners)+1)            
            if scanner0 is None:
                scanner0 = scanner
                scanner0.id = 0
                scanner.SetCoordinates(0, 0, 0)
            else:
                scanners.append(scanner)
        elif line.strip() == "":
            continue
        else:
            l = line.strip().split(",")
            beacon = Beacon(Coordinates(int(l[0]), int(l[1]), int(l[2])))
            scanner.AddBeacon(beacon)

compscanner = CompositeScanner(scanner0)
while True:
    unmatched = []
    if len(scanners) == 0:
        break
    indexes = []
    for scanner in scanners:
        matched = compscanner.FindOverlapping(scanner)
        if not matched:
            unmatched.append(scanner)
    scanners = list(unmatched)
            

print(compscanner.TotalBeacons)

